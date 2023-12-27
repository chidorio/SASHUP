using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using desktop.Models;
using desktop.Services;
using ReactiveUI;

namespace desktop.ViewModels;

public class MainViewModel : ViewModelBase
{
    private IUserReository _userReository;
    private IAuthorizationRepository _authorizationRepository;
    private IAccessTokenRepository _accessTokenRepository;
    private IRefreshTokenRepository _refreshTokenRepository;
    private IViewNavigation _viewNavigation;
    private readonly Lazy<CatalogTeachersViewModel> _catalogTeachersViewModel;
    private IStatusTeacherRepositosy _statusTeacherRepositosy;
    private ITypeEmploymentRepository _typeEmploymentRepository;
    private readonly IDialogService _dialogService;

    private IStatisticRepository _statisticRepository;
    private readonly Lazy<ReportViewModel> _reportViewModel;
    public MainViewModel(IUserReository userReository,
                            IAuthorizationRepository authorizationRepository,
                            IAccessTokenRepository accessTokenRepository,
                            INotificationService notificationService,
                            IUpdateTokenService updateTokenService,
                            IRefreshTokenRepository refreshTokenRepository,
                            IViewNavigation viewNavigation,
                            ITeacherRepository teacherRepository,
                            IStatusTeacherRepositosy statusTeacherRepositosy,
                            ITypeEmploymentRepository typeEmploymentRepository,
                            IDialogService dialogService,
                            IStatisticRepository statisticRepository) : base(notificationService,updateTokenService)
    {
        _userReository = userReository;
        _authorizationRepository = authorizationRepository;
        _accessTokenRepository = accessTokenRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _viewNavigation = viewNavigation;
        _statusTeacherRepositosy = statusTeacherRepositosy;
        _typeEmploymentRepository = typeEmploymentRepository;
        _dialogService = dialogService;
        _statisticRepository = statisticRepository;
        GetUserInfoCommand = ReactiveCommand.CreateFromTask<User>(GetUserInfo);
        GetUserInfoCommand.ThrownExceptions.Subscribe(async exc => CommandExc(exc,GetUserInfoCommand));
        GetUserInfoCommand.IsExecuting.ToProperty(this, x => x.IsUserInfoLoading, out _isUserInfoLoading);
        _user = GetUserInfoCommand.ToProperty(this, x => x.User);
        GetUserInfoCommand.Execute().Subscribe();
        _catalogTeachersViewModel = new Lazy<CatalogTeachersViewModel>(()=>new CatalogTeachersViewModel(teacherRepository,accessTokenRepository,notificationService,updateTokenService,statusTeacherRepositosy,typeEmploymentRepository,dialogService,viewNavigation));
        _reportViewModel = new Lazy<ReportViewModel>(()=> new ReportViewModel(accessTokenRepository, updateTokenService,
            notificationService, _statisticRepository));
        ExitCommang = ReactiveCommand.CreateFromTask(Exit);
        ExitCommang.ThrownExceptions.Subscribe(async x => CommandExc(x, ExitCommang));
        this.WhenAnyValue(t=>t.SelectedMenuItem).Where(t=>t!=null).Subscribe((x) =>
        {
            if (x.Name == "Выйти")
                Exit();
            else if (x.Name=="Преподаватели")
                SelectedViewModel = _catalogTeachersViewModel.Value;
            else if(x.Name == "Отчеты")
                SelectedViewModel = _reportViewModel.Value;
        });
    }
    private readonly ObservableAsPropertyHelper<bool> _isUserInfoLoading;
    private readonly ObservableAsPropertyHelper<User> _user;
    private async Task<User> GetUserInfo()
    {
        User user = await _userReository.GetUserInfo(_accessTokenRepository.GetAccessToken());
        var menuItems = new List<MenuItem>();
        menuItems.Add(new MenuItem(){Name="Преподаватели",Icon="HumanMaleBoard"});
        menuItems.Add(new MenuItem(){Name="Группы",Icon="AccountSchoolOutline"});
        menuItems.Add(new MenuItem(){Name="Расписание",Icon="BookClockOutline"});
        menuItems.Add(new MenuItem(){Name="Отчеты",Icon="chartBar"});
        menuItems.Add(new MenuItem(){Name="Настройки",Icon="Cogs"});
        menuItems.Add(new MenuItem(){Name="Выйти",Icon="LocationExit"});
        Menu = menuItems;
        SelectedMenuItem = Menu[0];
        return user;
    }
    public ReactiveCommand<Unit,User> GetUserInfoCommand {get;}
    public bool IsUserInfoLoading => _isUserInfoLoading.Value;
    public User User => _user.Value;
    private List<MenuItem> _menu;
    private MenuItem _selectedMenuItem;
    private ViewModelBase _selectedViewModel;
    public List<MenuItem> Menu
    {
        get=>_menu;
        set=> this.RaiseAndSetIfChanged(ref _menu,value);
    }
    public MenuItem SelectedMenuItem
    {
        get=>_selectedMenuItem;
        set=> this.RaiseAndSetIfChanged(ref _selectedMenuItem,value);
    }
    public ReactiveCommand<Unit,Unit> ExitCommang {get;}
    private async Task Exit()
    {
        _viewNavigation.GoToAndCloseOthers<LoginViewModel>();
        await _authorizationRepository.Logout(_accessTokenRepository.GetAccessToken());
        _accessTokenRepository.DeleteAccessToken();
        await _refreshTokenRepository.DeleteRefreshToken();
    }
    public ViewModelBase SelectedViewModel
    {
        get=>_selectedViewModel;
        set=> this.RaiseAndSetIfChanged(ref _selectedViewModel,value);
    }
}

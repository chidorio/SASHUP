using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using desktop.Models;
using desktop.Services;
using desktop.Tools;
using desktop.ViewModels;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace desktop.ViewModels;

public class CatalogTeachersViewModel:ViewModelBase
{
  private readonly ITeacherRepository _teacherRepository;
  private readonly IAccessTokenRepository _accessTokenRepository;
  private readonly IViewNavigation _viewNavigation;
  private readonly ObservableAsPropertyHelper<bool> _isLoadingTeachers;
  private readonly ObservableAsPropertyHelper<IEnumerable<Teacher>> _teachers;
  private ReactiveCommand<Unit,Unit> _cancelComand;
  private Teacher _selectedTeacher;

  public List<FilterCategory> Filters {get;set;}
  private readonly IDialogService _dialogService;
  public CatalogTeachersViewModel(ITeacherRepository teacherRepository,
                                  IAccessTokenRepository accessTokenRepository,
                                  INotificationService notificationService,
                                  IUpdateTokenService updateTokenService,
                                  IStatusTeacherRepositosy statusTeacherRepositosy,
                                  ITypeEmploymentRepository typeEmploymentRepository,
                                  IDialogService dialogService,
                                  IViewNavigation viewNavigation) : base(notificationService,updateTokenService)
  {
    _teacherRepository = teacherRepository;
    _accessTokenRepository = accessTokenRepository;
    _statusTeacherRepositosy = statusTeacherRepositosy;
    _typeEmploymentRepository = typeEmploymentRepository;
    _dialogService = dialogService;
    _viewNavigation = viewNavigation;
    LoadingTeachers = ReactiveCommand
      .CreateFromObservable(
        () => Observable
              .StartAsync(ct => this.LoadingTeachersTask(ct))
              .TakeUntil(_cancelComand));
    LoadingTeachers.IsExecuting.ToProperty(this,x=>x.IsLoadingTeachers, out _isLoadingTeachers);
    LoadingTeachers.ThrownExceptions.Subscribe(async (x) => await CommandExc(x,LoadingTeachers));
    _teachers = LoadingTeachers.Where(p=>p!=null).Select(x=>x.Teachers).ToProperty(this,x=>x.Teachers,scheduler:RxApp.MainThreadScheduler);
    _cancelComand = ReactiveCommand.Create(()=>{},LoadingTeachers.IsExecuting);
    LoadingTeacherDetails = ReactiveCommand.CreateFromTask<Teacher,TeacherDetails>(GetSelectedTeacherDetails);
    LoadingTeacherDetails.IsExecuting.ToProperty(this, x=>x.IsLoadingTeacherDetails, out _isLoadingTeacherDetails);
    LoadingTeacherDetails.ThrownExceptions.Subscribe(async (x) => await CommandExc(x,LoadingTeachers));
    this.WhenAnyValue(x=>x.SelectedTeacher).Where(p => p != null).InvokeCommand(LoadingTeacherDetails);
    _teacherDetails = LoadingTeacherDetails.ToProperty(this, x=>x.SelectedTeacherDelails);
    _countTeachers = LoadingTeachers.Where(x=>x!=null).Select(x=>x.Count).ToProperty(this,x=>x.CountTeachers,scheduler:RxApp.MainThreadScheduler);
    OwnerParameters = new OwnerParameters();
    this.WhenAnyValue(x=>x.CountTeachers,x=>x.OwnerParameters.SizePage)
      .Where(x=>x.Item2 !=0)
      .Select(x=>(x.Item1+x.Item2-1)/x.Item2)
      .ToProperty(this,x=>x.CountPage,out _countPage);
    OwnerParameters.WhenAnyValue(p=>p.PageNumber).Subscribe(_=>RestartLoadTeachers());
    OwnerParameters.WhenAnyValue(p=>p.SizePage).Subscribe(_=>GoToFirstPageAndRestartLoadTeachers());
    OwnerParameters.WhenAnyValue(p=>p.SearchString).Where(p=>!String.IsNullOrEmpty(p))
      .Throttle(TimeSpan.FromSeconds(0.75)).Subscribe(_=>GoToFirstPageAndRestartLoadTeachers());
    
    //Filters
    Filters = new List<FilterCategory>()
    {
      new FilterCategory() {NameCategory = "Статус", ParameterName="statusTeacher"},
      new FilterCategory() {NameCategory = "Тип трудоустройства",ParameterName="typeEmployment"},
    };
    ReactiveCommand.CreateFromTask(LoadFilters).Execute();
    OwnerParameters.WhenAnyValue(p=>p.Filters).Subscribe(_=>GoToFirstPageAndRestartLoadTeachers());
    //Sorts
    SortElements = new ObservableCollection<SortElement>
    {
      new SortElement(){Sort = new Sort(){NameColumn = "familyteacher"},NameSort = "Фамилия преподавателя"},
    };
    SortElements.ToObservableChangeSet()
      .AutoRefresh(p=>p.IsSelected)
      .ToCollection()
      .Subscribe(p=>
      {
        OwnerParameters.Sorts = JsonSerializer.Serialize(p.Where(x=>x.IsSelected).Select(p=>p.Sort).ToList());
      });
    SortElements.ToObservableChangeSet()
      .AutoRefresh(p=>p.Sort.Direction)
      .ToCollection()
      .Subscribe(p=>
      {
        OwnerParameters.Sorts = JsonSerializer.Serialize(p.Where(x=>x.IsSelected).Select(p=>p.Sort).ToList());
      });
    OwnerParameters.WhenAnyValue(p=>p.Sorts).Subscribe(_=>GoToFirstPageAndRestartLoadTeachers());
    //Remove Teacher
    RemoveTeacherCommand = ReactiveCommand.CreateFromTask<int>(RemoveTeacher);
    RemoveTeacherCommand.ThrownExceptions.Subscribe(async exp => await CommandExc(exp,LoadingTeachers));
    //Edit Teacher
    EditTeacherCommand = ReactiveCommand.Create<int>(EditTeacher);
  }
  private async Task<TeachersCollection> LoadingTeachersTask(CancellationToken ct)
  {
    var accessToken = _accessTokenRepository.GetAccessToken();
    if (ct.IsCancellationRequested)
      return null;
    var teachersCollection = await _teacherRepository.GetTeachers(accessToken,OwnerParameters);
    if(ct.IsCancellationRequested)
      return null;
    SelectedTeacher = teachersCollection.Teachers.Count() > 0 ? teachersCollection.Teachers.ToList()[0] : null;
    return teachersCollection;
  }
  public Teacher SelectedTeacher
  {
    get => _selectedTeacher;
    set => this.RaiseAndSetIfChanged(ref _selectedTeacher, value);
  }
  public bool IsLoadingTeachers => _isLoadingTeachers.Value;
  public IEnumerable<Teacher> Teachers => _teachers.Value;
  public ReactiveCommand<Unit,TeachersCollection> LoadingTeachers{get;private set;}
  private readonly ObservableAsPropertyHelper<bool> _isLoadingTeacherDetails;
  private readonly ObservableAsPropertyHelper<TeacherDetails> _teacherDetails;
  public bool IsLoadingTeacherDetails => _isLoadingTeacherDetails.Value;
  public TeacherDetails SelectedTeacherDelails => _teacherDetails.Value;
  public ReactiveCommand<Teacher,TeacherDetails> LoadingTeacherDetails {get;set;}
  private async Task<TeacherDetails> GetSelectedTeacherDetails(Teacher selectedTeacher)
  {
    return await _teacherRepository.GetTeacher(_accessTokenRepository.GetAccessToken(), selectedTeacher.IdTeacher);
  }
  private readonly ObservableAsPropertyHelper<int> _countPage;
  private readonly ObservableAsPropertyHelper<int> _countTeachers;
  public int CountPage => _countPage.Value;
  public int CountTeachers => _countTeachers.Value;
  public IEnumerable<int> PageCounts => new int[]{5,6,7,8,9,10};
  public OwnerParameters OwnerParameters{get;set;}
  public void NextPage()
  {
    if(OwnerParameters.PageNumber < CountPage)
      OwnerParameters.PageNumber++;
  }
  public void PreviousPage()
  {
    if(OwnerParameters.PageNumber > 1)
      OwnerParameters.PageNumber--;
  }
  public void GoFirstPage() => OwnerParameters.PageNumber = 1;
  public void GoLastPage() => OwnerParameters.PageNumber = CountPage;
  private void RestartLoadTeachers()
  {
    _cancelComand.Execute().Subscribe();
    LoadingTeachers.Execute().Subscribe();
  }
  public void GoToFirstPageAndRestartLoadTeachers()
  {
    if(OwnerParameters.PageNumber == 1)
      RestartLoadTeachers();
    else OwnerParameters.PageNumber = 1;
  }
  private readonly IStatusTeacherRepositosy _statusTeacherRepositosy;
  private readonly ITypeEmploymentRepository _typeEmploymentRepository;
  
  private List<FilterParameter> GetFilterParameters()
  {
    List<FilterParameter> filterParameters = new List<FilterParameter>();
    foreach(FilterCategory filterCategory in Filters)
    {
      if(filterCategory.Filters == null)
        break;
      foreach (Filter filter in filterCategory.Filters.Where(p=>p.IsPick))
      {
        filterParameters.Add(new FilterParameter()
          {NameParameter = filterCategory.ParameterName, Value = filter.Value});
      }
    }
    return filterParameters;
  }
  public ReactiveCommand<int,Unit> RemoveTeacherCommand {get;set;}
  public ReactiveCommand<Unit,Unit> LoadFiltersCommand {get;}
  private async Task LoadFilters()
  {
    var statusTeachers = await _statusTeacherRepositosy.GetStatusTeachers();
    Filters[0].Filters =
      new ObservableCollection<Filter>(statusTeachers.Select(x => new Filter()
        {NameFilter = x.Name, Value = x.Id}));
    var typeEmployments = await _typeEmploymentRepository.GetTypeEmployments();
    Filters[1].Filters = 
      new ObservableCollection<Filter>(typeEmployments.Select(x => new Filter()
        {NameFilter = x.Name, Value = x.Id}));
    Filters.ForEach(x=>x.Filters.ToObservableChangeSet()
      .AutoRefresh(a=>a.IsPick)
      .Subscribe(_=>
      {
        var filterParameters = GetFilterParameters();
        OwnerParameters.Filters = JsonSerializer.Serialize(filterParameters);
      }));
  }
  public ObservableCollection<SortElement> SortElements {get;set;}
  private async Task RemoveTeacher(int id)
  {
    var result = await _dialogService.ShowDialog("Удаление","Вы действительно хотите удалить преподавателя?",IDialogService.DialogType.YesNoDialog);
    if(result == IDialogService.DialogResult.Yes)
    {
      await _teacherRepository.DeleteTeacher(_accessTokenRepository.GetAccessToken(),id);
      GoToFirstPageAndRestartLoadTeachers();
    }
  }
  public ReactiveCommand<int,Unit> EditTeacherCommand {get;}
  private void EditTeacher(int id)
  {
    Bundle bundle = new Bundle(this);
    bundle.AddNewParameter("idTeacher", id);
    _viewNavigation.GoTo<AddEditTeacherViewModel>(bundle);
  }
  public void AddTeacher()
  {
    ViewModels.Bundle bundle = new Bundle(this);
    _viewNavigation.GoTo<AddEditTeacherViewModel>(bundle);
  }
}

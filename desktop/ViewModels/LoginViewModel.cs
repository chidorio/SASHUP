using System;
using System.Net;
using desktop.Models;
using System.Net.Http;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls.Notifications;
using desktop.ViewModels;
using ReactiveUI;
using Refit;
using Notification = Avalonia.Controls.Notifications.Notification;

namespace desktop.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private readonly ObservableAsPropertyHelper<bool> _isLogin;
    private readonly IAccessTokenRepository _accessTokenRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IAuthorizationRepository _authorizationRepository;
    private readonly IViewNavigation _viewNavigation;
    public ReactiveCommand<Unit,Unit> Login{get;private set;}
    public Models.Authorization AuthorizationData {get;private set;}
    public bool IsLogin => _isLogin.Value;
    public async Task LoginTask()
    {
        if(string.IsNullOrEmpty(AuthorizationData.Login) && string.IsNullOrEmpty(AuthorizationData.Password))
        {
            _notificationService.ShowNotification(new Notification("Ошибка","Заполните поля формы",NotificationType.Error));
            return;
        }
        Token token = await _authorizationRepository.Login(AuthorizationData);
        await _refreshTokenRepository.AddRefreshToken(token.RefreshToken);
        _accessTokenRepository.AddAccessToken(token.AccessToken);
        _viewNavigation.GoToAndCloseCurrent<MainViewModel>(this);
    }
    private void LoginThrownExc(Exception exp)
    {
        switch(exp)
        {
            case HttpRequestException:
                _notificationService.ShowNotification(new Notification("","Не удалось установить соединение с сервером"));
            break;
            case ApiException:
                if(((ApiException)exp).StatusCode == HttpStatusCode.Unauthorized)
                    _notificationService.ShowNotification(new Notification("Ошика","Неверный логин и пароль",NotificationType.Error));
            break;
        }
    }
    public LoginViewModel(IAccessTokenRepository accessTokenRepository,IRefreshTokenRepository refreshTokenRepository,
        IAuthorizationRepository authorizationRepository, INotificationService notificationService, IViewNavigation viewNavigation) : base(notificationService)
    {
        _accessTokenRepository = accessTokenRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _authorizationRepository = authorizationRepository;
        _viewNavigation = viewNavigation;

        Login = ReactiveCommand.CreateFromTask(LoginTask);
        Login.IsExecuting.ToProperty(this,x=>x.IsLogin, out _isLogin);
        Login.ThrownExceptions.Subscribe(LoginThrownExc);
        AuthorizationData = new Models.Authorization();
    }
}

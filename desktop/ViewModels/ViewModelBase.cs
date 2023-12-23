using System;
using System.Net;
using System.Net.Http;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls.Notifications;
using desktop.Services;
using ReactiveUI;
using Refit;
using Notification = Avalonia.Controls.Notifications.Notification;

namespace desktop.ViewModels;

public class ViewModelBase : ReactiveObject
{
    private Bundle _bundle;
    private string _title;
    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }
    public Bundle Bundle
    {
        get => _bundle;
        set => this.RaiseAndSetIfChanged(ref _bundle, value);
    }
    protected readonly IUpdateTokenService _updateTokenService;
    protected readonly INotificationService _notificationService;
    public ViewModelBase(INotificationService notificationService, IUpdateTokenService updateTokenService)
    {
        _updateTokenService = updateTokenService;
        _notificationService = notificationService;
    }
    public ViewModelBase(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
    protected async Task CommandExc<TParam,TResult>(Exception e,ReactiveCommand<TParam,TResult> reactiveCommand)
    {
        switch(e)
        {
            case HttpRequestException:
                _notificationService?.ShowNotification(new Notification("Ошибка","Не удалось установить соединение с сервером",NotificationType.Error));
            break;
            case ApiException exception:
                switch (exception.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        await _updateTokenService.UpdateTokens();
                        reactiveCommand.Execute();
                    break;
                    case HttpStatusCode.NotFound:
                    case HttpStatusCode.BadRequest:
                        _notificationService?.ShowNotification(new Notification("Ошибка",exception.Content,NotificationType.Error));
                    break;
                    case HttpStatusCode.InternalServerError:
                        _notificationService?.ShowNotification(new Notification("Ошибка","На стороне сервера произошла ошибка",NotificationType.Error));
                    break;
                    case HttpStatusCode.Forbidden:
                        _notificationService?.ShowNotification(new Notification("Ошибка","Недостаточно прав для выполнения данной операции",NotificationType.Error));
                    break;
                }
            break;
        }
    }
}

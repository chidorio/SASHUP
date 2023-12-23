using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using desktop;
using desktop.Models;
using desktop.Services;
using desktop.ViewModels;
using Refit;
using Splat;
using static desktop.IFilePickerService;

namespace desktop.Views;

public partial class SplashWindow : Window
{
    public const string RestApiURI = "http://localhost:5151";
    public SplashWindow()
    {
        InitializeComponent();
        Dispatcher.UIThread.Post(()=>LoadApp(),DispatcherPriority.Background);
    }
    private async Task LoadApp()
    {
        var statusTextBlock = this.FindControl<TextBlock>("StatusTextBlock");
        statusTextBlock.Text = "Регистрация сервисов...";
        await Task.Run(()=>
        {
            Locator.CurrentMutable.RegisterLazySingleton<SqLiteContext>(()=>new SqLiteContext());
            Locator.CurrentMutable.RegisterLazySingleton<HttpClient>(()=>RestService.CreateHttpClient(RestApiURI,null));
            Locator.CurrentMutable.Register<IUserReository>(()=>RestService.For<IUserReository>(Locator.Current.GetService<HttpClient>()));
            Locator.CurrentMutable.RegisterLazySingleton(()=>RestService.For<IAuthorizationRepository>(Locator.Current.GetService<HttpClient>()));
            Locator.CurrentMutable.Register<ITeacherRepository>(()=>RestService.For<ITeacherRepository>(Locator.Current.GetService<HttpClient>()));
            Locator.CurrentMutable.Register<IStatusTeacherRepositosy>(()=>RestService.For<IStatusTeacherRepositosy>(Locator.Current.GetService<HttpClient>()));
            Locator.CurrentMutable.Register<ITypeEmploymentRepository>(()=>RestService.For<ITypeEmploymentRepository>(Locator.Current.GetService<HttpClient>()));
            Locator.CurrentMutable.Register<IImageService>(()=>RestService.For<IImageService>(Locator.Current.GetService<HttpClient>()));
            SplatRegistrations.RegisterLazySingleton<IAccessTokenRepository,AccessTokenRepository>();
            SplatRegistrations.RegisterLazySingleton<IUpdateTokenService,UpdateTokenService>();
            SplatRegistrations.RegisterLazySingleton<IFilePickerService,FilePickerservice>();
            Locator.CurrentMutable.Register<IDialogService>(()=> new DesktopDialogService());
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                SplatRegistrations.RegisterLazySingleton<IRefreshTokenRepository, RefreshTokenWindowsRepository>();
            }
            else throw new PlatformNotSupportedException();
            SplatRegistrations.Register<LoginViewModel>();
            SplatRegistrations.Register<MainViewModel>();
            SplatRegistrations.Register<AddEditTeacherViewModel>();
            SplatRegistrations.RegisterLazySingleton<IViewNavigation,WindowsNavigation>();
            SplatRegistrations.RegisterLazySingleton<INotificationService,NotificationService>();
            SplatRegistrations.SetupIOC();
        });
    statusTextBlock.Text = "Попытка авторизации через токен...";
    string? token = await Task<string?>.Run(()=>
    {
        IRefreshTokenRepository refreshTokenRepository;
        refreshTokenRepository = Locator.Current.GetService<IRefreshTokenRepository>();
        return refreshTokenRepository.GetRefreshToken();
    });
    if(token == null)
    {
        Locator.Current.GetService<IViewNavigation>().GoToAndCloseCurrent<LoginViewModel>((ViewModelBase)DataContext);
        return;
    }
    try
    {
        var newTokens = await Locator.Current.GetService<IAuthorizationRepository>().UpdateToken(token);
        await Locator.Current.GetService<IRefreshTokenRepository>().UpdateRefreshToken(token);
        Locator.Current.GetService<IAccessTokenRepository>().AddAccessToken(newTokens.AccessToken);
        statusTextBlock.Text = "Входим в систему...";
        Locator.Current.GetService<IViewNavigation>().GoToAndCloseCurrent<MainViewModel>((ViewModelBase)DataContext);
    }
    catch(HttpRequestException)
    {
        statusTextBlock.Text = "Не удалось подключиться к серверу, выходим...";
        await Task.Delay(2000);
        this.Close();
        return;
    }
    catch(ApiException ex)
    {
        if(ex.StatusCode == System.Net.HttpStatusCode.InternalServerError)
        {
            statusTextBlock.Text = "Ошибка со стороны сервера, выходим...";
            await Task.Delay(2000);
            this.Close();
            return;
        }
    }
    await Locator.Current.GetService<IRefreshTokenRepository>().DeleteRefreshToken();
    Locator.Current.GetService<IViewNavigation>().GoToAndCloseCurrent<LoginViewModel>((ViewModelBase)DataContext);
    }
}
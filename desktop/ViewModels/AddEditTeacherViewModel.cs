using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia;
using desktop.Models;
using desktop.Services;
using desktop.ViewModels;
using ReactiveUI;
using Refit;

namespace desktop.ViewModels;

public class AddEditTeacherViewModel : ViewModelBase
{
  private readonly IViewNavigation _viewNavigation;
  private readonly IAccessTokenRepository _accessTokenRepository;
  private readonly ITeacherRepository _teacherRepository;
  private readonly IStatusTeacherRepositosy _statusTeacherRepositosy;
  private readonly ITypeEmploymentRepository _typeEmploymentRepository;
  private readonly IFilePickerService _filePickerService;
  private readonly IImageService _imageService;
  private TeacherEdit _teacher;
  private readonly Lazy<ReactiveCommand<int,TeacherEdit>> _lazyGetTeacherCommand;
  private readonly ObservableAsPropertyHelper<IEnumerable<StatusTeacher>> _statusTeacher;
  private readonly ObservableAsPropertyHelper<IEnumerable<TypeEmployment>> _typeEmployment;
  public IEnumerable<StatusTeacher> StatusTeacher => _statusTeacher.Value;
  public IEnumerable<TypeEmployment> TypeEmployment => _typeEmployment.Value;
  public ReactiveCommand<Unit, IEnumerable<StatusTeacher>> GetStatusTeacherCommand {get;private set;}
  public ReactiveCommand<Unit, IEnumerable<TypeEmployment>> GetTypeEmploymentCommand {get;private set;}
  public ReactiveCommand<Unit, Unit> SaveCommand {get;private set;}
  public ReactiveCommand<string,Unit> ChangeImageTeacherCommand {get;}
  public ReactiveCommand<string,Unit> RemoveImageCommand {get;}
  public TeacherEdit Teacher
  {
    get => _teacher;
    set => this.RaiseAndSetIfChanged(ref _teacher, value);
  }
  public AddEditTeacherViewModel(IViewNavigation viewNavigation,
                                IAccessTokenRepository accessTokenRepository,
                                IUpdateTokenService updateTokenService,
                                ITeacherRepository teacherRepository,
                                IStatusTeacherRepositosy statusTeacherRepositosy,
                                ITypeEmploymentRepository typeEmploymentRepository,
                                INotificationService notificationService,
                                IFilePickerService filePickerService,
                                IImageService imageService):base(notificationService,updateTokenService)
  {
    _viewNavigation = viewNavigation;
    _accessTokenRepository = accessTokenRepository;
    _teacherRepository = teacherRepository;
    _statusTeacherRepositosy = statusTeacherRepositosy;
    _typeEmploymentRepository = typeEmploymentRepository;
    _filePickerService = filePickerService;
    _imageService = imageService;
    _lazyGetTeacherCommand = new Lazy<ReactiveCommand<int, TeacherEdit>>(()=>
    {
      var command = ReactiveCommand.CreateFromTask<int,TeacherEdit>(GetTeacher);
      command.ThrownExceptions.Subscribe(async x=> await CommandExc(x,command));
      return command;
    });
    GetStatusTeacherCommand = ReactiveCommand.CreateFromTask(async () => await _statusTeacherRepositosy.GetStatusTeachers());
    GetStatusTeacherCommand.ThrownExceptions.Subscribe(async x => CommandExc(x, GetStatusTeacherCommand));
    _statusTeacher = GetStatusTeacherCommand.ToProperty(this, x=> x.StatusTeacher);
    GetStatusTeacherCommand.Subscribe(_=>
    {
      GetTypeEmploymentCommand?.Execute().Subscribe();
    });
    SaveCommand = ReactiveCommand.CreateFromTask(async () =>
    {
      if(Bundle.GetParameter("idTeacher")!=null)
        await _teacherRepository.UpdateTeacher(_accessTokenRepository.GetAccessToken(),Teacher);
      else await _teacherRepository.AddTeacher(_accessTokenRepository.GetAccessToken(), Teacher);
      (Bundle?.OwnerViewModel as CatalogTeachersViewModel)?.GoToFirstPageAndRestartLoadTeachers();
      _viewNavigation.Close(this);
    });
    SaveCommand.ThrownExceptions.Subscribe(async x=> await CommandExc(x,SaveCommand));
    this.WhenAnyValue(x=> x.Bundle).Where(x=>x!=null).Where(x=>x!=null).Subscribe(_=>
    {
      if(Bundle.GetParameter("idTeacher") != null)
        Title = "Редактировать";
      else
        Title = "Добавить";
        Teacher = new TeacherEdit();
      GetStatusTeacherCommand.Execute().Subscribe();
    });
    GetTypeEmploymentCommand = ReactiveCommand.CreateFromTask(async()=>
    {
      return await _typeEmploymentRepository.GetTypeEmployments();
    });
    GetTypeEmploymentCommand.ThrownExceptions.Subscribe(async x => await CommandExc(x, GetStatusTeacherCommand));
    _typeEmployment = GetTypeEmploymentCommand.ToProperty(this,x=>x.TypeEmployment);
    GetTypeEmploymentCommand.Subscribe(_=>
    {
      object? index = Bundle.GetParameter("idTeacher");
      if(index != null)
        _lazyGetTeacherCommand.Value.Execute((int)index).Subscribe(x=>Teacher = x);
      else Teacher = new TeacherEdit();
    });
    ChangeImageTeacherCommand = ReactiveCommand.CreateFromTask<string>(ChangeImageTeacher);
    ChangeImageTeacherCommand.ThrownExceptions.Subscribe(async x=> await CommandExc(x, GetStatusTeacherCommand)); //тут про GetPublishersCommand
    RemoveImageCommand = ReactiveCommand.Create<string>((par)=>
    {
      if(par == "image")
        Teacher.Image = null;
    });
  }

    private async Task<TeacherEdit> GetTeacher(int id)
    {
        return await _teacherRepository.GetTeacherEdit(_accessTokenRepository.GetAccessToken(),id);
    }
  private async Task ChangeImageTeacher(string parameter)
  {
    await using var stream = await _filePickerService.OpenFile(IFilePickerService.Filter.PngImage);
    if (stream == null)
      return;
    StreamPart file = new StreamPart(stream, "image.png", "image/png");
    string fileName = await _imageService.UploadImage(_accessTokenRepository.GetAccessToken(), file);
    if(parameter == "image")
      Teacher.Image = fileName;
  }
  
}

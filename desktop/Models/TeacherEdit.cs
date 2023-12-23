using System.Collections.Generic;
using ReactiveUI;

namespace desktop.Models;

public class TeacherEdit : ReactiveObject
{
  private string? _image;
   public int Id{get;set;}
  public string Family{get;set;}
  public string Name{get;set;}
  public string? Patronymic{get;set;}
  public int IdStatusTeacher{get;set;}
  public int IdTypeEmployment{get;set;}
  public string? Image
  {
    get => _image;
    set => this.RaiseAndSetIfChanged(ref _image, value);
  }
}

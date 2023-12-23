using System.Diagnostics.Eventing.Reader;
using api.Controllers;

namespace api.Models.DTO;

public class TeacherDTO
{
  public TeacherDTO(Teacher teacher)
  {
    IdTeacher = teacher.Id;
    FIOTeacher = $"{teacher.Family} {teacher.Name}{teacher.Patronymic}";
    StatusTeacherTeacher = teacher.IdStatusTeacherNavigation.Name;
    TypeEmploymentTeacher = teacher.IdTypeEmploymentNavigation.Name;
    ImageTeacher = teacher.Image;
  }
  public int IdTeacher{get;set;}
  public string FIOTeacher{get;set;}
  public string StatusTeacherTeacher{get;set;}
  public string TypeEmploymentTeacher{get;set;}
  public string? ImageTeacher{get;set;}
}

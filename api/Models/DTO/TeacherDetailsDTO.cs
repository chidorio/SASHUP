namespace api.Models.DTO;

public class TeacherDetailsDTO
{
  public TeacherDetailsDTO(Teacher teacher)
  {
    IdTeacher = teacher.Id;
    FIOTeacher = $"{teacher.Family} {teacher.Name}{teacher.Patronymic}";
    StatusTeacherTeacher = teacher.IdStatusTeacherNavigation.Name;
    TypeEmploymentTeacher = teacher.IdTypeEmploymentNavigation.Name;
  }
  public int IdTeacher{get;set;}
  public string FIOTeacher{get;set;}
  public string StatusTeacherTeacher{get;set;}
  public string TypeEmploymentTeacher{get;set;}
}

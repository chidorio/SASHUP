using api.Models;

namespace api;

public class StatusTeacherDTO
{
  public int Id {get;set;}
  public string Name {get;set;}
  public StatusTeacherDTO(StatusTeacher statusTeacher)
  {
    Id = statusTeacher.Id;
    Name = statusTeacher.Name;
  }
}

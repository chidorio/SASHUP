using System.Text.Json.Serialization;
using api.Models;

namespace api;

public class TeacherEditDTO
{
  [JsonConstructor]
  public TeacherEditDTO()
  {

  }
  public TeacherEditDTO(Teacher teacher)
  {
    Id = teacher.Id;
    Family = teacher.Family;
    Name = teacher.Name;
    Patronymic = teacher.Patronymic;
    IdStatusTeacher = teacher.IdStatusTeacher;
    IdTypeEmployment = teacher.IdTypeEmployment;
    Image = teacher.Image;
  }
  public int Id{get;set;}
  public string Family{get;set;}
  public string Name{get;set;}
  public string? Patronymic{get;set;}
  public int IdStatusTeacher{get;set;}
  public int IdTypeEmployment{get;set;}
  public string? Image{get;set;}
}

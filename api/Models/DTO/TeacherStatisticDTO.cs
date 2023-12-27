using api.Models.DTO;

namespace api;

public class TeacherStatisticDTO
{
  public TeacherDTO Teacher{get;set;}
  public int Count{get;set;}
  public TeacherStatisticDTO(TeacherDTO teacherDTO, int count){
    Teacher = teacherDTO;
    Count = count;
  }
}

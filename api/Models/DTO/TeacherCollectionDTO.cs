namespace api.Models.DTO;

public class TeacherCollectionDTO
{
  public IEnumerable<TeacherDTO> Teachers{get;set;}
  public int Count{get;set;}
  public TeacherCollectionDTO(IEnumerable<TeacherDTO> teachers, int count)
  {
    Teachers = teachers;
    Count = count;
  }
}

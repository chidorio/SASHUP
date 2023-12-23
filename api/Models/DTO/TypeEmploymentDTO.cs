using api.Models;

namespace api;

public class TypeEmploymentDTO
{
  public int Id {get;set;}
  public string Name {get;set;}
  public TypeEmploymentDTO(TypeEmployment typeEmployment)
  {
    Id = typeEmployment.Id;
    Name = typeEmployment.Name;
  }
}

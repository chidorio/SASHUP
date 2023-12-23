using api.Data;
using api.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class TypeEmploymentController : ControllerBase
{
  private StudyDepartmentContext _studyDepartmentContext;
  public TypeEmploymentController(StudyDepartmentContext studyDepartmentContext)
  {
    _studyDepartmentContext = studyDepartmentContext;
  }
  [HttpGet]
  [Route("GetTypeEmployments")]
  public async Task<ActionResult<IEnumerable<TypeEmploymentDTO>>> GetTypeEmployments()
  {
    try
    {
      var typeEmployments = await _studyDepartmentContext.TypeEmployments.ToListAsync();
      return Ok(typeEmployments.ConvertAll(p=>new TypeEmploymentDTO(p)));
    }
    catch(Exception e)
    {
      return StatusCode(StatusCodes.Status501NotImplemented);
    }
  }
}

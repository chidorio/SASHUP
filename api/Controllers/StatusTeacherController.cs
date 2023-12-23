using api.Data;
using api.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class StatusTeacherController : ControllerBase
{
  private StudyDepartmentContext _studyDepartmentContext;
  public StatusTeacherController(StudyDepartmentContext studyDepartmentContext)
  {
    _studyDepartmentContext = studyDepartmentContext;
  }
  [HttpGet]
  [Route("GetStatusTeachers")]
  public async Task<ActionResult<IEnumerable<StatusTeacherDTO>>> GetStatusTeachers()
  {
    try
    {
      var statusTeachers = await _studyDepartmentContext.StatusTeachers.ToListAsync();
      return Ok(statusTeachers.ConvertAll(g=>new StatusTeacherDTO(g)));
    }
    catch(Exception e)
    {
      return StatusCode(StatusCodes.Status501NotImplemented);
    }
  }
}

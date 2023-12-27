using api.Data;
using api.Models;
using api.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api;

[ApiController]
[Route("[controller]")]
public class StatisticController : ControllerBase
{
  private readonly StudyDepartmentContext _studyDepartmentContext;
  public StatisticController(StudyDepartmentContext studyDepartmentContext){
    _studyDepartmentContext = studyDepartmentContext;
  }
  
  [HttpGet]
  [Route("GetTeacherStatistics")]
  public async Task<ActionResult<IEnumerable<TeacherStatisticDTO>>> GetTeacherStatistics([FromQuery]DateRangeDTO dateRange)
  {
    IQueryable<Change> query = _studyDepartmentContext.Changes.Include(p=>p.IdTeacherNavigation).Include(p=>p.IdTeacherNavigation.IdStatusTeacherNavigation)
      .Include(p=>p.IdTeacherNavigation.IdTypeEmploymentNavigation);
    if(dateRange.DateOne != null && dateRange.DateTwo != null && dateRange.DateOne <= dateRange.DateTwo)
      query = query.Where(x=>x.Date >= dateRange.DateOne && x.Date <= dateRange.DateTwo);
    try{
      var result = await query.GroupBy(p=>p.IdTeacherNavigation).ToListAsync();
      return Ok(result.ConvertAll(p=>new TeacherStatisticDTO(new TeacherDTO(p.Key),p.Count())));
    }
    catch(Exception e){
      return StatusCode(StatusCodes.Status500InternalServerError);
    }
  }
  
}

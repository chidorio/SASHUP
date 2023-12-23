using System.Linq.Expressions;
using System.Text.Json;
using api.Data;
using api.Models;
using api.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api;

[ApiController]
[Route("[controller]")]
public class TeacherController : ControllerBase
{
  StudyDepartmentContext _studyDepartmentContext;
  public TeacherController(StudyDepartmentContext studyDepartmentContext)
  {
    _studyDepartmentContext = studyDepartmentContext;
  }
  [Authorize]
  [HttpGet]
  [Route("GetTeachers")]
  public async Task<ActionResult<TeacherCollectionDTO>> GetTeachers([FromQuery]OwnerParametrs ownerParametrs)
  {          
    try
    {
      IQueryable<Teacher> teachersQuery = _studyDepartmentContext.Teachers.Include(x=>x.IdStatusTeacherNavigation).Include(x=>x.IdTypeEmploymentNavigation);
      if(!String.IsNullOrEmpty(ownerParametrs.SearchString))
      {
        string search = ownerParametrs.SearchString.ToLower();
        teachersQuery = teachersQuery.Where(b=>b.Name.ToLower().Contains(search) || b.Family.ToLower().Contains(search));
      }
      if(!String.IsNullOrEmpty(ownerParametrs.Filters))
      {
        List<FilterParameter>? filterParameters = JsonSerializer.Deserialize<List<FilterParameter>>(ownerParametrs.Filters);
        if(filterParameters != null && filterParameters.Count > 0)
        {
          var groupFilters = filterParameters.GroupBy(p=>p.NameParameter);
          foreach(var group in groupFilters)
          {
            switch(group.Key.ToLower())
            {
              case "statusteacher":
                teachersQuery = teachersQuery.Where(p=> group.Select(p=>p.Value).Contains(p.IdStatusTeacher));
              break;
              case "typeemployment":
                teachersQuery = teachersQuery.Where(p=> group.Select(p=>p.Value).Contains(p.IdTypeEmployment));
              break;
            }
          }
        }
      }
      if(!String.IsNullOrEmpty(ownerParametrs.Sorts))
      {
        List<SortDTO>? sortParameters = JsonSerializer.Deserialize<List<SortDTO>>(ownerParametrs.Sorts);
        if(sortParameters != null && sortParameters.Count > 0)
        {
          IOrderedQueryable<Teacher> sortTeachers = null;
          sortParameters.ForEach((x)=>
          {
            if(sortTeachers == null)
              sortTeachers = x.Direction
                ? teachersQuery.OrderByDescending(GetSortProperty(x.NameColumn))
                : teachersQuery.OrderBy(GetSortProperty(x.NameColumn));
            else
              sortTeachers = x.Direction
                ? sortTeachers.ThenByDescending(GetSortProperty(x.NameColumn))
                : sortTeachers.ThenBy(GetSortProperty(x.NameColumn));
          });
          teachersQuery = sortTeachers;
        }
      }
      int count = await teachersQuery.CountAsync();
      var teachers = await teachersQuery.Skip((ownerParametrs.PageNumber-1)*ownerParametrs.SizePage)
      .Take(ownerParametrs.SizePage)
      .ToListAsync();
      return Ok(new TeacherCollectionDTO(teachers.ConvertAll(x=>new TeacherDTO(x)),count));
    }
    catch(JsonException)
    {
      return BadRequest();
    }
    catch(Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError);
    }
  }
  [Authorize]
  [HttpGet]
  [Route(("GetTeacher/{id}"))]
  public async Task<ActionResult<TeacherDetailsDTO>> GetTeacher(int id)
  {
    try
    {
      var teacher = await _studyDepartmentContext.Teachers.Include(b=>b.IdStatusTeacherNavigation)
        .Include(b=>b.IdTypeEmploymentNavigation).FirstOrDefaultAsync(b=>b.Id == id);
        if(teacher == null)
          return NotFound();
        await Task.Delay(5000);
        return Ok(new TeacherDetailsDTO(teacher));
    }
    catch(Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError);
    }
  }
  private static Expression<Func<Teacher, object>> GetSortProperty(string sortName)
  {
    return sortName?.ToLower() switch
    {
      "familyteacher" => teacher => teacher.Family,
      _=> teacher => teacher.Id
    };
  }
  [HttpDelete]
  [Route("DeleteTeacher/{id}")]
  public async Task<ActionResult> DeleteTeacher(int id)
  {
    try
    {
      var teacher = await _studyDepartmentContext.Teachers.FindAsync(id);
      if(teacher == null)
        return NotFound("Преподаватель с указанным id не найден");
      _studyDepartmentContext.Teachers.Remove(teacher);
      await _studyDepartmentContext.SaveChangesAsync();
      return Ok();
    }
    catch(Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError);
    }
  }
  [HttpGet]
  [Route(("GetTeacherEdit/{id}"))]
  public async Task<ActionResult<TeacherEditDTO>> GetTeacherEdit(int id)
  {
    try
    {
      var teacher = await _studyDepartmentContext.Teachers.Include(b=>b.IdStatusTeacherNavigation)
        .Include(b=>b.IdTypeEmploymentNavigation).FirstOrDefaultAsync(b=>b.Id == id);
      if(teacher == null)
        return NotFound();
      return Ok(new TeacherEditDTO(teacher));
    }
    catch(Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError);
    }
  }
  [HttpPut]
  [Route("UpdateTeacher")]
  public async Task<ActionResult> UpdateTeacher([FromBody]TeacherEditDTO teacherEditDTO)
  {
    try
    {
      Teacher teacher = await _studyDepartmentContext.Teachers.Include(x=>x.IdStatusTeacherNavigation)
        .Include(x=>x.IdTypeEmploymentNavigation).FirstOrDefaultAsync(x=>x.Id == teacherEditDTO.Id);
      if(teacher == null)
        return NotFound("Преподаватель не найден");
      teacher.Family = teacherEditDTO.Family;
      teacher.Name = teacherEditDTO.Name;
      teacher.Patronymic = teacherEditDTO.Patronymic;
      teacher.IdStatusTeacher = teacherEditDTO.IdStatusTeacher;
      teacher.IdTypeEmployment = teacherEditDTO.IdTypeEmployment;
      teacher.Image = teacherEditDTO.Image;
      await _studyDepartmentContext.SaveChangesAsync();
      return Ok();
    }
    catch(Exception e)
    {
      return StatusCode(StatusCodes.Status501NotImplemented);
    }
  }
  [HttpPost]
  [Route("AddTeacher")]
  public async Task<ActionResult> AddTeacher([FromBody]TeacherEditDTO teacherEditDTO)
  {
    try
    {
      Teacher teacher = new Teacher();
      teacher.Family = teacherEditDTO.Family;
      teacher.Name = teacherEditDTO.Name;
      teacher.Patronymic = teacherEditDTO.Patronymic;
      teacher.IdStatusTeacher = teacherEditDTO.IdStatusTeacher;
      teacher.IdTypeEmployment = teacherEditDTO.IdTypeEmployment;
      teacher.Image = teacherEditDTO.Image;
      await _studyDepartmentContext.Teachers.AddAsync(teacher);
      await _studyDepartmentContext.SaveChangesAsync();
      return Ok();
    }
    catch(Exception e)
    {
      return StatusCode(StatusCodes.Status501NotImplemented);
    }
  }
}

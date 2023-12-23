using System.Security.Claims;
using api.Data;
using api.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private StudyDepartmentContext _studyDepartmentContext;
    public UserController(StudyDepartmentContext studyDepartmentContext)
    {
        _studyDepartmentContext = studyDepartmentContext;
    }
    [Authorize]
    [HttpGet("GetInfo")]
    public async Task<ActionResult<UserDTO>> GetInfo()
    {
        string idUser = User.FindFirst(ClaimTypes.Name)?.Value;
        var user = await _studyDepartmentContext.Autorizations.Include(p=>p.IdRoleNavigation).FirstOrDefaultAsync(p=>p.Id == int.Parse(idUser));
        if(user == null)
            NotFound();
        await Task.Delay(5000);
        return Ok(new UserDTO(user));
    }
}

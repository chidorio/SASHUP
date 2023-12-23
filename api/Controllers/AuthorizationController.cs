using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using api.Data;
using api.Models;
using api.Models.DTO;
using api.Settings;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("[controller]")]
public class AutorizationController:ControllerBase
{
    private string GenerateToken(IEnumerable<Claim> claims,DateTime exp)
    {
        var claimsIdentity = new ClaimsIdentity(claims,"Token",ClaimsIdentity.DefaultNameClaimType,ClaimsIdentity.DefaultRoleClaimType);
        var dateNow = DateTime.UtcNow;
        var jwtToken = new JwtSecurityToken(
            issuer: JwtSettings.Study_departmentISServer,
            audience: JwtSettings.Audience,
            notBefore:dateNow,
            claims:claimsIdentity.Claims,
            expires:exp,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key)),SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
    private string GenerateRefreshToken(Autorization user,DateTime exp)
    {
        var claims = new List<Claim>
        {
            new Claim("id",user.Id.ToString()),
        };
        return GenerateToken(claims,exp.ToUniversalTime());
    }
    private string GenereteAccessToken(Autorization user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType,user.Id.ToString()),
            new Claim(ClaimsIdentity.DefaultRoleClaimType,user.IdRoleNavigation.Name)
        };
        return GenerateToken(claims,DateTime.UtcNow.AddMinutes(JwtSettings.LifeTime));
    }
    private string? GetIdFromJWTToken(string jwtToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var validations = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = JwtSettings.Study_departmentISServer,
            ValidAudience = JwtSettings.Audience,
            ValidateLifetime = false
        };
        try
        {
            var claims = handler.ValidateToken(jwtToken,validations,out var securityToken);
            return claims.FindFirstValue("id");
        }
        catch(Exception e)
        {
            return null;
        }
    }
    private const int RefreshTokenExpiryTimeDay = 60;
    private StudyDepartmentContext _studyDepartmentContext;
    public AutorizationController(StudyDepartmentContext studyDepartmentContext)
    {
        _studyDepartmentContext = studyDepartmentContext;
    }
    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<TokenDTO>> Login([FromBody]AuthorizationDTO authorizationDTO)
    {
        if(string.IsNullOrEmpty(authorizationDTO.Login) || string.IsNullOrEmpty(authorizationDTO.Password))
            return BadRequest();
        Autorization? user;
        try
        {
            user = await _studyDepartmentContext.Autorizations.Include(p=>p.IdRoleNavigation).FirstOrDefaultAsync(a=>
            a.Login == authorizationDTO.Login);
        }
        catch(Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        if(user == null)
            return Unauthorized();
        PasswordHasher<object> passwordHasher = new PasswordHasher<object>();
        var result = passwordHasher.VerifyHashedPassword(null,user.Password,authorizationDTO.Password);
        if(result == PasswordVerificationResult.Failed)
            return Unauthorized();
        var newDateExpiry = DateTime.Now;
        newDateExpiry = newDateExpiry.AddDays(RefreshTokenExpiryTimeDay);
        string refreshToken = GenerateRefreshToken(user,newDateExpiry);
        user.RefreshToken = refreshToken;
        user.RefreshTokenExp = newDateExpiry;
        try
        {
            await _studyDepartmentContext.SaveChangesAsync();
        }
        catch(Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        return Ok(new TokenDTO(){AccessToken=GenereteAccessToken(user),RefreshToken=refreshToken});
    }
    [HttpPost]
    [Route("UpdateToken")]
    public async Task<ActionResult<TokenDTO>> UpdateToken([FromHeader(Name = "RefreshToken")]string refreshToken)
    {
        if(string.IsNullOrEmpty(refreshToken))
        {
            ModelState.AddModelError("ErrorMessage","Неверный формат данных");
            return BadRequest(ModelState);
        }
        string? idUser = GetIdFromJWTToken(refreshToken);
        if(string.IsNullOrEmpty(idUser))
        {
            ModelState.AddModelError("ErrorMessage","Неверный RefreshToken");
            return BadRequest(ModelState);
        }
        api.Models.Autorization? user;
        try
        {
            user = await _studyDepartmentContext.Autorizations.Include(p=>p.IdRoleNavigation).FirstOrDefaultAsync(a=>a.
            Id == int.Parse(idUser) && a.RefreshToken.Equals(refreshToken));
        }
        catch(Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        if(user == null)
        {
            ModelState.AddModelError("ErrorMessage","Неверный RefreshToken");
            return BadRequest(ModelState);
        }
        var newDateExpiry = DateTime.Now;
        newDateExpiry = newDateExpiry.AddDays(RefreshTokenExpiryTimeDay);
        string newRefreshToken = GenerateRefreshToken(user,newDateExpiry);
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExp = newDateExpiry;
        try
        {
            await _studyDepartmentContext.SaveChangesAsync();
        }
        catch(Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        return Ok(new TokenDTO(){AccessToken=GenereteAccessToken(user),RefreshToken=newRefreshToken});
    }
    [HttpGet]
    [Route("HashPassword")]
    public async Task<ActionResult<string>> GetHashPassword(string password)
    {
        PasswordHasher<object> passwordHasher = new PasswordHasher<object>();
        return Ok(passwordHasher.HashPassword(null,password));
    }
    [Authorize]
    [HttpPost]
    [Route("Logout")]
    public async Task<ActionResult> Logout()
    {
        string idUser = User.FindFirst(ClaimTypes.Name)?.Value;
        var user = await _studyDepartmentContext.Autorizations.Include(p=>p.IdRoleNavigation).FirstOrDefaultAsync(p=>p.Id == int.Parse(idUser));
        if(user == null)
            NotFound();
        user.RefreshToken = null;
        user.RefreshTokenExp = null;
        try
        {
            _studyDepartmentContext.SaveChanges();
        }
        catch(Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        return Ok();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using desktop.Models;
using Refit;

namespace desktop.Services;

public interface ITeacherRepository
{
  [Get("/Teacher/GetTeachers")]
  public Task<TeachersCollection> GetTeachers([Authorize("Bearer")] string accessToken,[Query]OwnerParameters ownerParameters);
  [Get("/Teacher/GetTeacher/{id}")]
  public Task<TeacherDetails> GetTeacher([Authorize("Bearer")] string accessToken, [AliasAs("id")]int id);
  [Delete("/Teacher/DeleteTeacher/{id}")]
  public Task DeleteTeacher([Authorize("Bearer")] string accessToken, [AliasAs("id")] int id);
  [Get("/Teacher/GetTeacherEdit/{id}")]
  public Task<TeacherEdit> GetTeacherEdit([Authorize("Bearer")] string accessToken, [AliasAs("id")] int id);
  [Put("/Teacher/UpdateTeacher")]
  public Task UpdateTeacher([Authorize("Bearer")] string accessToken, [Body] TeacherEdit teacherEdit);
  [Post("/Teacher/AddTeacher")]
  public Task AddTeacher([Authorize("Bearer")] string accessToken, [Body] TeacherEdit teacherEdit);
}

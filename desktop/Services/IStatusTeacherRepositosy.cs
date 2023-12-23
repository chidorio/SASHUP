using System.Collections.Generic;
using System.Threading.Tasks;
using desktop.Models;
using Refit;

namespace desktop.Services;

public interface IStatusTeacherRepositosy
{
  [Get("/StatusTeacher/GetStatusTeachers")]
  public Task<IEnumerable<StatusTeacher>> GetStatusTeachers();
}

using System.Collections.Generic;
using System.Threading.Tasks;
using desktop.Models;
using Refit;

namespace desktop.Services;

public interface ITypeEmploymentRepository
{
  [Get("/TypeEmployment/GetTypeEmployments")]
  public Task<IEnumerable<TypeEmployment>> GetTypeEmployments();
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using desktop.Models;
using Refit;

namespace desktop;

public interface IStatisticRepository
{
  [Get("/Statistic/GetTeacherStatistics")]
  public Task<IEnumerable<TeacherStatistic>> GetTeacherStatistics([Authorize("Bearer")] string accessToken, [Query] DateRange dateRange);
}

using System;
using System.Threading.Tasks;
using desktop.Models;

namespace desktop.Services;

public interface IReportService
{
  public Task SaveDocument(TeacherStatistic[] teacherStatistics, DateRange dateRange);
}

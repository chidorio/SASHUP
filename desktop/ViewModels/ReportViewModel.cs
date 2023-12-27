using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using desktop.Models;
using desktop.Services;
using desktop.ViewModels;
using ReactiveUI;

namespace desktop.ViewModels;

public class ReportViewModel : ViewModelBase
{
  private readonly IAccessTokenRepository _accessTokenRepository;
  private readonly IStatisticRepository _statisticRepository;

  private readonly IReportService _reportService;

  private readonly IFilePickerService _filePickerService;

  private readonly ObservableAsPropertyHelper<IEnumerable<TeacherStatistic>> _teacherStatistics;

  private readonly ObservableAsPropertyHelper<bool> _isLoading;

  private readonly ObservableAsPropertyHelper<IEnumerable<(string, float)>> _relativeFrequencies;


  private readonly ObservableAsPropertyHelper<IEnumerable<float>> _accumlatedRelativeFrequencies;

  public DateRange DateRange{get;set;}

  public ReactiveCommand<Unit,IEnumerable<TeacherStatistic>> LoadTeacherStatisticsCommand{get;}

  public ReactiveCommand<Unit,Unit> CreateDocReportCommand {get;}
  public IEnumerable<TeacherStatistic> TeacherStatistics => _teacherStatistics.Value;

  public bool IsLoading => _isLoading.Value;

  public IEnumerable<(string, float)> RelativeFrequencies => _relativeFrequencies.Value;

  public IEnumerable<float> AccumlatedRelativeFrequencies => _accumlatedRelativeFrequencies.Value;

  public ReportViewModel(IAccessTokenRepository accessTokenRepository, IUpdateTokenService updateTokenService, INotificationService notificationService,
    IStatisticRepository statisticRepository, IReportService reportService, IFilePickerService filePickerService) : base(notificationService,updateTokenService)
    {
      Title = "Отчет по количеству изменений";
      _accessTokenRepository = accessTokenRepository;
      _statisticRepository = statisticRepository;
      _reportService = reportService;
      _filePickerService = filePickerService;

      DateRange = new DateRange();

      LoadTeacherStatisticsCommand = ReactiveCommand.CreateFromTask<IEnumerable<TeacherStatistic>>(async LoadTeacherStatistics =>
        await _statisticRepository.GetTeacherStatistics(_accessTokenRepository.GetAccessToken(),DateRange));
      LoadTeacherStatisticsCommand.ThrownExceptions.Subscribe(async x=> await CommandExc(x, LoadTeacherStatisticsCommand));
      LoadTeacherStatisticsCommand.IsExecuting.ToProperty(this,x=>x.IsLoading, out _isLoading);
      _teacherStatistics = LoadTeacherStatisticsCommand.ToProperty(this,x=>x.TeacherStatistics);

      

      this.WhenAnyValue(x => x.TeacherStatistics)
        .Where(x=>x!=null)
        .Select(p=>p.ToList().ConvertAll(t => (t.Teacher.FIOTeacher, (float)t.Count / p.Sum(x=>x.Count))))
        .ToProperty(this, x => x.RelativeFrequencies, out _relativeFrequencies);

      this.WhenAnyValue(x => x.RelativeFrequencies)
        .Where(x => x != null)
        .Select(x =>
        {
          var rel = x.Select(x=>x.Item2).ToList();
          var accum = new List<float>();
          if(!rel.Any())
            return accum;
          accum.Add(rel[0]);
          for (int i = 1; i < rel.Count; i++)
            accum.Add(accum [i-1]+ rel[i]);
          return accum;
        })
        .ToProperty(this, x => x.AccumlatedRelativeFrequencies, out _accumlatedRelativeFrequencies);

        DateRange.WhenAnyValue(x => x.DateOne, x => x.DateTwo)
          .Where((x) => x.Item1 != null && x.Item2 != null && x.Item1 <= x.Item2)
          .Subscribe(_=>LoadTeacherStatisticsCommand.Execute().Subscribe());

        LoadTeacherStatisticsCommand.Execute().Subscribe();

      CreateDocReportCommand = ReactiveCommand.CreateFromTask(async =>
      _reportService.SaveDocument(TeacherStatistics.ToArray(), DateRange));
      CreateDocReportCommand.ThrownExceptions.Subscribe(async x => CommandExc(x, CreateDocReportCommand));
    }
}

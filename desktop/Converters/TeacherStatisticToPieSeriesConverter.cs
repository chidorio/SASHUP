using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;
using desktop.Models;
using LiveChartsCore.SkiaSharpView;

namespace desktop.Converters;

public class TeacherStatisticToPieSeriesConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
      IEnumerable<TeacherStatistic> objects = value as IEnumerable<TeacherStatistic>;
      if (objects == null)
        return new PieSeries<int>(){Values = new int []{0}};
      return objects.ToList().ConvertAll(p=> new PieSeries<int>(){
        Values = new int []{p.Count},
        Name = p.Teacher.FIOTeacher
      });
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

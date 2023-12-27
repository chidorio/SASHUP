using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;
using LiveChartsCore.SkiaSharpView;

namespace desktop.Converters;

public class RelativeFrequenciesToColumnSeriesConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
      IEnumerable<(string, float)> teacherStatistics = value as IEnumerable<(string,float)>;
      if(teacherStatistics == null)
        return new ColumnSeries<float>(){Values = new float[]{0}};
      return teacherStatistics.ToList().ConvertAll(p=> new ColumnSeries<float>(){
        Values = new float []{p.Item2},
        Name = p.Item1
      });
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

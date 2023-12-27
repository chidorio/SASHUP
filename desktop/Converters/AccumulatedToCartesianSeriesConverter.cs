using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace desktop.Converters;

public class AccumulatedToCartesianSeriesConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
      IEnumerable<float> teacherStatistics = value as IEnumerable<float>;
      if(teacherStatistics == null)
        return new LineSeries<float>(){Values = new float[]{0}};
      return new ISeries[]
      {
        new LineSeries<float>()
        {
          Values = teacherStatistics,
          Name = "Кумуля"
        }
      };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

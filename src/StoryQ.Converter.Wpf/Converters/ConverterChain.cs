using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace StoryQ.Converter.Wpf.Converters
{
    [ContentProperty("Converters")]
    public class ConverterChain : IValueConverter
    {
        public ConverterChain()
        {
            Converters = new List<IValueConverter>();
        }

        public List<IValueConverter> Converters { get; private set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Converters.Aggregate(value, (v, c) => c.Convert(v, targetType, parameter, culture));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
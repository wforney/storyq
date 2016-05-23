namespace StoryQ.Converter.Wpf.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(bool), typeof(bool))]
    public class InvertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => !System.Convert.ToBoolean(value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => !System.Convert.ToBoolean(value);
    }
}
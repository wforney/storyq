namespace StoryQ.Converter.Wpf.Converters
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Windows.Data;

    [ValueConversion(typeof(string), typeof(string))]
    public class UnCamelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => Regex.Replace("" + value, "(?<!^)[A-Z]", match => " " + match.Value.ToLowerInvariant());

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}
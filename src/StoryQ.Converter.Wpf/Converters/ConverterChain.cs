namespace StoryQ.Converter.Wpf.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;
    using System.Windows.Markup;

    [ContentProperty("Converters")]
    public class ConverterChain : IValueConverter
    {
        public ConverterChain()
        {
            this.Converters = new List<IValueConverter>();
        }

        public List<IValueConverter> Converters { get; private set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => this.Converters.Aggregate(value, (v, c) => c.Convert(v, targetType, parameter, culture));

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}
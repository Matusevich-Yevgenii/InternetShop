using System;
using System.Windows.Data;

namespace InternetShop.Convert
{
    [ValueConversion(typeof (string), typeof (string))]
    public class StringUpperConverter : IValueConverter
    {
        private string _text;

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            _text = value.ToString();
            return _text.ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return _text;
        }
    }
}
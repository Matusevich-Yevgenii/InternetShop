using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace InternetShop.Convert
{
    internal class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return new BitmapImage(
                new Uri(
                    System.IO.Directory.GetCurrentDirectory() + "\\" + (string) value));
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}

namespace EsHeichSample.Forms
{
    using System;
    using System.Globalization;
    using Xamarin.Forms;
    public class HexToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value switch
            {
                string hex => Color.FromHex(hex),
                null => Color.Default,
                _ => throw new ArgumentException("")
            };

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

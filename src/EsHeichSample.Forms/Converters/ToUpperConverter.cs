
namespace EsHeichSample.Forms
{
    using System;
    using System.Globalization;
    using Xamarin.Forms;

    public class ToUpperConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text)
            {
                return text.ToUpper();
            }
            else if (value == null)
            {
                return string.Empty;
            }
            else throw new ArgumentException("value");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

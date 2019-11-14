
namespace EsHeichSample.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using Xamarin.Forms;

    public class StringToResourceImageConverer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string path)
            {
                return ResourceImageExtension.FromResource("Img." + value);
            }
            else if (value == null)
            {
                return (ImageSource)null;
            }
            else
                throw new ArgumentException("value must be string");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

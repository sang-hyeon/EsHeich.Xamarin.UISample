
namespace EsHeichSample.Forms
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using Xamarin.Forms;

    public class ArrayConverter<TTargetType> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<object> datas)
            {
                return datas.Select(x => (TTargetType)value).ToArray();
            }
            else throw new ArgumentNullException("value");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ToCarouselItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable datas)
            {
                return datas.Cast<object>()
                    .Select(x => new ItemsViewItem<object> { BindingItem = x })
                    .ToArray();
            }
            else if (value is null)
            {
                return default;
            }
            else throw new ArgumentNullException("value");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

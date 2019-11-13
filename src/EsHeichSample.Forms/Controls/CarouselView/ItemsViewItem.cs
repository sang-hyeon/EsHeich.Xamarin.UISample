
namespace EsHeichSample.Forms
{
    using System;
    using Xamarin.Forms;

    public class ItemsViewItem<TData> : BindableObject
        where TData : class
    {
        public static readonly BindableProperty VisibleRateProperty =
            BindableProperty.Create(
                nameof(VisibleRate),
                typeof(double),
                typeof(ItemsViewItem<TData>));

        public static readonly BindableProperty BindingItemProperty =
            BindableProperty.Create(
                nameof(BindingItem),
                typeof(TData),
                typeof(ItemsViewItem<TData>));

        public static readonly BindableProperty LocatedOnCenterProperty =
            BindableProperty.Create(
                nameof(LocatedToCenter),
                typeof(bool),
                typeof(ItemsViewItem<TData>));

        public bool LocatedToCenter
        {
            get => (bool)GetValue(LocatedOnCenterProperty);
            set => SetValue(LocatedOnCenterProperty, value);
        }

        public TData BindingItem
        {
            get => (TData)GetValue(BindingItemProperty);
            set => SetValue(BindingItemProperty, value);
        }
        public double VisibleRate
        {
            get => (double)GetValue(VisibleRateProperty);
            set => SetValue(VisibleRateProperty, value);
        }
    }
}

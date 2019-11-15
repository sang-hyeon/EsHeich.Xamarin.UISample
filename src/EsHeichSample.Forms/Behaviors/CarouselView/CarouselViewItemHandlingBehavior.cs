
namespace EsHeichSample.Forms
{
    using System.Runtime.CompilerServices;
    using Xamarin.Forms;

    public class CarouselViewItemHandlingBehavior : CarouselViewScrolledHandler
    {
        protected override void OnAttachedTo(CarouselView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.PropertyChanged += Element_PropertyChanged;
        }
        protected override void OnDetachingFrom(CarouselView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.PropertyChanged -= Element_PropertyChanged;
        }

        private void Element_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.Element.ItemsSource):
                    if (GetItemsSourceCount() > 0)
                    {
                        UpdateItemsSource_ViewState();
                    }
                    break;
            }
        }

        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanging(propertyName);
            switch (propertyName)
            {
                case nameof(this.CenterItemIndex):
                    if (this.Element.ItemsSource is ItemsViewItem<object>[] vItems)
                    {
                        vItems[this.CenterItemIndex].LocatedToCenter = false;
                    }
                    break;
            }
        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName)
            {
                case nameof(this.CenterItemIndex):
                    if (this.Element.ItemsSource is ItemsViewItem<object>[] vItems)
                    {
                        vItems[this.CenterItemIndex].LocatedToCenter = true;
                    }
                    break;
            }
        }

        protected override void Bindable_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            base.Bindable_Scrolled(sender, e);
            UpdateItemsSource_ViewState();
        }

        public int? GetItemsSourceCount()
        {
            if (this.Element?.ItemsSource is ItemsViewItem<object>[] vItems)
            {
                return vItems?.Length;
            }
            else return null;
        }
        protected void UpdateItemsSource_ViewState()
        {
            var items = this.Element.ItemsSource;
            if (items is ItemsViewItem<object>[] vItems)
            {
                double size, offset = 0d;
                if (this.Element.ItemsLayout.Orientation == ItemsLayoutOrientation.Horizontal)
                {
                    size = this.Element.Width;
                    offset = this.HorizontalOffset;
                }
                else
                {
                    size = this.Element.Height;
                    offset = this.VerticalOffset;
                }

                var viewRate = ((offset + size) / size) % 1;
                var prevViewIndex =
                    CenterItemIndex == FirstVisibleItemIndex ?
                        LastVisibleItemIndex : FirstVisibleItemIndex;

                if (viewRate > 0.5d)
                {
                    vItems[CenterItemIndex].VisibleRate = viewRate;
                    if (CenterItemIndex != prevViewIndex)
                        vItems[prevViewIndex].VisibleRate = 1d - viewRate;
                }
                else
                {
                    vItems[CenterItemIndex].VisibleRate = 1d - viewRate;
                    if (CenterItemIndex != prevViewIndex)
                        vItems[prevViewIndex].VisibleRate = viewRate;
                }
            }
        }
    }
}

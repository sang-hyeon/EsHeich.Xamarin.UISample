
namespace EsHeichSample.Forms
{
    using System.Runtime.CompilerServices;
    using Xamarin.Forms;

    public class CarouselViewItemHandlingBehavior : ItemsViewExposingBehavior
    {
        public new CarouselView Element
            => base.Element as CarouselView;

        protected override void OnAttachedTo(ItemsView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.PropertyChanged += Element_PropertyChanged;
        }
        protected override void OnDetachingFrom(ItemsView bindable)
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
                        UpdateViewStateViaItemsSource();
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

        protected override void Element_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            base.Element_Scrolled(sender, e);
            UpdateViewStateViaItemsSource();
        }

        public int? GetItemsSourceCount()
        {
            if (this.Element?.ItemsSource is ItemsViewItem<object>[] vItems)
            {
                return vItems?.Length;
            }
            else return null;
        }

        protected void UpdateViewStateViaItemsSource()
        {
            if (this.Element.ItemsSource is ItemsViewItem<object>[] vItems)
            {
                var (size, offset) = GetCurrentSizeAndOffsetByOrientaion();

                var viewRate = ((offset + size) / size) % 1;
                var prevViewIndex = CenterItemIndex == FirstVisibleItemIndex ?
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

        protected (double, double) GetCurrentSizeAndOffsetByOrientaion()
        {
            if (this.Element.ItemsLayout.Orientation == ItemsLayoutOrientation.Horizontal)
            {
                return (this.Element.Width, this.HorizontalOffset);
            }
            else
            {
                return (this.Element.Height, this.VerticalOffset);
            }
        }
    }
}

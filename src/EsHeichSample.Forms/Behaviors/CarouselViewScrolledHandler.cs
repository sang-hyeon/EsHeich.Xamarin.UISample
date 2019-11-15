
namespace EsHeichSample.Forms
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections;
    using Xamarin.Forms;
    using System.Runtime.CompilerServices;

    public class CarouselViewScrolledHandler : Behavior<CarouselView>
    {
        #region Bindable Properties
        public static readonly BindableProperty HorizontalDeltaProperty =
            BindableProperty.Create(
                nameof(HorizontalDelta),
                typeof(double),
                typeof(CarouselViewScrolledHandler));

        public static readonly BindableProperty VerticalDeltaProperty =
            BindableProperty.Create(
                nameof(VerticalDelta),
                typeof(double),
                typeof(CarouselViewScrolledHandler));

        public static readonly BindableProperty HorizontalOffsetProperty =
            BindableProperty.Create(
                nameof(HorizontalOffset),
                typeof(double),
                typeof(CarouselViewScrolledHandler));

        public static readonly BindableProperty VerticalOffsetProperty =
            BindableProperty.Create(
                nameof(VerticalOffset),
                typeof(double),
                typeof(CarouselViewScrolledHandler));

        public static readonly BindableProperty FirstVisibleItemIndexProperty =
            BindableProperty.Create(
                nameof(FirstVisibleItemIndex),
                typeof(int),
                typeof(CarouselViewScrolledHandler));

        public static readonly BindableProperty CenterItemIndexProperty =
            BindableProperty.Create(
                nameof(CenterItemIndex),
                typeof(int),
                typeof(CarouselViewScrolledHandler));

        public static readonly BindableProperty LastVisibleItemIndexProperty =
            BindableProperty.Create(
                nameof(LastVisibleItemIndex),
                typeof(int),
                typeof(CarouselViewScrolledHandler));

        public int LastVisibleItemIndex
        {
            get => (int)GetValue(LastVisibleItemIndexProperty);
            set => SetValue(LastVisibleItemIndexProperty, value);
        }
        public int CenterItemIndex
        {
            get => (int)GetValue(CenterItemIndexProperty);
            set => SetValue(CenterItemIndexProperty, value);
        }
        public int FirstVisibleItemIndex
        {
            get => (int)GetValue(FirstVisibleItemIndexProperty);
            set => SetValue(FirstVisibleItemIndexProperty, value);
        }
        public double VerticalOffset
        {
            get => (double)GetValue(VerticalOffsetProperty);
            set => SetValue(VerticalOffsetProperty, value);
        }
        public double HorizontalOffset
        {
            get => (double)GetValue(HorizontalOffsetProperty);
            set => SetValue(HorizontalOffsetProperty, value);
        }
        public double VerticalDelta
        {
            get => (double)GetValue(VerticalDeltaProperty);
            set => SetValue(VerticalDeltaProperty, value);
        }
        public double HorizontalDelta
        {
            get => (double)GetValue(HorizontalDeltaProperty);
            set => SetValue(HorizontalDeltaProperty, value);
        }
        #endregion

        public CarouselView Element { get; private set; }
        protected bool busy;

        protected override void OnAttachedTo(CarouselView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Scrolled += Bindable_Scrolled;
            this.Element = bindable;
        }
        protected override void OnDetachingFrom(CarouselView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Scrolled -= Bindable_Scrolled;
            this.Element = null;
        }

        protected virtual void Bindable_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            this.HorizontalDelta = e.HorizontalDelta;
            this.HorizontalOffset = e.HorizontalOffset;
            this.VerticalDelta = e.VerticalDelta;
            this.VerticalOffset = e.VerticalOffset;

            this.CenterItemIndex = e.CenterItemIndex;
            this.FirstVisibleItemIndex = e.FirstVisibleItemIndex;
            this.LastVisibleItemIndex = e.LastVisibleItemIndex;
        }
    }

    public class CarouselViewItemsBehavior : CarouselViewScrolledHandler
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
            switch(e.PropertyName)
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
            switch(propertyName)
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

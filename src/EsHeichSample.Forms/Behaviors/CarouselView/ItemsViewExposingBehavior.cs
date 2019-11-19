
namespace EsHeichSample.Forms
{
    using Xamarin.Forms;

    public class ItemsViewExposingBehavior : Behavior<ItemsView>
    {
        #region Bindable Properties
        public static readonly BindableProperty HorizontalDeltaProperty =
            BindableProperty.Create(
                nameof(HorizontalDelta),
                typeof(double),
                typeof(ItemsViewExposingBehavior));

        public static readonly BindableProperty VerticalDeltaProperty =
            BindableProperty.Create(
                nameof(VerticalDelta),
                typeof(double),
                typeof(ItemsViewExposingBehavior));

        public static readonly BindableProperty HorizontalOffsetProperty =
            BindableProperty.Create(
                nameof(HorizontalOffset),
                typeof(double),
                typeof(ItemsViewExposingBehavior));

        public static readonly BindableProperty VerticalOffsetProperty =
            BindableProperty.Create(
                nameof(VerticalOffset),
                typeof(double),
                typeof(ItemsViewExposingBehavior));

        public static readonly BindableProperty FirstVisibleItemIndexProperty =
            BindableProperty.Create(
                nameof(FirstVisibleItemIndex),
                typeof(int),
                typeof(ItemsViewExposingBehavior));

        public static readonly BindableProperty CenterItemIndexProperty =
            BindableProperty.Create(
                nameof(CenterItemIndex),
                typeof(int),
                typeof(ItemsViewExposingBehavior));

        public static readonly BindableProperty LastVisibleItemIndexProperty =
            BindableProperty.Create(
                nameof(LastVisibleItemIndex),
                typeof(int),
                typeof(ItemsViewExposingBehavior));

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

        public ItemsView Element { get; private set; }
        protected bool busy;

        protected override void OnAttachedTo(ItemsView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Scrolled += Element_Scrolled;
            this.Element = bindable;
        }
        protected override void OnDetachingFrom(ItemsView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Scrolled -= Element_Scrolled;
            this.Element = null;
        }

        protected virtual void Element_Scrolled(object sender, ItemsViewScrolledEventArgs e)
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

}

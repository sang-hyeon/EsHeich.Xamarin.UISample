
namespace EsHeichSample.Forms
{
    using System;
    using System.Runtime.CompilerServices;
    using Xamarin.Forms;

    public class LinearSacleToBehavior<TType> : Behavior<TType>
        where TType : VisualElement
    {
        #region Bindable Property
        /// <summary>
        /// 0~1
        /// </summary>
        public static readonly BindableProperty PercentageProperty =
            BindableProperty.Create(
                nameof(Percentage),
                typeof(double),
                typeof(LinearSacleToBehavior<TType>),
                0d,
                validateValue: ValidateValueDelegate);

        public static readonly BindableProperty MinProperty =
            BindableProperty.Create(
                nameof(Min),
                typeof(double),
                typeof(LinearSacleToBehavior<TType>),
                0d);

        public static readonly BindableProperty MaxProperty =
            BindableProperty.Create(
                nameof(Max),
                typeof(double),
                typeof(LinearSacleToBehavior<TType>),
                10d);


        public static readonly BindableProperty TargetPropertyProperty =
            BindableProperty.Create(
                nameof(TargetProperty),
                typeof(BindableProperty),
                typeof(LinearSacleToBehavior<TType>));

        public static readonly BindableProperty EasingProperty =
            BindableProperty.Create(
                nameof(Easing),
                typeof(Easing),
                typeof(LinearSacleToBehavior<TType>),
                Easing.Linear);

        public Easing Easing
        {
            get => (Easing)GetValue(EasingProperty);
            set => SetValue(EasingProperty, value);
        }
        public BindableProperty TargetProperty
        {
            get => (BindableProperty)GetValue(TargetPropertyProperty);
            set => SetValue(TargetPropertyProperty, value);
        }
        public double Max
        {
            get => (double)GetValue(MaxProperty);
            set => SetValue(MaxProperty, value);
        }
        public double Min
        {
            get => (double)GetValue(MinProperty);
            set => SetValue(MinProperty, value);
        }
        public double Percentage
        {
            get => (double)GetValue(PercentageProperty);
            set => SetValue(PercentageProperty, value);
        }

        public static bool ValidateValueDelegate
            (BindableObject bindable, object value)
        {
            if (value is double percentage)
            {
                if (0 <= percentage & percentage <= 1)
                    return true;
                else return false;
            }
            else
                return false;
        }
        #endregion

        protected TType Element;
        protected override void OnAttachedTo(TType bindable)
        {
            base.OnAttachedTo(bindable);

            this.Element = bindable;
            this.Element.BindingContextChanged += Element_BindingContextChanged;
        }

        protected override void OnDetachingFrom(TType bindable)
        {
            base.OnDetachingFrom(bindable);

            this.Element = null;
            this.BindingContext = null;
            this.Element.BindingContextChanged -= Element_BindingContextChanged;
        }
        private void Element_BindingContextChanged(object sender, EventArgs e)
        {
            this.BindingContext = (sender as VisualElement).BindingContext;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch(propertyName)
            {
                case nameof(Max):
                case nameof(Min):
                case nameof(Percentage):
                    if (Validate())
                        UpdateTargetProperty();
                    break;
            }
        }

        protected void UpdateTargetProperty()
        {
            var easedValue = ConvertCurrentValue();
            this.Element.SetValue(this.TargetProperty, easedValue);
        }

        protected double ConvertCurrentValue()
        {
            var easedValue = this.Easing.Ease(this.Percentage);

            var width = Max - Min;
            if (width > 0)
            {
                return Min + (width * easedValue);
            }
            else 
                throw new IndexOutOfRangeException("Max 는 Min보다 작을 수 없습니다.");
        }

        protected bool Validate()
        {
            if (Max > Min & TargetProperty != null)
                return true;
            else return false;
        }
    }
}

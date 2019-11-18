
namespace EsHeichSample.Forms
{
    using System;
    using System.Runtime.CompilerServices;
    using Xamarin.Forms;
    using EsHeichSample.Client;

    public abstract class ScaleToBehavior<TView, TTargetValue> : BehaviorBase<TView>
        where TView : VisualElement
        where TTargetValue : struct
    {
        #region Bindable Property
        /// <summary>
        /// 0~1
        /// </summary>
        public static readonly BindableProperty PercentageProperty =
            BindableProperty.Create(
                nameof(Percentage),
                typeof(double),
                typeof(ScaleToBehavior<TView, TTargetValue>),
                0d,
                validateValue: ValidateValueDelegate);


        public static readonly BindableProperty StartAtProperty =
            BindableProperty.Create(
                nameof(StartAt),
                typeof(double),
                typeof(ScaleToBehavior<TView,TTargetValue>),
                0d,
                validateValue: ValidateValueDelegate);


        public static readonly BindableProperty EndAtProperty =
            BindableProperty.Create(
                nameof(EndAt),
                typeof(double),
                typeof(ScaleToBehavior<TView,TTargetValue>),
                1d,
                validateValue: ValidateValueDelegate);

        public static readonly BindableProperty FromProperty =
            BindableProperty.Create(
                nameof(From),
                typeof(TTargetValue),
                typeof(ScaleToBehavior<TView, TTargetValue>),
                default);

        public static readonly BindableProperty ToProperty =
            BindableProperty.Create(
                nameof(To),
                typeof(TTargetValue),
                typeof(ScaleToBehavior<TView, TTargetValue>),
                default);


        public static readonly BindableProperty TargetPropertyProperty =
            BindableProperty.Create(
                nameof(TargetProperty),
                typeof(BindableProperty),
                typeof(ScaleToBehavior<TView, TTargetValue>));

        public static readonly BindableProperty EasingProperty =
            BindableProperty.Create(
                nameof(Easing),
                typeof(Easing),
                typeof(ScaleToBehavior<TView, TTargetValue>),
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
        public TTargetValue To
        {
            get => (TTargetValue)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }
        public TTargetValue From
        {
            get => (TTargetValue)GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }
        public double Percentage
        {
            get => (double)GetValue(PercentageProperty);
            set => SetValue(PercentageProperty, value);
        }
        public double EndAt
        {
            get => (double)GetValue(EndAtProperty);
            set => SetValue(EndAtProperty, value);
        }
        public double StartAt
        {
            get => (double)GetValue(StartAtProperty);
            set => SetValue(StartAtProperty, value);
        }

        public static bool ValidateValueDelegate(BindableObject bindable, object value)
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

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName)
            {
                case nameof(To):
                case nameof(From):
                case nameof(Percentage):
                    if (Validate())
                        UpdateTargetProperty();
                    break;
            }
        }
        protected virtual void UpdateTargetProperty()
        {
            var easedValue = ConvertCurrentValue();
            this.AssociatedObject.SetValue(this.TargetProperty, easedValue);
        }
        protected virtual bool Validate()
        {
            if (Percentage.IsInRange(StartAt, EndAt) & TargetProperty != null)
            {
                return true;
            }
            else
                return false;
        }

        protected abstract TTargetValue ConvertCurrentValue();

    }
}

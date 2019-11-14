
namespace EsHeichSample.Forms
{
    using Xamarin.Forms;

    public class LinearSacleToBehavior<TType> : ScaleToBehavior<TType, double>
        where TType : VisualElement
    {

        protected override double ConvertCurrentValue()
        {
            var factor = (Percentage - StartAt) / (EndAt - StartAt);
            if (0 <= factor & factor <= 1)
            {
                var easedValue = this.Easing.Ease(factor);

                var width = To - From;
                return From + (width * easedValue);
            }
            else
                return 0d;
        }
    }
}

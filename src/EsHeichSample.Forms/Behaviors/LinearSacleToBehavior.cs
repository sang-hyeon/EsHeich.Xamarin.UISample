
namespace EsHeichSample.Forms
{
    using Xamarin.Forms;

    public class LinearSacleToBehavior<TType> : ScaleToBehavior<TType, double>
        where TType : VisualElement
    {

        protected override double ConvertCurrentValue()
        {
            var easedValue = this.Easing.Ease(this.Percentage);

            var width = To - From;
            return From + (width * easedValue);
        }
    }
}

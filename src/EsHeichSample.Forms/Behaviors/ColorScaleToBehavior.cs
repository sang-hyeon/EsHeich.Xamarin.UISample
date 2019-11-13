
namespace EsHeichSample.Forms
{
    using Xamarin.Forms;

    public class ColorScaleToBehavior<TType> : ScaleToBehavior<TType, Color>
        where TType : VisualElement
    {
        protected override Color ConvertCurrentValue()
        {
            return ColorTo(this.From, this.To, this.Percentage);
        }

        protected Color ColorTo(Color from, Color to, double percentage)
        {
            return Color.FromRgba(from.R + percentage * (to.R - from.R),
                               from.G + percentage * (to.G - from.G),
                               from.B + percentage * (to.B - from.B),
                               from.A + percentage * (to.A - from.A));
        }
    }
}

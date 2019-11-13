
namespace EsHeichSample.Forms
{
    using System.ComponentModel;
    using Xamarin.Forms;

    [DesignTimeVisible(true)]
    public class ContentControl : ContentView
    {
        #region Bindable Properties

        public static readonly BindableProperty CornerRadiusProperty 
            = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(ContentControl), default(CornerRadius));

        public static readonly BindableProperty HasShadowProperty 
            = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(ContentControl), default(bool));

        public static readonly BindableProperty ElevationProperty 
            = BindableProperty.Create(nameof(Elevation), typeof(int), typeof(ContentControl), 0);

        public static readonly BindableProperty BorderThicknessProperty 
            = BindableProperty.Create(nameof(BorderThickness), typeof(float), typeof(ContentControl), default(float));

        public static readonly BindableProperty BorderIsDashedProperty 
            = BindableProperty.Create(nameof(BorderIsDashed), typeof(bool), typeof(ContentControl), default(bool));

        public static readonly BindableProperty BorderColorProperty 
            = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(ContentControl), default(Color));

        public static readonly BindableProperty BorderDrawingStyleProperty 
            = BindableProperty.Create(nameof(BorderDrawingStyle), typeof(BorderDrawingStyle), typeof(ContentControl), defaultValue: BorderDrawingStyle.Inside);



        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public float BorderThickness
        {
            get { return (float)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        public bool BorderIsDashed
        {
            get { return (bool)GetValue(BorderIsDashedProperty); }
            set { SetValue(BorderIsDashedProperty, value); }
        }

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public bool HasShadow
        {
            get { return (bool)GetValue(HasShadowProperty); }
            set { SetValue(HasShadowProperty, value); }
        }

        public int Elevation
        {
            get { return (int)GetValue(ElevationProperty); }
            set { SetValue(ElevationProperty, value); }
        }

        public BorderDrawingStyle BorderDrawingStyle
        {
            get { return (BorderDrawingStyle)GetValue(BorderDrawingStyleProperty); }
            set { SetValue(BorderDrawingStyleProperty, value); }
        }

        #endregion
    }

    public enum BorderDrawingStyle
    {
        Inside,
        Outside
    }
}
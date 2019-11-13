using System;
using System.Linq;

using Foundation;
using UIKit;
using CoreGraphics;
using CoreAnimation;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using EsHeichSample.Forms;
using System.ComponentModel;

[assembly: ExportRenderer(
    typeof(EsHeichSample.Forms.ContentControl), 
    typeof(EsHeichSample.iOS.Renderers.ContentControlRenderer))]
namespace EsHeichSample.iOS.Renderers
{
    public class ContentControlRenderer : ViewRenderer<ContentControl, UIView>
    {
        private UIView _actualView;
        private UIView _wrapperView;

        private UIColor _colorToRender;
        private CGSize _previousSize;
        private nfloat _topLeft;
        private nfloat _topRight;
        private nfloat _bottomLeft;
        private nfloat _bottomRight;

        public static new void Init()
        {
#pragma warning disable 0219
            var ignore1 = typeof(ContentControlRenderer);
            var ignore2 = typeof(ContentControl);
#pragma warning restore 0219
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ContentControl> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                _actualView = new UIView();
                _wrapperView = new UIView();

                foreach (var item in NativeView.Subviews)
                {
                    _actualView.AddSubview(item);
                }

                _wrapperView.AddSubview(_actualView);

                SetNativeControl(_wrapperView);

                SetBackgroundColor(Element.BackgroundColor);
                SetCornerRadius();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch(e.PropertyName)
            {
                case nameof(VisualElement.BackgroundColor):
                    SetBackgroundColor(Element.BackgroundColor);
                    break;

                case nameof(ContentControl.CornerRadius):
                    SetCornerRadius();
                    break;

                case nameof(ContentControl.IsVisible):
                    SetNeedsDisplay();
                    break;

                default:
                    break;
            }
        }

        public override void LayoutSubviews()
        {
            if (_previousSize != Bounds.Size)
                SetNeedsDisplay();

            base.LayoutSubviews();
        }

        public override void Draw(CGRect rect)
        {
            _actualView.Frame = Bounds;
            _wrapperView.Frame = Bounds;

            DrawBackground();
            DrawShadow();
            DrawBorder();

            base.Draw(rect);

            _previousSize = Bounds.Size;
        }

        private void SetCornerRadius()
        {
            if (Element == null)
                return;

            var elementCornerRadius = (Element as ContentControl).CornerRadius;

            _topLeft = (float)elementCornerRadius.TopLeft;
            _topRight = (float)elementCornerRadius.TopRight;
            _bottomLeft = (float)elementCornerRadius.BottomLeft;
            _bottomRight = (float)elementCornerRadius.BottomRight;

            SetNeedsDisplay();
        }

        protected override void SetBackgroundColor(Color color)
        {
            if (Element == null)
                return;

            var elementColor = Element.BackgroundColor;

            if (!elementColor.IsDefault)
                _colorToRender = elementColor.ToUIColor();
            else
                _colorToRender = color.ToUIColor();

            SetNeedsDisplay();
        }

        private void DrawBackground()
        {
            var element = Element as ContentControl;
            var cornerPath = new UIBezierPath();

            cornerPath.AddArc(new CGPoint((float)Bounds.X + Bounds.Width - _topRight, (float)Bounds.Y + _topRight), _topRight, (float)(Math.PI * 1.5), (float)Math.PI * 2, true);
            cornerPath.AddArc(new CGPoint((float)Bounds.X + Bounds.Width - _bottomRight, (float)Bounds.Y + Bounds.Height - _bottomRight), _bottomRight, 0, (float)(Math.PI * .5), true);
            cornerPath.AddArc(new CGPoint((float)Bounds.X + _bottomLeft, (float)Bounds.Y + Bounds.Height - _bottomLeft), _bottomLeft, (float)(Math.PI * .5), (float)Math.PI, true);
            cornerPath.AddArc(new CGPoint((float)Bounds.X + _topLeft, (float)Bounds.Y + _topLeft), (float)_topLeft, (float)Math.PI, (float)(Math.PI * 1.5), true);

            //draw background
            var maskLayer = new CAShapeLayer
            {
                Frame = Bounds,
                Path = cornerPath.CGPath
            };

            _actualView.Layer.Mask = maskLayer;
            _actualView.Layer.MasksToBounds = true;

            var shapeLayer = new CAShapeLayer
            {
                Frame = Bounds,
                Path = cornerPath.CGPath,
                MasksToBounds = true,
                FillColor = _colorToRender.CGColor
            };

            AddOrRemoveLayer(shapeLayer, 0, _actualView);
        }

        private void DrawBorder()
        {
            var element = Element as ContentControl;

            if (element.BorderThickness > 0)
            {
                var borderLayer = new CAShapeLayer
                {
                    StrokeColor = element.BorderColor == Color.Default ? UIColor.Clear.CGColor : element.BorderColor.ToCGColor(),
                    FillColor = null,
                    LineWidth = element.BorderThickness,
                    Name = "borderLayer"
                };

                var frameBounds = Bounds;
                var insetBounds = element.BorderDrawingStyle == BorderDrawingStyle.Outside ? Bounds.Inset(-(element.BorderThickness / 2), -(element.BorderThickness / 2)) : Bounds.Inset(element.BorderThickness / 2, element.BorderThickness / 2);

                bool hasShadowOrElevation = element.HasShadow || element.Elevation > 0;

                if (!hasShadowOrElevation)
                {
                    borderLayer.Path = CreateCornerPath(element.CornerRadius.TopLeft, element.CornerRadius.TopRight, element.CornerRadius.BottomRight, element.CornerRadius.BottomLeft, insetBounds);
                }
                else
                {
                    borderLayer.Path = CreateCornerPath(element.CornerRadius.TopLeft, element.CornerRadius.TopLeft, element.CornerRadius.TopLeft, element.CornerRadius.TopLeft, insetBounds);
                }

                borderLayer.Frame = frameBounds;
                borderLayer.Position = new CGPoint(frameBounds.Width / 2, frameBounds.Height / 2);

                if (element.BorderIsDashed)
                {
                    borderLayer.LineDashPattern = new NSNumber[] { new NSNumber(6), new NSNumber(3) };
                }

                AddOrRemoveLayer(borderLayer, -1, _wrapperView);
            }
        }

        private void DrawShadow()
        {
            var element = Element;

            bool hasShadowOrElevation = element.HasShadow || element.Elevation > 0;
            nfloat cornerRadius = (nfloat)element.CornerRadius.TopLeft;

            if (element.HasShadow)
            {
                DrawDefaultShadow(_wrapperView.Layer, Bounds, cornerRadius);
            }

            if (element.Elevation > 0)
            {
                DrawElevation(_wrapperView.Layer, element.Elevation, Bounds, cornerRadius);
            }

            if (hasShadowOrElevation)
            {
                _actualView.Layer.CornerRadius = (nfloat)element.CornerRadius.TopLeft;
                _actualView.ClipsToBounds = true;
            }
            else
            {
                _wrapperView.Layer.ShadowOpacity = 0;
            }
            _wrapperView.Layer.RasterizationScale = UIScreen.MainScreen.Scale;
            _wrapperView.Layer.ShouldRasterize = true;

            _actualView.Layer.RasterizationScale = UIScreen.MainScreen.Scale;
            _actualView.Layer.ShouldRasterize = true;
        }

        private void DrawDefaultShadow(CALayer layer, CGRect bounds, nfloat cornerRadius)
        {
            layer.CornerRadius = cornerRadius;
            layer.ShadowRadius = 10;
            layer.ShadowColor = UIColor.Black.CGColor;
            layer.ShadowOpacity = 0.4f;
            layer.ShadowOffset = new System.Drawing.SizeF();
            layer.ShadowPath = UIBezierPath.FromRoundedRect(bounds, cornerRadius).CGPath;
        }

        private void DrawElevation(CALayer layer, int elevation, CGRect bounds, nfloat cornerRadius)
        {
            layer.CornerRadius = cornerRadius;
            layer.ShadowRadius = elevation;
            layer.ShadowColor = UIColor.Black.CGColor;
            layer.ShadowOpacity = 0.24f;
            layer.ShadowOffset = new CGSize(0, elevation);
            layer.ShadowPath = UIBezierPath.FromRoundedRect(bounds, cornerRadius).CGPath;

            layer.MasksToBounds = false;
        }

        private CGPath CreateCornerPath(double topLeft, double topRight, double bottomRight, double bottomLeft, CGRect insetBounds)
        {
            var cornerPath = new CGPath();

            cornerPath.MoveToPoint(new CGPoint(topLeft + insetBounds.X, insetBounds.Y));

            cornerPath.AddLineToPoint(new CGPoint(insetBounds.Width - topRight, insetBounds.Y));
            cornerPath.AddArc((float)(insetBounds.X + insetBounds.Width - topRight), (float)(insetBounds.Y + topRight), (float)topRight, (float)(Math.PI * 1.5), (float)Math.PI * 2, false);

            cornerPath.AddLineToPoint(insetBounds.Width + insetBounds.X, (float)(insetBounds.Height - bottomRight));
            cornerPath.AddArc((float)(insetBounds.X + insetBounds.Width - bottomRight), (float)(insetBounds.Y + insetBounds.Height - bottomRight), (float)bottomRight, 0, (float)(Math.PI * .5), false);

            cornerPath.AddLineToPoint((float)(insetBounds.X + bottomLeft), insetBounds.Height + insetBounds.Y);
            cornerPath.AddArc((float)(insetBounds.X + bottomLeft), (float)(insetBounds.Y + insetBounds.Height - bottomLeft), (float)bottomLeft, (float)(Math.PI * .5), (float)Math.PI, false);

            cornerPath.AddLineToPoint(insetBounds.X, (float)(insetBounds.Y + topLeft));
            cornerPath.AddArc((float)(insetBounds.X + topLeft), (float)(insetBounds.Y + topLeft), (float)topLeft, (float)Math.PI, (float)(Math.PI * 1.5), false);

            return cornerPath;
        }

        public void AddOrRemoveLayer(CALayer layer, int position, UIView viewToAddTo)
        {
            if (viewToAddTo.Layer.Sublayers == null || (viewToAddTo.Layer.Sublayers != null && !viewToAddTo.Layer.Sublayers.Any(x => x.GetType() == layer.GetType())))
            {
                if (position > -1)
                    viewToAddTo.Layer.InsertSublayer(layer, position);
                else
                    viewToAddTo.Layer.AddSublayer(layer);
            }
            else
            {
                var gradLayer = viewToAddTo.Layer.Sublayers.FirstOrDefault(x => x.GetType() == layer.GetType());

                if (gradLayer != null)
                    gradLayer.RemoveFromSuperLayer();

                if (position > -1)
                    viewToAddTo.Layer.InsertSublayer(layer, position);
                else
                    viewToAddTo.Layer.AddSublayer(layer);
            }
        }
    }
}
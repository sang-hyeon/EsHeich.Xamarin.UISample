
namespace EsHeichSample.Forms
{
    using System;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    /// <summary>
    /// 좋은 방법이 아닙니다. 애니메이션을 활용하는 제대로된 계층 구현은
    /// 시간이 많이 걸리는 관계로 하드코딩합니다.
    /// </summary>
    public class HardCodingAnimation : TriggerAction<VisualElement>
    {
        public bool IsIn { get; set; }
        public double Lazy_sec { get; set; } = 0.5;

        protected async override void Invoke(VisualElement sender)
        {
            if (IsIn)
            {
                ViewExtensions.CancelAnimations(sender);
                await Task.Delay(TimeSpan.FromSeconds(Lazy_sec));
                _ = sender.FadeTo(1, 300, Easing.Linear);
                _ = sender.TranslateTo(0, -150, 300, Easing.Linear);
            }
            else
            {
                ViewExtensions.CancelAnimations(sender);
                await Task.Delay(TimeSpan.FromSeconds(Lazy_sec));
                _ = sender.FadeTo(0, 300, Easing.Linear);
                _ = sender.TranslateTo(0, 0, 300, Easing.Linear);
            }
        }
    }
}

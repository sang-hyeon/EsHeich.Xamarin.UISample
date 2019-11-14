
namespace EsHeichSample.Forms
{
    using Autofac;
    using EsHeichSample.Client.ViewModels;

    public class ViewModelLocator
    {
        public HomeViewModel HomeVM
            => App.Container.Resolve<HomeViewModel>();
    }
}

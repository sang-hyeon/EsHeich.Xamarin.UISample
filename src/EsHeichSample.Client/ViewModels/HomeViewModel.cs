
namespace EsHeichSample.Client.ViewModels
{
    using System;
    using EsHeich.RepositoryPattern;

    public class HomeViewModel : ViewModel
    {
        readonly protected IUnitOfWorkFactory factory;

        public HomeViewModel(IUnitOfWorkFactory factory)
        {
            this.factory = factory ??
                throw new ArgumentNullException("factory");
        }

        protected override void Prepare()
        {
        }
    }
}

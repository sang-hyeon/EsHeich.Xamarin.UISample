
namespace EsHeichSample.Client.Datas
{
    using System;
    using EsHeich.RepositoryPattern;
    using EsHeich.RepositoryPattern.EntityFrameworkCore;

    public class ContextFactory : IUnitOfWorkFactory
    {
        readonly protected Func<EsHeichContext> factory;

        public ContextFactory(Func<EsHeichContext> factory)
        {
            this.factory = factory;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            var context = this.factory.Invoke();
            return new UnitOfWork<EsHeichContext>(context);
        }
    }
}

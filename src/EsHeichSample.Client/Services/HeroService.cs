
namespace EsHeichSample.Client.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EsHeichSample.Client.Models;
    using EsHeich.RepositoryPattern;

    public class HeroService : IHeroService
    {
        readonly protected IUnitOfWorkFactory factory;
        public HeroService(IUnitOfWorkFactory factory)
        {
            this.factory = factory ??
                throw new ArgumentNullException("factory");
        }

        public async Task<Hero[]> GetDcHeroesAsync(CancellationToken token = default)
        {
            Hero[] heroes = default;
            using(var worker = this.factory.CreateUnitOfWork())
            {
                var repo = worker.GetRepository<Hero, int>();

                heroes = await repo.GetAllAsync();
            }

            return heroes;
        }
    }
}


namespace EsHeichSample.Client.ViewModels
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EsHeichSample.Client.Services;

    public class HomeViewModel : ViewModel
    {
        readonly protected IHeroService heroService;

        IEnumerable<HeroViewModel> _heroes;

        public IEnumerable<HeroViewModel> Heroes
        {
            get => _heroes;
            set => SetProperty(ref _heroes, value);
        }

        public HomeViewModel(IHeroService heroService) : base()
        {
            this.heroService = heroService ??
                throw new ArgumentNullException("factory");

            Prepare();
        }

        protected async new void Prepare()
        {
            this.Heroes = await this.LoadHerosViaServiceAsync();
        }

        protected async Task<HeroViewModel[]> LoadHerosViaServiceAsync()
        {
            var heroes = await this.heroService.GetDcHeroesAsync();

            return heroes.Select(x => new HeroViewModel(x)).ToArray();
        }
    }
}

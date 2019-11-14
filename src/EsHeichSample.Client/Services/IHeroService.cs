
namespace EsHeichSample.Client.Services
{
    using System.Threading;
    using System.Threading.Tasks;
    using EsHeichSample.Client.Models;

    public interface IHeroService
    {
        Task<Hero[]> GetDcHeroesAsync(CancellationToken toekn = default);
    }
}

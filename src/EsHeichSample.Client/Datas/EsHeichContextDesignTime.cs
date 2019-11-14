
namespace EsHeichSample.Client.Datas
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class PerfectStudyContextDesignTime : IDesignTimeDbContextFactory<EsHeichContext>
    {
        public EsHeichContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EsHeichContext>();
            //builder.UseSqlite($"Data Source=EsHeichStore");

            return new EsHeichContext(builder.Options);
        }
    }
}

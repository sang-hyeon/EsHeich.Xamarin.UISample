
namespace EsHeichSample.Client.Datas
{
    using System.IO;
    using System.Text;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using EsHeichSample.Client.Models;

    public class EsHeichContext : DbContext
    {
        DbSet<Hero> Heroes { get; set; }

        public EsHeichContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Hero>(q =>
            {
                q.HasKey(x => x.ID);
                q.HasData(GetSeedFromResource());
            });
        }


        protected Hero[] GetSeedFromResource()
        {
            var result = new List<string>();
            var resourcePath = "EsHeichSample.Client.Resources.Datas.Hero.txt";
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            var resourceStream = assembly.GetManifestResourceStream(resourcePath);
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                while (reader.EndOfStream == false)
                {
                    result.Add(reader.ReadLine());
                }
            }

            return result.Select(q =>
            {
                var splited = q.Split(',');
                return new Hero
                {
                    HeroName = splited[0],
                    RealName = splited[1],
                    SignatureColor_Hex = splited[5]
                };
            }).ToArray();
        }
    }
}

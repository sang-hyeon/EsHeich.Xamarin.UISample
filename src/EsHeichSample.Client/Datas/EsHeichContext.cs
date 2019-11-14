
namespace EsHeichSample.Client.Datas
{
    using System.IO;
    using System.Text;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using EsHeichSample.Client.Models;
    using System;

    public class EsHeichContext : DbContext
    {
        public DbSet<Hero> Heroes { get; set; }

        public EsHeichContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Hero>(q =>
            {
                q.HasKey(x => x.ID);
                q.Property(x => x.ID).ValueGeneratedOnAdd();
                q.HasData(GetSeedFromResource());
            });
        }

        public void Migrate()
            => this.Database.EnsureCreated();


        protected Hero[] GetSeedFromResource()
        {
            var result = new List<string>();
            var resourcePath = "EsHeichSample.Client.Resources.Datas.Heros.txt";
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            var resourceStream = assembly.GetManifestResourceStream(resourcePath);
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                while (reader.EndOfStream == false)
                {
                    result.Add(reader.ReadLine());
                }
            }

            return result.Select((q,i) =>
            {
                var splited = q.Split(',');
                return new Hero
                {
                    ID = i + 1,
                    HeroName = splited[0],
                    RealName = splited[1],
                    HeroImg = splited[2],
                    Role = Enum.Parse<HeroRole>(splited[3]),
                    SignatureColor_Hex = splited[6],
                    Summary = splited[7],
                    HeroName_ko = splited[8]
                };
            }).ToArray();
        }
    }
}

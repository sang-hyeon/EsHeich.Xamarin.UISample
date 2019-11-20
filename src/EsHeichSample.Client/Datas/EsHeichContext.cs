
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
                q.HasData(this.ReadHerosFromResource());
            });
        }

        /// <summary>
        /// 내부에서 실제 마이그레이션을 진행하지 않습니다.
        /// Xamarin에 대한 샘플 프로젝트인 만큼, Data Layer는 간소화합니다.
        /// </summary>
        public void Migrate()
            => this.Database.EnsureCreated();

    }

    public static class EsHeichContextExtensions
    {
        public static Hero[] ReadHerosFromResource(this EsHeichContext context)
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

            return result.Select((q, i) =>
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

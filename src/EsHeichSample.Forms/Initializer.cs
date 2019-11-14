
namespace EsHeichSample.Forms
{   
    using Microsoft.EntityFrameworkCore;
    using Autofac;
    using EsHeichSample.Client.Services;
    using EsHeichSample.Client.Datas;
    using EsHeichSample.Client.ViewModels;
    using EsHeich.RepositoryPattern;

    internal static class Initializer
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            var context = CreateDatabase();
            context.Migrate();

            builder.Register(x => new ContextFactory(CreateDatabase)).As<IUnitOfWorkFactory>();

            RegisterViewModel(ref builder);
            RegisterService(ref builder);

            return builder.Build();
        }

        public static EsHeichContext CreateDatabase()
        {
            var options = new DbContextOptionsBuilder<EsHeichContext>()
                                .UseInMemoryDatabase(databaseName: "EsHeichMem")
                                .Options;

            return new EsHeichContext(options);            
        }

        public static void RegisterService(ref ContainerBuilder builder)
        {
            builder.RegisterType<HeroService>().As<IHeroService>().SingleInstance();

        }

        public static void RegisterViewModel(ref ContainerBuilder builder)
        {
            builder.RegisterType<HomeViewModel>();
        }
    }
}

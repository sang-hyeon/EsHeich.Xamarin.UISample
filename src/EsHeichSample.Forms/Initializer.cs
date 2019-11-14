
namespace EsHeichSample.Forms
{   
    using Microsoft.EntityFrameworkCore;
    using Autofac;
    using EsHeichSample.Client.Datas;
    using EsHeichSample.Client.ViewModels;
    using EsHeich.RepositoryPattern;

    internal static class Initializer
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.Register(x => new ContextFactory(CreateDatabase)).As<IUnitOfWorkFactory>();

            RegisterViewModel(builder);

            return builder.Build();
        }

        public static EsHeichContext CreateDatabase()
        {
            var options = new DbContextOptionsBuilder<EsHeichContext>()
                                .UseInMemoryDatabase(databaseName: "EsHeichMem")
                                .Options;

            return new EsHeichContext(options);
        }

        public static void RegisterViewModel(ContainerBuilder builder)
        {
            builder.RegisterType<HomeViewModel>();
        }
    }
}

namespace CustomAutomapper.App
{
    using Automapper;
    using Core;
    using Core.Interfaces;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            IServiceProvider services = ConfigureServices();
            IEngine engine = new Engine(services);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<CustomAutomapperContext>(db =>
                db.UseSqlServer(Configuration.ConnectionString));

            serviceCollection.AddTransient<ICommandInterpreter, CommandInterpreter>();
            serviceCollection.AddTransient<Mapper>();

            ServiceProvider provider = serviceCollection.BuildServiceProvider();

            return provider;
        }
    }
}

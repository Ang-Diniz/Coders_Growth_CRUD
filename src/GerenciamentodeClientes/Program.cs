using Dominio;
using FluentMigrator.Runner;
using FluentValidation;
using Infraestrutura;
using LinqToDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GerenciamentodeClientes
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var serviceProvider = CreateServices())
            using (var scope = serviceProvider.CreateScope())
            
            {
                UpdateDatabase(scope.ServiceProvider);
            }

            var builder = CriarHostBuilder();
            var servicesProvider = builder.Build().Services;
            var validacao = servicesProvider.GetService<IValidator<Cliente>>();

            var builder1 = CriarHostBuilder();
            var servicesProvider1 = builder1.Build().Services;
            var repositorio = servicesProvider1.GetService<ICliente>();

            var builder2 = CriarHostBuilder();
            var servicesProvider2 = builder2.Build().Services;
            var repositorioLinq2Db = servicesProvider2.GetService<ICliente>();


            ApplicationConfiguration.Initialize();
            Application.Run(new TelaInicial(repositorio, validacao, repositorioLinq2Db));
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }

        private static ServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(RepositorioClienteBancoDeDados.connectionString)
                .ScanIn(typeof(AddClienteTable).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        public void configureService(IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<Cliente>();
        }

        static IHostBuilder CriarHostBuilder()
        {
            return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => {
                services.AddScoped<IValidator<Cliente>, ClienteFluentValidation>();
                services.AddScoped<ICliente, RepositorioClienteBancoDeDados>();
                services.AddScoped<ICliente, RepositorioClienteLinq2DB>();
            });
        }
    }
}
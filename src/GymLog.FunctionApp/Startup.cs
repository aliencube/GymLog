using Azure.Data.Tables;

using GymLog.FunctionApp.Configurations;

using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(GymLog.FunctionApp.Startup))]
namespace GymLog.FunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            this.ConfigureAppSettings(builder.Services);
            this.ConfigureClients(builder.Services);
        }

        private void ConfigureAppSettings(IServiceCollection services)
        {
            services.AddSingleton<AppSettings>();
        }

        private void ConfigureClients(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var settings = provider.GetService<AppSettings>();

            var tableServiceClient = new TableServiceClient(settings.GymLog.StorageAccount.ConnectionString);
            services.AddSingleton<TableServiceClient>(tableServiceClient);

            var cosmosClient = new CosmosClientBuilder(settings.GymLog.CosmosDB.ConnectionString)
                                   .Build();
            services.AddSingleton<CosmosClient>(cosmosClient);
        }
    }
}

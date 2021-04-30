using System;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;

namespace GymLog.FunctionApp.Configurations
{
    public class AppSettings
    {
        private const string StorageAccountKey = "AzureWebJobsStorage";
        private const string ServiceBusKey = "AzureWebJobsServiceBus";
        private const string CosmosDBKey = "CosmosDbConnection";
        private const string AppInsightsKey = "APPLICATIONINSIGHTS_CONNECTION_STRING";
        private const string GymLogKey = "GymLog";

        public virtual GymLogSettings GymLog { get; } = new GymLogSettings();
        public virtual OpenApiSettings OpenApi { get; } = new OpenApiSettings();
    }

    public class GymLogSettings
    {
        public virtual StorageAccountSettings StorageAccount { get; } = new StorageAccountSettings();
        public virtual ServiceBusSettings ServiceBus { get; } = new ServiceBusSettings();
        public virtual CosmosDBSettings CosmosDB { get; } = new CosmosDBSettings();
        public virtual ApplicationInsightsSettings ApplicationInsights { get; } = new ApplicationInsightsSettings();
    }

    public class StorageAccountSettings
    {
        private const string StorageAccountKey = "AzureWebJobsStorage";

        public virtual string ConnectionString { get; set; } = Environment.GetEnvironmentVariable(StorageAccountKey);

        public virtual TableStorageSettings Table { get; set; } = new TableStorageSettings();
    }

    public class TableStorageSettings
    {
        private const string GymLogTableKey = "GymLog__StorageAccount__Table__TableName";

        public virtual string TableName { get; set; } = Environment.GetEnvironmentVariable(GymLogTableKey);
    }

    public class ServiceBusSettings
    {
        private const string ServiceBusKey = "AzureWebJobsServiceBus";
        private const string GymLogTopicKey = "GymLog__ServiceBus__TopicName";
        private const string GymLogSubscriptionKey = "GymLog__ServiceBus__SubscriptionName";

        public virtual string ConnectionString { get; set; } = Environment.GetEnvironmentVariable(ServiceBusKey);

        public virtual string TopicName { get; set; } = Environment.GetEnvironmentVariable(GymLogTopicKey);

        public virtual string SubscriptionName { get; set; } = Environment.GetEnvironmentVariable(GymLogSubscriptionKey);
    }

    public class CosmosDBSettings
    {
        private const string CosmosDBKey = "CosmosDbConnection";
        private const string GymLogDatabaseNameKey = "GymLog__CosmosDB__DatabaseName";
        private const string GymLogContainerKey = "GymLog__CosmosDB__ContainerName";
        private const string GymLogPartitionKeyPathKey = "GymLog__CosmosDB__PartitionKeyPath";

        public virtual string ConnectionString { get; set; } = Environment.GetEnvironmentVariable(CosmosDBKey);

        public virtual string DatabaseName { get; set; } = Environment.GetEnvironmentVariable(GymLogDatabaseNameKey);

        public virtual string ContainerName { get; set; } = Environment.GetEnvironmentVariable(GymLogContainerKey);

        public virtual string PartitionKeyPath { get; set; } = Environment.GetEnvironmentVariable(GymLogPartitionKeyPathKey);
    }

    public class ApplicationInsightsSettings
    {
        private const string AppInsightsKey = "APPLICATIONINSIGHTS_CONNECTION_STRING";

        public virtual string ConnectionString { get; set; } = Environment.GetEnvironmentVariable(AppInsightsKey);
    }

    public class OpenApiSettings
    {
        private const string OpenApiVersionKey = "OpenApi__Version";
        private const string OpenApiDocVersionKey = "OpenApi__DocVersion";

        public virtual OpenApiVersionType Version { get; set; } = Enum.TryParse<OpenApiVersionType>(
                                                                      Environment.GetEnvironmentVariable(OpenApiVersionKey), ignoreCase: true, out var result)
                                                                    ? result
                                                                    : OpenApiVersionType.V2;
        public virtual string DocumentVersion { get; set; } = Environment.GetEnvironmentVariable(OpenApiDocVersionKey);
    }
}

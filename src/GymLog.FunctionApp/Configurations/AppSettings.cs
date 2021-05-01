using System;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;

namespace GymLog.FunctionApp.Configurations
{
    /// <summary>
    /// This represents the app settings entity.
    /// </summary>
    public class AppSettings
    {
        private const string StorageAccountKey = "AzureWebJobsStorage";
        private const string ServiceBusKey = "AzureWebJobsServiceBus";
        private const string CosmosDBKey = "CosmosDbConnection";
        private const string AppInsightsKey = "APPLICATIONINSIGHTS_CONNECTION_STRING";
        private const string GymLogKey = "GymLog";

        /// <summary>
        /// Gets the <see cref="GymLogSettings"/> object.
        /// </summary>
        public virtual GymLogSettings GymLog { get; } = new GymLogSettings();

        /// <summary>
        /// Gets the <see cref="OpenApiSettings"/> object.
        /// </summary>
        public virtual OpenApiSettings OpenApi { get; } = new OpenApiSettings();
    }

    /// <summary>
    /// This represents the app settings entity for GymLog.
    /// </summary>
    public class GymLogSettings
    {
        /// <summary>
        /// Gets the <see cref="StorageAccountSettings"/> object.
        /// </summary>
        public virtual StorageAccountSettings StorageAccount { get; } = new StorageAccountSettings();

        /// <summary>
        /// Gets the <see cref="ServiceBusSettings"/> object.
        /// </summary>
        public virtual ServiceBusSettings ServiceBus { get; } = new ServiceBusSettings();

        /// <summary>
        /// Gets the <see cref="CosmosDBSettings"/> object.
        /// </summary>
        public virtual CosmosDBSettings CosmosDB { get; } = new CosmosDBSettings();

        /// <summary>
        /// Gets the <see cref="ApplicationInsightsSettings"/> object.
        /// </summary>
        public virtual ApplicationInsightsSettings ApplicationInsights { get; } = new ApplicationInsightsSettings();
    }

    /// <summary>
    /// This represents the app settings entity for Azure Storage Account.
    /// </summary>
    public class StorageAccountSettings
    {
        private const string StorageAccountKey = "AzureWebJobsStorage";

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        public virtual string ConnectionString { get; set; } = Environment.GetEnvironmentVariable(StorageAccountKey);

        /// <summary>
        /// Gets the <see cref="TableStorageSettings"/> object.
        /// </summary>
        public virtual TableStorageSettings Table { get; set; } = new TableStorageSettings();
    }

    /// <summary>
    /// This represents the app settings entity for Azure Table Storage.
    /// </summary>
    public class TableStorageSettings
    {
        private const string GymLogTableKey = "GymLog__StorageAccount__Table__TableName";

        /// <summary>
        /// Gets the table name.
        /// </summary>
        public virtual string TableName { get; set; } = Environment.GetEnvironmentVariable(GymLogTableKey);
    }

    /// <summary>
    /// This represents the app settings entity for Azure Service Bus.
    /// </summary>
    public class ServiceBusSettings
    {
        private const string ServiceBusKey = "AzureWebJobsServiceBus";
        private const string GymLogTopicKey = "GymLog__ServiceBus__TopicName";
        private const string GymLogSubscriptionKey = "GymLog__ServiceBus__SubscriptionName";

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        public virtual string ConnectionString { get; set; } = Environment.GetEnvironmentVariable(ServiceBusKey);

        /// <summary>
        /// Gets the topic name.
        /// </summary>
        public virtual string TopicName { get; set; } = Environment.GetEnvironmentVariable(GymLogTopicKey);

        /// <summary>
        /// Gets the subscription name.
        /// </summary>
        public virtual string SubscriptionName { get; set; } = Environment.GetEnvironmentVariable(GymLogSubscriptionKey);
    }

    /// <summary>
    /// This represents the app settings entity for Azure Cosmos DB.
    /// </summary>
    public class CosmosDBSettings
    {
        private const string CosmosDBKey = "CosmosDbConnection";
        private const string GymLogDatabaseNameKey = "GymLog__CosmosDB__DatabaseName";
        private const string GymLogContainerKey = "GymLog__CosmosDB__ContainerName";
        private const string GymLogPartitionKeyPathKey = "GymLog__CosmosDB__PartitionKeyPath";

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        public virtual string ConnectionString { get; set; } = Environment.GetEnvironmentVariable(CosmosDBKey);

        /// <summary>
        /// Gets the database name.
        /// </summary>
        public virtual string DatabaseName { get; set; } = Environment.GetEnvironmentVariable(GymLogDatabaseNameKey);

        /// <summary>
        /// Gets the container name.
        /// </summary>
        public virtual string ContainerName { get; set; } = Environment.GetEnvironmentVariable(GymLogContainerKey);

        /// <summary>
        /// Gets the partition key path.
        /// </summary>
        public virtual string PartitionKeyPath { get; set; } = Environment.GetEnvironmentVariable(GymLogPartitionKeyPathKey);
    }

    /// <summary>
    /// This represents the app settings entity for Azure Application Insights.
    /// </summary>
    public class ApplicationInsightsSettings
    {
        private const string AppInsightsKey = "APPLICATIONINSIGHTS_CONNECTION_STRING";

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        public virtual string ConnectionString { get; set; } = Environment.GetEnvironmentVariable(AppInsightsKey);
    }

    /// <summary>
    /// This represents the app settings entity for OpenAPI.
    /// </summary>
    public class OpenApiSettings
    {
        private const string OpenApiVersionKey = "OpenApi__Version";
        private const string OpenApiDocVersionKey = "OpenApi__DocVersion";

        /// <summary>
        /// Gets the <see cref="OpenApiVersionType"/> value.
        /// </summary>
        public virtual OpenApiVersionType Version { get; set; } = Enum.TryParse<OpenApiVersionType>(
                                                                      Environment.GetEnvironmentVariable(OpenApiVersionKey), ignoreCase: true, out var result)
                                                                    ? result
                                                                    : OpenApiVersionType.V2;
        /// <summary>
        /// Gets the OpenAPI document version.
        /// </summary>
        public virtual string DocumentVersion { get; set; } = Environment.GetEnvironmentVariable(OpenApiDocVersionKey);
    }
}

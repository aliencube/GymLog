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
        private const string OpenApiKey = "OpenApi";
        private const string ForceErrorKey = "ForceError";

        /// <summary>
        /// Gets the <see cref="GymLogSettings"/> object.
        /// </summary>
        public virtual GymLogSettings GymLog { get; } = new GymLogSettings();

        /// <summary>
        /// Gets the <see cref="OpenApiSettings"/> object.
        /// </summary>
        public virtual OpenApiSettings OpenApi { get; } = new OpenApiSettings();

        /// <summary>
        /// Gets the <see cref="ForceErrorSettings"/> object.
        /// </summary>
        public virtual ForceErrorSettings ForceError { get; } = new ForceErrorSettings();
    }

    /// <summary>
    /// This represents the app settings entity for GymLog.
    /// </summary>
    public class GymLogSettings
    {
        /// <summary>
        /// Gets or sets the <see cref="StorageAccountSettings"/> object.
        /// </summary>
        public virtual StorageAccountSettings StorageAccount { get; set; } = new StorageAccountSettings();

        /// <summary>
        /// Gets or sets the <see cref="ServiceBusSettings"/> object.
        /// </summary>
        public virtual ServiceBusSettings ServiceBus { get; set; } = new ServiceBusSettings();

        /// <summary>
        /// Gets or sets the <see cref="CosmosDBSettings"/> object.
        /// </summary>
        public virtual CosmosDBSettings CosmosDB { get; set; } = new CosmosDBSettings();

        /// <summary>
        /// Gets or sets the <see cref="ApplicationInsightsSettings"/> object.
        /// </summary>
        public virtual ApplicationInsightsSettings ApplicationInsights { get; set; } = new ApplicationInsightsSettings();
    }

    /// <summary>
    /// This represents the app settings entity for Azure Storage Account.
    /// </summary>
    public class StorageAccountSettings
    {
        private const string StorageAccountKey = "AzureWebJobsStorage";

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public virtual string ConnectionString { get; set; } = Environment.GetEnvironmentVariable(StorageAccountKey);

        /// <summary>
        /// Gets or sets the <see cref="TableStorageSettings"/> object.
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
        /// Gets or sets the table name.
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
        /// Gets or sets the connection string.
        /// </summary>
        public virtual string ConnectionString { get; set; } = Environment.GetEnvironmentVariable(ServiceBusKey);

        /// <summary>
        /// Gets or sets the topic name.
        /// </summary>
        public virtual string TopicName { get; set; } = Environment.GetEnvironmentVariable(GymLogTopicKey);

        /// <summary>
        /// Gets or sets the subscription name.
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
        /// Gets or sets the connection string.
        /// </summary>
        public virtual string ConnectionString { get; set; } = Environment.GetEnvironmentVariable(CosmosDBKey);

        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        public virtual string DatabaseName { get; set; } = Environment.GetEnvironmentVariable(GymLogDatabaseNameKey);

        /// <summary>
        /// Gets or sets the container name.
        /// </summary>
        public virtual string ContainerName { get; set; } = Environment.GetEnvironmentVariable(GymLogContainerKey);

        /// <summary>
        /// Gets or sets the partition key path.
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
        /// Gets or sets the connection string.
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
        /// Gets or sets the <see cref="OpenApiVersionType"/> value.
        /// </summary>
        public virtual OpenApiVersionType Version { get; set; } = Enum.TryParse<OpenApiVersionType>(
                                                                      Environment.GetEnvironmentVariable(OpenApiVersionKey), ignoreCase: true, out var result)
                                                                    ? result
                                                                    : OpenApiVersionType.V2;
        /// <summary>
        /// Gets or sets the OpenAPI document version.
        /// </summary>
        public virtual string DocumentVersion { get; set; } = Environment.GetEnvironmentVariable(OpenApiDocVersionKey);
    }

    /// <summary>
    /// This represents the app settings entity for error enforcement.
    /// </summary>
    public class ForceErrorSettings
    {
        /// <summary>
        /// Gets or sets the error enforcement settings from the publisher.
        /// </summary>
        public virtual ForceErrorPublisherSettings Publisher { get; set; } = new ForceErrorPublisherSettings();

        /// <summary>
        /// Gets or sets the error enforcement settings from the subscriber.
        /// </summary>
        public virtual ForceErrorSubscriberSettings Subscriber { get; set; } = new ForceErrorSubscriberSettings();
    }

    /// <summary>
    /// This represents the app settings entity for error enforcement on the publisher.
    /// </summary>
    public class ForceErrorPublisherSettings
    {
        private const string ForceErrorPublisherRoutineKey = "ForceError__Publisher__Routine";
        private const string ForceErrorPublisherExerciseKey = "ForceError__Publisher__Exercise";
        private const string ForceErrorPublisherPublishKey = "ForceError__Publisher__Publish";

        /// <summary>
        /// Gets or sets the value indicating whether to enforce an error on the routine action at publisher.
        /// </summary>
        public virtual bool Routine { get; set; } = bool.TryParse(
                                                        Environment.GetEnvironmentVariable(ForceErrorPublisherRoutineKey), out var result)
                                                      ? result
                                                      : false;

        /// <summary>
        /// Gets or sets the value indicating whether to enforce an error on the exercise action at publisher.
        /// </summary>
        public virtual bool Exercise { get; set; } = bool.TryParse(
                                                         Environment.GetEnvironmentVariable(ForceErrorPublisherExerciseKey), out var result)
                                                       ? result
                                                       : false;

        /// <summary>
        /// Gets or sets the value indicating whether to enforce an error on the record/publish action at publisher.
        /// </summary>
        public virtual bool Publish { get; set; } = bool.TryParse(
                                                        Environment.GetEnvironmentVariable(ForceErrorPublisherPublishKey), out var result)
                                                      ? result
                                                      : false;
    }

    /// <summary>
    /// This represents the app settings entity for error enforcement on the subscriber.
    /// </summary>
    public class ForceErrorSubscriberSettings
    {
        private const string ForceErrorSubscriberIngestKey = "ForceError__Subscriber__Ingest";

        /// <summary>
        /// Gets or sets the value indicating whether to enforce an error on the ingest action at subscriber.
        /// </summary>
        public virtual bool Ingest { get; set; } = bool.TryParse(
                                                       Environment.GetEnvironmentVariable(ForceErrorSubscriberIngestKey), out var result)
                                                     ? result
                                                     : false;
    }
}

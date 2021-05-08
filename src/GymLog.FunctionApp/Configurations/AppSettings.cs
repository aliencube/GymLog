using System;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;

namespace GymLog.FunctionApp.Configurations
{
    /// <summary>
    /// This represents the app settings entity.
    /// </summary>
    public class AppSettings
    {
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
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public virtual string ConnectionString { get; set; } = Environment.GetEnvironmentVariable(AppSettingsKeys.StorageAccountKey) ?? string.Empty;

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
        /// <summary>
        /// Gets or sets the table name.
        /// </summary>
        public virtual string TableName { get; set; } = Environment.GetEnvironmentVariable(AppSettingsKeys.GymLogTableKey) ?? string.Empty;
    }

    /// <summary>
    /// This represents the app settings entity for Azure Service Bus.
    /// </summary>
    public class ServiceBusSettings
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public virtual string ConnectionString { get; set; } = Environment.GetEnvironmentVariable(AppSettingsKeys.ServiceBusKey) ?? string.Empty;

        /// <summary>
        /// Gets or sets the topic name.
        /// </summary>
        public virtual string TopicName { get; set; } = Environment.GetEnvironmentVariable(AppSettingsKeys.GymLogTopicKey) ?? string.Empty;

        /// <summary>
        /// Gets or sets the subscription name.
        /// </summary>
        public virtual string SubscriptionName { get; set; } = Environment.GetEnvironmentVariable(AppSettingsKeys.GymLogSubscriptionKey) ?? string.Empty;
    }

    /// <summary>
    /// This represents the app settings entity for Azure Cosmos DB.
    /// </summary>
    public class CosmosDBSettings
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public virtual string ConnectionString { get; set; } = Environment.GetEnvironmentVariable(AppSettingsKeys.CosmosDBKey) ?? string.Empty;

        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        public virtual string DatabaseName { get; set; } = Environment.GetEnvironmentVariable(AppSettingsKeys.GymLogDatabaseNameKey) ?? string.Empty;

        /// <summary>
        /// Gets or sets the container name.
        /// </summary>
        public virtual string ContainerName { get; set; } = Environment.GetEnvironmentVariable(AppSettingsKeys.GymLogContainerKey) ?? string.Empty;

        /// <summary>
        /// Gets or sets the partition key path.
        /// </summary>
        public virtual string PartitionKeyPath { get; set; } = Environment.GetEnvironmentVariable(AppSettingsKeys.GymLogPartitionKeyPathKey) ?? string.Empty;
    }

    /// <summary>
    /// This represents the app settings entity for Azure Application Insights.
    /// </summary>
    public class ApplicationInsightsSettings
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public virtual string ConnectionString { get; set; } = Environment.GetEnvironmentVariable(AppSettingsKeys.AppInsightsKey) ?? string.Empty;
    }

    /// <summary>
    /// This represents the app settings entity for OpenAPI.
    /// </summary>
    public class OpenApiSettings
    {
        /// <summary>
        /// Gets or sets the <see cref="OpenApiVersionType"/> value.
        /// </summary>
        public virtual OpenApiVersionType Version { get; set; } = Enum.TryParse<OpenApiVersionType>(
                                                                       Environment.GetEnvironmentVariable(AppSettingsKeys.OpenApiVersionKey), ignoreCase: true, out var result)
                                                                     ? result
                                                                     : OpenApiConfigurationOptions.DefaultVersion();
        /// <summary>
        /// Gets or sets the OpenAPI document version.
        /// </summary>
        public virtual string DocumentVersion { get; set; } = Environment.GetEnvironmentVariable(AppSettingsKeys.OpenApiDocVersionKey) ?? OpenApiConfigurationOptions.DefaultDocVersion();

        /// <summary>
        /// Gets or sets the OpenAPI document title.
        /// </summary>
        public virtual string DocumentTitle { get; set; } = Environment.GetEnvironmentVariable(AppSettingsKeys.OpenApiDocTitleKey) ?? OpenApiConfigurationOptions.DefaultDocTitle(typeof(AppSettings));
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
        /// <summary>
        /// Gets or sets the value indicating whether to enforce an error on the routine action at publisher.
        /// </summary>
        public virtual bool Routine { get; set; } = bool.TryParse(
                                                         Environment.GetEnvironmentVariable(AppSettingsKeys.ForceErrorPublisherRoutineKey), out var result)
                                                       ? result
                                                       : false;

        /// <summary>
        /// Gets or sets the value indicating whether to enforce an error on the exercise action at publisher.
        /// </summary>
        public virtual bool Exercise { get; set; } = bool.TryParse(
                                                          Environment.GetEnvironmentVariable(AppSettingsKeys.ForceErrorPublisherExerciseKey), out var result)
                                                        ? result
                                                        : false;

        /// <summary>
        /// Gets or sets the value indicating whether to enforce an error on the record/publish action at publisher.
        /// </summary>
        public virtual bool Publish { get; set; } = bool.TryParse(
                                                         Environment.GetEnvironmentVariable(AppSettingsKeys.ForceErrorPublisherPublishKey), out var result)
                                                       ? result
                                                       : false;
    }

    /// <summary>
    /// This represents the app settings entity for error enforcement on the subscriber.
    /// </summary>
    public class ForceErrorSubscriberSettings
    {
        /// <summary>
        /// Gets or sets the value indicating whether to enforce an error on the ingest action at subscriber.
        /// </summary>
        public virtual bool Ingest { get; set; } = bool.TryParse(
                                                        Environment.GetEnvironmentVariable(AppSettingsKeys.ForceErrorSubscriberIngestKey), out var result)
                                                      ? result
                                                      : false;
    }
}

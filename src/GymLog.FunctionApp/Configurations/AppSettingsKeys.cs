namespace GymLog.FunctionApp.Configurations
{
    /// <summary>
    /// This represents the constants entity for app settings.
    /// </summary>
    public static class AppSettingsKeys
    {
        /// <summary>
        /// Identifies Azure Storage Account connection string key.
        /// </summary>
        public const string StorageAccountKey = "AzureWebJobsStorage";

        /// <summary>
        /// Identifies Azure Service Bus connection string key.
        /// </summary>
        public const string ServiceBusKey = "AzureWebJobsServiceBus";

        /// <summary>
        /// Identifies Azure Cosmos DB connectionstring key.
        /// </summary>
        public const string CosmosDBKey = "CosmosDbConnection";

        /// <summary>
        /// Identifies Azure Application Insights connectionstring key.
        /// </summary>
        public const string AppInsightsKey = "APPLICATIONINSIGHTS_CONNECTION_STRING";

        /// <summary>
        /// Identifies the GymLog key.
        /// </summary>
        public const string GymLogKey = "GymLog";

        /// <summary>
        /// Identifies the table name key of the Azure Table Storage.
        /// </summary>
        public const string GymLogTableKey = "GymLog__StorageAccount__Table__TableName";

        /// <summary>
        /// Identifies the topic name key of the Azure Service Bus.
        /// </summary>
        public const string GymLogTopicKey = "GymLog__ServiceBus__TopicName";

        /// <summary>
        /// Identifies the subscription name key of the Azure Service Bus.
        /// </summary>
        public const string GymLogSubscriptionKey = "GymLog__ServiceBus__SubscriptionName";

        /// <summary>
        /// Identifies the database name key of the Azure Cosmos DB.
        /// </summary>
        public const string GymLogDatabaseNameKey = "GymLog__CosmosDB__DatabaseName";

        /// <summary>
        /// Identifies the container name key of the Azure Cosmos DB.
        /// </summary>
        public const string GymLogContainerKey = "GymLog__CosmosDB__ContainerName";

        /// <summary>
        /// Identifies the partition key path key of the Azure Cosmos DB.
        /// </summary>
        public const string GymLogPartitionKeyPathKey = "GymLog__CosmosDB__PartitionKeyPath";

        /// <summary>
        /// Identifies the OpenApi key.
        /// </summary>
        public const string OpenApiKey = "OpenApi";

        /// <summary>
        /// Identifies the OpenAPI version key.
        /// </summary>
        public const string OpenApiVersionKey = "OpenApi__Version";

        /// <summary>
        /// Identifies the OpenAPI document version key.
        /// </summary>
        public const string OpenApiDocVersionKey = "OpenApi__DocVersion";

        /// <summary>
        /// Identifies the OpenAPI document title key.
        /// </summary>
        public const string OpenApiDocTitleKey = "OpenApi__DocTitle";

        /// <summary>
        /// Identifies the ForceError key.
        /// </summary>
        public const string ForceErrorKey = "ForceError";

        /// <summary>
        /// Identifies the error enforcement key for the publisher routine.
        /// </summary>
        public const string ForceErrorPublisherRoutineKey = "ForceError__Publisher__Routine";

        /// <summary>
        /// Identifies the error enforcement key for the publisher exercise.
        /// </summary>
        public const string ForceErrorPublisherExerciseKey = "ForceError__Publisher__Exercise";

        /// <summary>
        /// Identifies the error enforcement key for the publisher publish.
        /// </summary>
        public const string ForceErrorPublisherPublishKey = "ForceError__Publisher__Publish";

        /// <summary>
        /// Identifies the error enforcement key for the subscriber ingest.
        /// </summary>
        public const string ForceErrorSubscriberIngestKey = "ForceError__Subscriber__Ingest";
    }
}

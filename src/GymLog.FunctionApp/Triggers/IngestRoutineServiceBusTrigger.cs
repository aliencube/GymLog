using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Azure.Messaging.ServiceBus;

using GymLog.FunctionApp.Configurations;
using GymLog.FunctionApp.Exceptions;
using GymLog.FunctionApp.Extensions;
using GymLog.FunctionApp.Models;
using GymLog.FunctionApp.Traces;

using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace GymLog.FunctionApp.Triggers
{
    /// <summary>
    /// This represents the Service Bus trigger entity to process records.
    /// </summary>
    public class IngestRoutineServiceBusTrigger
    {
        private const string GymLogTopicKey = "%AzureWebJobsServiceBusTopicName%";
        private const string GymLogSubscriptionKey = "%AzureWebJobsServiceBusSubscriptionName%";

        private readonly AppSettings _settings;
        private readonly CosmosClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="IngestRoutineServiceBusTrigger"/> class.
        /// </summary>
        /// <param name="settings"><see cref="AppSettings"/> instance.</param>
        /// <param name="client"><see cref="CosmosClient"/> instance.</param>
        public IngestRoutineServiceBusTrigger(AppSettings settings, CosmosClient client)
        {
            this._settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this._client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Ingests message from Service Bus and stores it to Cosmos DB.
        /// </summary>
        /// <param name="msg"><see cref="ServiceBusReceivedMessage"/> instance.</param>
        /// <param name="context"><see cref="ExecutionContext"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        [FunctionName(nameof(IngestRoutineServiceBusTrigger.IngestAsync))]
        public async Task IngestAsync(
            [ServiceBusTrigger(GymLogTopicKey, GymLogSubscriptionKey)] ServiceBusReceivedMessage msg,
            ExecutionContext context,
            ILogger log)
        {
            var message = JsonConvert.DeserializeObject<RoutineQueueMessage>(msg.Body.ToString());
            var messageId = Guid.Parse(msg.MessageId);
            var eventId = context.InvocationId;
            var spanId = (Guid)msg.ApplicationProperties["subSpanId"];
            var correlationId = Guid.Parse(msg.CorrelationId);
            var upn = message.Upn;
            var @interface = Enum.Parse<InterfaceType>(msg.ApplicationProperties["interface"] as string, ignoreCase: true);
            var timestamp = DateTimeOffset.Parse(msg.ApplicationProperties["timestamp"] as string, CultureInfo.InvariantCulture);

            log.LogData(LogLevel.Information, message,
                        EventType.MessageReceived, EventStatusType.Succeeded, eventId,
                        SpanType.Subscriber, SpanStatusType.SubscriberInitiated, spanId,
                        @interface, correlationId);

            try
            {
                if (this._settings.ForceError.Subscriber.Ingest)
                {
                    throw new ErrorEnforcementException("Error Enforced!");
                }

                var databaseName = this._settings.GymLog.CosmosDB.DatabaseName;
                var containerName = this._settings.GymLog.CosmosDB.ContainerName;
                var partitionKeyPath = this._settings.GymLog.CosmosDB.PartitionKeyPath;

                var db = (Database) await this._client
                                              .CreateDatabaseIfNotExistsAsync(databaseName)
                                              .ConfigureAwait(false);

                var properties = new ContainerProperties(containerName, partitionKeyPath);
                var container = (Container) await db.CreateContainerIfNotExistsAsync(properties)
                                                    .ConfigureAwait(false);

                var record = ((RoutineRecordItem) message).WithEntityId(messageId)
                                                      .WithTimestamp(timestamp);
                var response = await container.UpsertItemAsync<RoutineRecordItem>(record, new PartitionKey(record.ItemType.ToString())).ConfigureAwait(false);
                if (response.StatusCode >= HttpStatusCode.BadRequest)
                {
                    throw new HttpRequestException(response.StatusCode.ToDisplayName());
                }

                log.LogData(LogLevel.Information, message,
                            EventType.MessageProcessed, EventStatusType.Succeeded, eventId,
                            SpanType.Subscriber, SpanStatusType.SubscriberCompleted, spanId,
                            @interface, correlationId,
                            recordId: record.EntityId.ToString(),
                            message: response.StatusCode.ToMessageEventType().ToDisplayName());
            }
            catch (Exception ex)
            {
                log.LogData(LogLevel.Error, message,
                            EventType.MessageNotProcessed, EventStatusType.Failed, eventId,
                            SpanType.Subscriber, SpanStatusType.SubscriberCompleted, spanId,
                            @interface, correlationId,
                            ex: ex,
                            message: ex.Message);
                throw;
            }
        }
    }
}

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Azure.Messaging.ServiceBus;

using GymLog.FunctionApp.Configurations;
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
    public class RecordServiceBusTrigger
    {
        private const string GymLogTopicKey = "%AzureWebJobsServiceBusTopicName%";
        private const string GymLogSubscriptionKey = "%AzureWebJobsServiceBusSubscriptionName%";

        private readonly AppSettings _settings;
        private readonly CosmosClient _client;

        public RecordServiceBusTrigger(AppSettings settings, CosmosClient client)
        {
            this._settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this._client = client ?? throw new ArgumentNullException(nameof(client));
        }

        [FunctionName(nameof(RecordServiceBusTrigger.IngestAsync))]
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
            var @interface = Enum.Parse<InterfaceType>(msg.ApplicationProperties["interface"] as string, ignoreCase: true);

            log.LogData(LogLevel.Information, message,
                        EventType.MessageReceived, EventStatusType.Succeeded, eventId,
                        SpanType.Subscriber, SpanStatusType.SubscriberInitiated, spanId,
                        @interface, correlationId);

            try
            {
                var databaseName = this._settings.GymLog.CosmosDB.DatabaseName;
                var containerName = this._settings.GymLog.CosmosDB.ContainerName;
                var partitionKeyPath = this._settings.GymLog.CosmosDB.PartitionKeyPath;

                var db = (Database) await this._client
                                              .CreateDatabaseIfNotExistsAsync(databaseName)
                                              .ConfigureAwait(false);

                var properties = new ContainerProperties(containerName, partitionKeyPath);
                var container = (Container) await db.CreateContainerIfNotExistsAsync(properties)
                                                    .ConfigureAwait(false);

                var record = new RoutineRecord(message) { EntityId = messageId };
                var response = await container.UpsertItemAsync<RoutineQueueMessage>(record, new PartitionKey(record.Routine.ToString())).ConfigureAwait(false);
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

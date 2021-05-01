param name string
param location string = resourceGroup().location
param locationCode string = 'krc'
param env string = 'dev'

param storageAccountId string
param storageAccountName string
param serviceBusId string
param serviceBusAuthRuleId string
param cosmosDbId string
param cosmosDbName string
param appInsightsId string
param consumptionPlanId string

param functionAppTimezone string = 'Korea Standard Time'

param gymLogTableName string = 'gymlogs'
param gymLogTopicName string = 'topic-gymlogs'
param gymLogSubscriptionName string = 'subscription-gymlogs'
param gymLogDatabaseName string = 'GymLog'
param gymLogContainerName string = 'gymlogs'
param gymLogPartitionKeyPath string = '/routine'

param openApiVersion string = 'v2'
param openApiDocVersion string = 'v1.0.0'

var metadata = {
    longName: '{0}-${name}-${env}-${locationCode}'
    shortName: '{0}${name}${env}${locationCode}'
}

var storage = {
    id: storageAccountId
    name: storageAccountName
}
var serviceBus = {
    id: serviceBusId
    authRuleId: serviceBusAuthRuleId
}
var cosmosDb = {
    id: cosmosDbId
    name: cosmosDbName
}
var consumption = {
    id: consumptionPlanId
}
var appInsights = {
    id: appInsightsId
}
var functionApp = {
    name: format(metadata.longName, 'fncapp')
    location: location
    timezone: functionAppTimezone
    gymLog: {
        tableName: gymLogTableName
        topicName: gymLogTopicName
        subscriptionName: gymLogSubscriptionName
        databaseName: gymLogDatabaseName
        containerName: gymLogContainerName
        partitionKeyPath: gymLogPartitionKeyPath
    }
    openapi: {
        version: openApiVersion
        docVersion: openApiDocVersion
    }
}

resource fncapp 'Microsoft.Web/sites@2020-12-01' = {
    name: functionApp.name
    location: functionApp.location
    kind: 'functionapp'
    properties: {
        serverFarmId: consumption.id
        httpsOnly: true
        siteConfig: {
            appSettings: [
                // Common Settings
                {
                    name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
                    value: '${reference(appInsights.id, '2020-02-02-preview', 'Full').properties.InstrumentationKey}'
                }
                {
                    name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
                    value: '${reference(appInsights.id, '2020-02-02-preview', 'Full').properties.connectionString}'
                }
                {
                    name: 'AzureWebJobsStorage'
                    value: 'DefaultEndpointsProtocol=https;AccountName=${storage.name};EndpointSuffix=${environment().suffixes.storage};AccountKey=${listKeys(storage.id, '2021-02-01').keys[0].value}'
                }
                {
                    name: 'FUNCTIONS_EXTENSION_VERSION'
                    value: '~3'
                }
                {
                    name: 'FUNCTION_APP_EDIT_MODE'
                    value: 'readonly'
                }
                {
                    name: 'FUNCTIONS_WORKER_RUNTIME'
                    value: 'dotnet'
                }
                {
                    name: 'WEBSITE_CONTENTAZUREFILECONNECTIONSTRING'
                    value: 'DefaultEndpointsProtocol=https;AccountName=${storage.name};EndpointSuffix=${environment().suffixes.storage};AccountKey=${listKeys(storage.id, '2021-02-01').keys[0].value}'
                }
                {
                    name: 'WEBSITE_CONTENTSHARE'
                    value: functionApp.name
                }
                {
                    name: 'WEBSITE_TIME_ZONE'
                    value: functionApp.timezone
                }
                // Service Bus
                {
                    name: 'AzureWebJobsServiceBus'
                    value: '${listKeys(serviceBus.authRuleId, '2017-04-01').primaryConnectionString}'
                }
                {
                    name:  'AzureWebJobsServiceBusTopicName'
                    value: functionApp.gymLog.topicName
                }
                {
                    name:  'AzureWebJobsServiceBusSubscriptionName'
                    value: functionApp.gymLog.subscriptionName
                }
                // Cosmos DB
                {
                    name: 'CosmosDBConnection'
                    value: 'AccountEndpoint=https://${cosmosDb.name}.documents.azure.com:443/;AccountKey=${listKeys(cosmosDb.id, '2021-03-15').primaryMasterKey};'
                }
                // GymLog Settings
                {
                    name:  'GymLog__StorageAccount__Table__TableName'
                    value: functionApp.gymLog.tableName
                }
                {
                    name:  'GymLog__ServiceBus__TopicName'
                    value: functionApp.gymLog.topicName
                }
                {
                    name:  'GymLog__ServiceBus__SubscriptionName'
                    value: functionApp.gymLog.subscriptionName
                }
                {
                    name:  'GymLog__CosmosDB__DatabaseName'
                    value: functionApp.gymLog.databaseName
                }
                {
                    name:  'GymLog__CosmosDB__ContainerName'
                    value: functionApp.gymLog.containerName
                }
                {
                    name:  'GymLog__CosmosDB__PartitionKeyPath'
                    value: functionApp.gymLog.partitionKeyPath
                }
                // OpenAPI Settings
                {
                    name:  'OpenApi__Version'
                    value: functionApp.openapi.version
                }
                {
                    name:  'OpenApi__DocVersion'
                    value: functionApp.openapi.docVersion
                }
            ]
        }
    }
}

output id string = fncapp.id
output name string = fncapp.name

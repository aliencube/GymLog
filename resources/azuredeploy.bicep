param name string
param location string = resourceGroup().location
param locationCode string = 'krc'
param env string = 'dev'

// Storage
param storageAccountToProvision bool = true
param storageAccountSku string = 'Standard_LRS'

// Service Bus
param serviceBusToProvision bool = true
param serviceBusSku string = 'Standard'
param serviceBusAuthRule string = 'RootManageSharedAccessKey'
param serviceBusTopic string = 'topic-gymlogs'
param serviceBusSubscription string = 'subscription-gymlogs'

// Cosmos DB
param cosmosDbToProvision bool = true
param cosmosDbAccountOfferType string = 'Standard'
param cosmosDbAutomaticFailover bool = true
param cosmosDbConsistencyLevel string = 'Session'
param cosmosDbPrimaryRegion string = 'Korea Central'
param cosmosDbCapability string = 'EnableServerless'
param cosmosDbName string = 'GymLog'
param cosmosDbContainerName string = 'gymlogs'
param cosmosDbPartitionKeyPath string = '/itemType'

// Log Analytics Workspace
param workspaceToProvision bool = true
param workspaceSku string = 'PerGB2018'

// Application Insights
param appInsightsToProvision bool = true

// Azure Workbook
param azureWorkbookToProvision bool = false

// Consumption Plan
param consumptionPlanToProvision bool = true
param isLinux bool = false

// Function App
param functionAppToProvision bool = true
param functionAppTimezone string = 'Korea Standard Time'
param gymLogTableName string = 'gymlogs'
param openApiVersion string = 'v3'
param openApiDocVersion string = 'v1.0.0'
param openApiDocTitle string = 'GymLogs Publisher Interface'
param forceErrorRoutine bool = false
param forceErrorExercise bool = false
param forceErrorPublish bool = false
param forceErrorIngest bool = false

module st './storageAccount.bicep' = if (storageAccountToProvision) {
    name: 'StorageAccount'
    params: {
        name: name
        location: location
        locationCode: locationCode
        env: env
        storageAccountSku: storageAccountSku
    }
}

module svcbus './serviceBus.bicep' = if (serviceBusToProvision) {
    name: 'ServiceBus'
    params: {
        name: name
        location: location
        locationCode: locationCode
        env: env
        serviceBusSku: serviceBusSku
        serviceBusAuthRule: serviceBusAuthRule
        serviceBusTopic: serviceBusTopic
        serviceBusSubscription: serviceBusSubscription
    }
}

module cosdba './cosmosDb.bicep' = if (cosmosDbToProvision) {
    name: 'CosmosDB'
    params: {
        name: name
        location: location
        locationCode: locationCode
        env: env
        cosmosDbAccountOfferType: cosmosDbAccountOfferType
        cosmosDbAutomaticFailover: cosmosDbAutomaticFailover
        cosmosDbConsistencyLevel: cosmosDbConsistencyLevel
        cosmosDbPrimaryRegion: cosmosDbPrimaryRegion
        cosmosDbCapability: cosmosDbCapability
        cosmosDbName: cosmosDbName
        cosmosDbContainerName: cosmosDbContainerName
        cosmosDbPartitionKeyPath: cosmosDbPartitionKeyPath
    }
}

module wrkspc './logAnalyticsWorkspace.bicep' = if (workspaceToProvision) {
    name: 'LogAnalyticsWorkspace'
    params: {
        name: name
        location: location
        locationCode: locationCode
        env: env
        workspaceSku: workspaceSku
    }
}

module appins './appInsights.bicep' = if (appInsightsToProvision) {
    name: 'ApplicationInsights'
    params: {
        name: name
        location: location
        locationCode: locationCode
        env: env
        workspaceId: wrkspc.outputs.id
    }
}

module azwkbk './azureWorkbook.bicep' = if (azureWorkbookToProvision) {
    name: 'AzureWorkbook'
    params: {
        name: name
        location: location
        locationCode: locationCode
        env: env
        appInsightsId: appins.outputs.id
    }
}

module csplan './consumptionPlan.bicep' = if (consumptionPlanToProvision) {
    name: 'ConsumptionPlan'
    params: {
        name: name
        location: location
        locationCode: locationCode
        env: env
        isLinux: isLinux
    }
}

module fncapp './functionApp.bicep' = if (functionAppToProvision) {
    name: 'FunctionApp'
    params: {
        name: name
        location: location
        locationCode: locationCode
        env: env
        storageAccountId: st.outputs.id
        storageAccountName: st.outputs.name
        serviceBusId: svcbus.outputs.id
        serviceBusAuthRuleId: svcbus.outputs.authRuleId
        cosmosDbId: cosdba.outputs.id
        cosmosDbName: cosdba.outputs.name
        appInsightsId: appins.outputs.id
        consumptionPlanId: csplan.outputs.id
        functionAppTimezone: functionAppTimezone
        gymLogTableName: gymLogTableName
        gymLogTopicName: svcbus.outputs.topic
        gymLogSubscriptionName: svcbus.outputs.subscription
        gymLogDatabaseName: cosdba.outputs.database
        gymLogContainerName: cosdba.outputs.container
        gymLogPartitionKeyPath: cosdba.outputs.partitionKeyPath
        openApiVersion: openApiVersion
        openApiDocVersion: openApiDocVersion
        openApiDocTitle: openApiDocTitle
        forceErrorRoutine: forceErrorRoutine
        forceErrorExercise: forceErrorExercise
        forceErrorPublish: forceErrorPublish
        forceErrorIngest: forceErrorIngest
    }
}

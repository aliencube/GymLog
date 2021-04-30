param name string
param location string = resourceGroup().location
param locationCode string = 'krc'
param env string = 'dev'

param cosmosDbAccountOfferType string = 'Standard'
param cosmosDbAutomaticFailover bool = true
param cosmosDbConsistencyLevel string = 'Session'
param cosmosDbPrimaryRegion string = 'Korea Central'
param cosmosDbCapability string = 'EnableServerless'
param cosmosDbName string
param cosmosDbContainerName string
param cosmosDbPartitionKeyPath string

var metadata = {
    longName: '{0}-${name}-${env}-${locationCode}'
    shortName: '{0}${name}${env}${locationCode}'
}

var cosmosDb = {
    name: format(metadata.longName, 'cosdba')
    location: location
    databaseAccountOfferType: cosmosDbAccountOfferType
    enableAutomaticFailover: cosmosDbAutomaticFailover
    defaultConsistencyLevel: cosmosDbConsistencyLevel
    primaryRegion: cosmosDbPrimaryRegion
    capability: cosmosDbCapability
    dbName: cosmosDbName
    containerName: cosmosDbContainerName
    partitionKeyPath: cosmosDbPartitionKeyPath
}

resource cosdba 'Microsoft.DocumentDB/databaseAccounts@2021-03-15' = {
    name: cosmosDb.name
    location: cosmosDb.location
    kind: 'GlobalDocumentDB'
    properties: {
        databaseAccountOfferType: cosmosDb.databaseAccountOfferType
        enableAutomaticFailover: cosmosDb.enableAutomaticFailover
        consistencyPolicy: {
            defaultConsistencyLevel: cosmosDb.defaultConsistencyLevel
            maxIntervalInSeconds: 5
            maxStalenessPrefix: 100
        }
        locations: [
            {
                locationName: cosmosDb.primaryRegion
                failoverPriority: 0
                isZoneRedundant: false
            }
        ]
        capabilities: [
            {
                name: cosmosDb.capability
            }
        ]
        backupPolicy: {
            type: 'Periodic'
            periodicModeProperties: {
                backupIntervalInMinutes: 240
                backupRetentionIntervalInHours: 8
            }
        }
    }
}

resource cosdbaSqlDB 'Microsoft.DocumentDB/databaseAccounts/sqlDatabases@2021-03-15' = {
    name: '${cosdba.name}/${cosmosDb.dbName}'
    location: cosmosDb.location
    properties: {
        resource: {
            id: cosmosDb.dbName
        }
    }
}

resource cosdbaSqlDbContainer 'Microsoft.DocumentDB/databaseAccounts/sqlDatabases/containers@2021-03-15' = {
    name: '${cosdbaSqlDB.name}/${cosmosDb.containerName}'
    location: cosmosDb.location
    properties: {
        resource: {
            id: cosmosDb.containerName
            partitionKey: {
                kind: 'Hash'
                paths: [
                    cosmosDb.partitionKeyPath
                ]
            }
        }
    }
}

output id string = cosdba.id
output name string = cosdba.name
output database string = cosmosDb.dbName
output container string = cosmosDb.containerName
output partitionKeyPath string = cosmosDb.partitionKeyPath

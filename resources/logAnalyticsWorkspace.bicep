param name string
param location string = resourceGroup().location
param locationCode string = 'krc'
param env string = 'dev'

param workspaceSku string = 'PerGB2018'

var metadata = {
    longName: '{0}-${name}-${env}-${locationCode}'
    shortName: '{0}${name}${env}${locationCode}'
}

var workspace = {
    name: format(metadata.longName, 'wrkspc')
    location: location
    sku: workspaceSku
}

resource wrkspc 'Microsoft.OperationalInsights/workspaces@2020-10-01' = {
    name: workspace.name
    location: workspace.location
    properties: {
        sku: {
            name: workspace.sku
        }
        retentionInDays: 30
        workspaceCapping: {
            dailyQuotaGb: -1
        }
        publicNetworkAccessForIngestion: 'Enabled'
        publicNetworkAccessForQuery: 'Enabled'
    }
}

output id string = wrkspc.id
output name string = wrkspc.name

param name string
param location string = resourceGroup().location
param locationCode string = 'krc'
param env string = 'dev'

param workspaceId string

var metadata = {
    longName: '{0}-${name}-${env}-${locationCode}'
    shortName: '{0}${name}${env}${locationCode}'
}

var workspace = {
    id: workspaceId
}
var appInsights = {
    name: format(metadata.longName, 'appins')
    location: location
}

resource appins 'Microsoft.Insights/components@2020-02-02-preview' = {
    name: appInsights.name
    location: appInsights.location
    kind: 'web'
    properties: {
        Flow_Type: 'Bluefield'
        Application_Type: 'web'
        Request_Source: 'rest'
        WorkspaceResourceId: workspace.id
    }
}

output id string = appins.id
output name string = appins.name

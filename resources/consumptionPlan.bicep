param name string
param location string = resourceGroup().location
param locationCode string = 'krc'
param env string = 'dev'

param isLinux bool = false

var metadata = {
    longName: '{0}-${name}-${env}-${locationCode}'
    shortName: '{0}${name}${env}${locationCode}'
}

var consumption = {
    name: format(metadata.longName, 'csplan')
    location: location
    isLinux: isLinux
}

resource csplan 'Microsoft.Web/serverfarms@2020-12-01' = {
    name: consumption.name
    location: consumption.location
    kind: 'functionApp'
    sku: {
        name: 'Y1'
        tier: 'Dynamic'
    }
    properties: {
        reserved: consumption.isLinux
    }
}

output id string = csplan.id
output name string = csplan.name

param name string
param location string = resourceGroup().location
param locationCode string = 'krc'
param env string = 'dev'

param serviceBusSku string = 'Standard'
param serviceBusAuthRule string = 'RootManageSharedAccessKey'
param serviceBusTopic string
param serviceBusSubscription string

var metadata = {
    longName: '{0}-${name}-${env}-${locationCode}'
    shortName: '{0}${name}${env}${locationCode}'
}

var serviceBus = {
    name: format(metadata.longName, 'svcbus')
    location: location
    sku: serviceBusSku
    authRule: serviceBusAuthRule
    topic: serviceBusTopic
    subscription: serviceBusSubscription
}

resource svcbus 'Microsoft.ServiceBus/namespaces@2021-01-01-preview' = {
    name: serviceBus.name
    location: serviceBus.location
    sku: {
        name: serviceBus.sku
    }
}

resource svcBusAuthRules 'Microsoft.ServiceBus/namespaces/authorizationRules@2017-04-01' = {
    name: '${svcbus.name}/${serviceBus.authRule}'
    properties: {
        rights: [
            'Listen'
            'Send'
            'Manage'
        ]
    }
}

resource svcbusTopic 'Microsoft.ServiceBus/namespaces/topics@2018-01-01-preview' = {
    name: '${svcbus.name}/${serviceBus.topic}'
    properties: {
        requiresDuplicateDetection: true
    }
}

resource svcbusTopicSubscription 'Microsoft.ServiceBus/namespaces/topics/subscriptions@2018-01-01-preview' = {
    name: '${svcbusTopic.name}/${serviceBus.subscription}'
    properties: {
        maxDeliveryCount: 10
    }
}

resource svcbusTopicSubscriptionRule 'Microsoft.ServiceBus/namespaces/topics/subscriptions/rules@2017-04-01' = {
    name: '${svcbusTopicSubscription.name}/${serviceBus.subscription}-filter'
    properties: {
        filterType: 'SqlFilter'
        sqlFilter: {
            sqlExpression: '1=1'
        }
    }
}

output id string = svcbus.id
output name string = svcbus.name
output authRuleId string = svcBusAuthRules.id
output topic string = serviceBus.topic
output subscription string = serviceBus.subscription

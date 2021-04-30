param name string
param location string = resourceGroup().location
param locationCode string = 'krc'
param env string = 'dev'

param workbookName string = newGuid()
param appInsightsId string

var metadata = {
    longName: '{0}-${name}-${env}-${locationCode}'
    shortName: '{0}${name}${env}${locationCode}'
}

var appInsights = {
    id: appInsightsId
}
var workbook = {
    name: workbookName
    displayName: format(metadata.longName, 'azwkbk')
    location: location
    queryData: '{"version":"Notebook/1.0","items":[{"type":3,"content":{"version":"KqlItem/1.0","query":"let correlationId = \\"5159ba25-b2c0-48b8-9620-c474b84cb958\\";\\nlet eventStatus = \\"Failed\\";\\nlet spanStatus = \\"PublisherInProgress\\";\\ntraces\\n| sort by timestamp desc\\n| where customDimensions.prop__correlationId == correlationId\\n// | where customDimensions.prop__eventStatus == eventStatus\\n// | where customDimensions.prop__spanStatus == spanStatus\\n| project Timestamp = timestamp\\n        , LogLevel = customDimensions.prop__logLevel\\n        , CorrelationId = tostring(customDimensions.prop__correlationId)\\n        , Interface = customDimensions.prop__interfaceType\\n        , SpanType = customDimensions.prop__spanType\\n        , SpanStatus = customDimensions.prop__spanStatus\\n        , SpanId = tostring(customDimensions.prop__spanId)\\n        , EventType = customDimensions.prop__eventType\\n        , EventStatus = customDimensions.prop__eventStatus\\n        , EventId = tostring(customDimensions.prop__eventId)\\n        , EntityType = customDimensions.prop__entityType\\n        , ClientRequestId = customDimensions.prop__clientRequestId\\n        , MessageId = customDimensions.prop__messageId\\n        , RecordId = customDimensions.prop__recordId\\n| project Timestamp\\n        , CorrelationId\\n        , SpanType\\n        , SpanStatus\\n        , EventType\\n        , EventStatus\\n        , EntityType\\n        , MessageId\\n        , RecordId\\n\\n","size":1,"timeContext":{"durationMs":86400000},"queryType":0,"resourceType":"microsoft.insights/components"},"showPin":false,"name":"E2E Tracing"}],"isLocked":false,"fallbackResourceIds":["@@id@@"]}'
}

resource azwkbk 'Microsoft.Insights/workbooks@2020-10-20' = {
    name: workbook.name
    location: workbook.location
    kind: 'shared'
    properties: {
        displayName: workbook.displayName
        version: '1.0'
        sourceId: appInsights.id
        category: 'tsg'
        serializedData: replace(workbook.queryData, '@@id@@', appInsights.id)
    }
}

output id string = azwkbk.id
output name string = azwkbk.name

using System;

using GymLog.FunctionApp.Models;
using GymLog.FunctionApp.Traces;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;

using Newtonsoft.Json.Serialization;

namespace GymLog.FunctionApp.Examples
{
    /// <summary>
    /// This represents the example entity for the <see cref="RoutineRequestMessage"/> object.
    /// </summary>
    public class RoutineRequestMessageExample : OpenApiExample<RoutineRequestMessage>
    {
        /// <inheritdoc/>
        public override IOpenApiExample<RoutineRequestMessage> Build(NamingStrategy namingStrategy = null)
        {
            this.Examples.Add(
                OpenApiExampleResolver.Resolve(
                    "shoulder",
                    new RoutineRequestMessage()
                    {
                        Upn = "exercise@gymlogs.com",
                        CorrelationId = Guid.Parse("36b5511c-f183-4eb3-b6b5-18cdf53417c9"),
                        Interface = InterfaceType.PowerAppsApp,
                        SpanId = Guid.Parse("0458130f-d474-492f-b2c3-e385596f9d9b"),
                        Routine = RoutineType.Shoulder,
                    },
                    namingStrategy
                )
            );

            return this;
        }
    }
}

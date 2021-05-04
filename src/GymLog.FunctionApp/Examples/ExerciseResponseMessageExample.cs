using System;
using System.Collections.Generic;

using GymLog.FunctionApp.Models;
using GymLog.FunctionApp.Traces;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;

using Newtonsoft.Json.Serialization;

namespace GymLog.FunctionApp.Examples
{
    /// <summary>
    /// This represents the example entity for the <see cref="ExerciseResponseMessage"/> object.
    /// </summary>
    public class ExerciseResponseMessageExample : OpenApiExample<ExerciseResponseMessage>
    {
        /// <inheritdoc/>
        public override IOpenApiExample<ExerciseResponseMessage> Build(NamingStrategy namingStrategy = null)
        {
            this.Examples.Add(
                OpenApiExampleResolver.Resolve(
                    "shoulder",
                    new ExerciseResponseMessage()
                    {
                        Upn = "exercise@gymlogs.com",
                        CorrelationId = Guid.Parse("36b5511c-f183-4eb3-b6b5-18cdf53417c9"),
                        Interface = InterfaceType.PowerAppsApp,
                        SpanId = Guid.Parse("0458130f-d474-492f-b2c3-e385596f9d9b"),
                        EventId = Guid.Parse("93e8a2d5-dc0e-4603-897d-651eeb2969f8"),
                        RoutineId = Guid.Parse("15638065-9ab4-4041-83dd-e34870f7f6e5"),
                        Routine = RoutineType.Shoulder,
                        ExerciseId = Guid.Parse("efdb1a4f-1641-4b7d-8f0a-206e620c0ff3"),
                        Exercise = "Shoulder Press",
                        Sets = new List<ExerciseSet>()
                        {
                            new ExerciseSet() { Sequence = 1, Repetitions = 15, Weight = 10.0M, Unit = WeightUnitType.Lbs },
                            new ExerciseSet() { Sequence = 2, Repetitions = 15, Weight = 10.0M, Unit = WeightUnitType.Lbs },
                            new ExerciseSet() { Sequence = 3, Repetitions = 15, Weight = 10.0M, Unit = WeightUnitType.Lbs },
                            new ExerciseSet() { Sequence = 4, Repetitions = 15, Weight = 10.0M, Unit = WeightUnitType.Lbs },
                            new ExerciseSet() { Sequence = 5, Repetitions = 15, Weight = 10.0M, Unit = WeightUnitType.Lbs },
                        },
                        AdditionalNotes = "All sets with the same weight",
                    },
                    namingStrategy
                )
            );

            return this;
        }
    }
}

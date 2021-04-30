using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GymLog.FunctionApp.Traces
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EventType
    {
        [EnumMember(Value = "Routine Completed")]
        RoutineCompleted = 1200,

        [EnumMember(Value = "Routine Created")]
        RoutineCreated = 1201,

        [EnumMember(Value = "Routine Received")]
        RoutineReceived = 1202,

        [EnumMember(Value = "Invalid Routine")]
        InvalidRoutine = 1400,

        [EnumMember(Value = "Routine Not Found")]
        RoutineNotFound = 1404,

        [EnumMember(Value = "Routine Not Completed")]
        RoutineNotCompleted = 1500,

        [EnumMember(Value = "Routine Not Created")]
        RoutineNotCreated = 1501,

        [EnumMember(Value = "Exercise Created")]
        ExerciseCreated = 2201,

        [EnumMember(Value = "Exercise Received")]
        ExerciseReceived = 2202,

        [EnumMember(Value = "Invalid Exercise")]
        InvalidExercise = 2400,

        [EnumMember(Value = "Exercise Not Found")]
        ExerciseNotFound = 2404,

        [EnumMember(Value = "Exercise Not Created")]
        ExerciseNotCreated = 2500,

        [EnumMember(Value = "Record Populated")]
        RecordPopulated = 3200,

        [EnumMember(Value = "Publish Request Received")]
        PublishRequestReceived = 3202,

        [EnumMember(Value = "Message Published")]
        MessagePublished = 3302,

        [EnumMember(Value = "Invalid Publish Request")]
        InvalidPublishRequest = 3400,

        [EnumMember(Value = "Record Not Found")]
        RecordNotFound = 3404,

        [EnumMember(Value = "Message Not Published")]
        MessageNotPublished = 3500,

        [EnumMember(Value = "Message Processed")]
        MessageProcessed = 4200,

        [EnumMember(Value = "Message Received")]
        MessageReceived = 4202,

        [EnumMember(Value = "Message Not Processed")]
        MessageNotProcessed = 4500,
    }
}

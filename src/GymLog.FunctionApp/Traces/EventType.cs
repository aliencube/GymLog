using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GymLog.FunctionApp.Traces
{
    /// <summary>
    /// This specifies the event type.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EventType
    {
        /// <summary>
        /// Identifies the routine completed.
        /// </summary>
        [EnumMember(Value = "Routine Completed")]
        RoutineCompleted = 1200,

        /// <summary>
        /// Identifies the routine created.
        /// </summary>
        [EnumMember(Value = "Routine Created")]
        RoutineCreated = 1201,

        /// <summary>
        /// Identifies the routine received.
        /// </summary>
        [EnumMember(Value = "Routine Received")]
        RoutineReceived = 1202,

        /// <summary>
        /// Identifies the invalid routine.
        /// </summary>
        [EnumMember(Value = "Invalid Routine")]
        InvalidRoutine = 1400,

        /// <summary>
        /// Identifies the routine not found.
        /// </summary>
        [EnumMember(Value = "Routine Not Found")]
        RoutineNotFound = 1404,

        /// <summary>
        /// Identifies the routine not completed.
        /// </summary>
        [EnumMember(Value = "Routine Not Completed")]
        RoutineNotCompleted = 1500,

        /// <summary>
        /// Identifies the routine not created.
        /// </summary>
        [EnumMember(Value = "Routine Not Created")]
        RoutineNotCreated = 1501,

        /// <summary>
        /// Identifies the exercise created.
        /// </summary>
        [EnumMember(Value = "Exercise Created")]
        ExerciseCreated = 2201,

        /// <summary>
        /// Identifies the exercise received.
        /// </summary>
        [EnumMember(Value = "Exercise Received")]
        ExerciseReceived = 2202,

        /// <summary>
        /// Identifies the invalid exercise.
        /// </summary>
        [EnumMember(Value = "Invalid Exercise")]
        InvalidExercise = 2400,

        /// <summary>
        /// Identifies the exercise not found.
        /// </summary>
        [EnumMember(Value = "Exercise Not Found")]
        ExerciseNotFound = 2404,

        /// <summary>
        /// Identifies the exercise not created.
        /// </summary>
        [EnumMember(Value = "Exercise Not Created")]
        ExerciseNotCreated = 2500,

        /// <summary>
        /// Identifies the record populated.
        /// </summary>
        [EnumMember(Value = "Record Populated")]
        RecordPopulated = 3200,

        /// <summary>
        /// Identifies the publish request received.
        /// </summary>
        [EnumMember(Value = "Publish Request Received")]
        PublishRequestReceived = 3202,

        /// <summary>
        /// Identifies the message published.
        /// </summary>
        [EnumMember(Value = "Message Published")]
        MessagePublished = 3302,

        /// <summary>
        /// Identifies the invalid publish request.
        /// </summary>
        [EnumMember(Value = "Invalid Publish Request")]
        InvalidPublishRequest = 3400,

        /// <summary>
        /// Identifies the record not found.
        /// </summary>
        [EnumMember(Value = "Record Not Found")]
        RecordNotFound = 3404,

        /// <summary>
        /// Identifies the message not published.
        /// </summary>
        [EnumMember(Value = "Message Not Published")]
        MessageNotPublished = 3500,

        /// <summary>
        /// Identifies the message processed.
        /// </summary>
        [EnumMember(Value = "Message Processed")]
        MessageProcessed = 4200,

        /// <summary>
        /// Identifies the message received.
        /// </summary>
        [EnumMember(Value = "Message Received")]
        MessageReceived = 4202,

        /// <summary>
        /// Identifies the message not processed.
        /// </summary>
        [EnumMember(Value = "Message Not Processed")]
        MessageNotProcessed = 4500,
    }
}

using System;
using System.Linq;
using System.Net;

using Azure;

using FluentAssertions;

using GymLog.FunctionApp.Extensions;
using GymLog.FunctionApp.Models;
using GymLog.FunctionApp.Traces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace GymLog.FunctionApp.Tests.Extensions
{
    [TestClass]
    public class AzureRoutineResponseExtensionsTests
    {
        [DataTestMethod]
        [DataRow(HttpStatusCode.BadRequest, "hello world")]
        [DataRow(HttpStatusCode.InternalServerError, "lorem ipsum")]
        public void Given_Error_StatusCode_When_ToRoutineResponseMessage_ForRoutine_Invoked_Then_It_Should_Return_ErrorObjectResult(HttpStatusCode responseStatusCode, string reasonPhrase)
        {
            var response = new Mock<Response>();
            response.SetupGet(p => p.Status).Returns((int)responseStatusCode);
            response.SetupGet(p => p.ReasonPhrase).Returns(reasonPhrase);

            var request = new RoutineRequestMessage();
            var eventId = Guid.NewGuid();
            var routineId = Guid.NewGuid();
            var statusCode = HttpStatusCode.OK;

            var result = AzureResponseExtensions.ToRoutineResponseMessage(response.Object, request, eventId, routineId, statusCode);

            result.Should().BeOfType<ObjectResult>();
            result.Value.Should().BeOfType<ErrorResponseMessage>();
            result.StatusCode.Should().Be((int)responseStatusCode);
            (result.Value as ErrorResponseMessage).Message.Should().Be($"{(int)responseStatusCode}: {reasonPhrase}");
        }

        [DataTestMethod]
        [DataRow(HttpStatusCode.OK, "hello", InterfaceType.PowerAppsApp, "world", TargetType.Back)]
        [DataRow(HttpStatusCode.Created, "lorem", InterfaceType.TestHarness, "ipsum", TargetType.Back, TargetType.Chest)]
        [DataRow(HttpStatusCode.Accepted, "dolor", InterfaceType.WebApp, "sit amet", TargetType.Back, TargetType.Chest, TargetType.Core)]
        public void Given_RequestMessage_When_ToRoutineResponseMessage_ForRoutine_Invoked_Then_It_Should_Return_Result(HttpStatusCode responseStatusCode, string upn, InterfaceType @interface, string routine, params TargetType[] targets)
        {
            var response = new Mock<Response>();
            response.SetupGet(p => p.Status).Returns((int)responseStatusCode);

            var correlationId = Guid.NewGuid();
            var spanId = Guid.NewGuid();
            var request = new RoutineRequestMessage()
            {
                Upn = upn,
                CorrelationId = correlationId,
                Interface = @interface,
                SpanId = spanId,
                Routine = routine,
                Targets = targets.ToList(),
            };
            var eventId = Guid.NewGuid();
            var routineId = Guid.NewGuid();
            var statusCode = HttpStatusCode.OK;

            var result = AzureResponseExtensions.ToRoutineResponseMessage(response.Object, request, eventId, routineId, statusCode);

            result.Should().BeOfType<ObjectResult>();
            result.Value.Should().BeOfType<RoutineResponseMessage>();
            result.StatusCode.Should().Be((int)statusCode);
            (result.Value as RoutineResponseMessage).Upn.Should().Be(upn);
            (result.Value as RoutineResponseMessage).CorrelationId.Should().Be(correlationId);
            (result.Value as RoutineResponseMessage).Interface.Should().Be(@interface);
            (result.Value as RoutineResponseMessage).SpanId.Should().Be(spanId);
            (result.Value as RoutineResponseMessage).EventId.Should().Be(eventId);
            (result.Value as RoutineResponseMessage).RoutineId.Should().Be(routineId);
            (result.Value as RoutineResponseMessage).Routine.Should().Be(routine);
            (result.Value as RoutineResponseMessage).Targets.Should().BeEquivalentTo(targets.ToList());
        }

        [DataTestMethod]
        [DataRow(HttpStatusCode.BadRequest, "hello world")]
        [DataRow(HttpStatusCode.InternalServerError, "lorem ipsum")]
        public void Given_Error_StatusCode_When_ToRoutineResponseMessage_ForPublish_Invoked_Then_It_Should_Return_ErrorObjectResult(HttpStatusCode responseStatusCode, string reasonPhrase)
        {
            var response = new Mock<Response>();
            response.SetupGet(p => p.Status).Returns((int)responseStatusCode);
            response.SetupGet(p => p.ReasonPhrase).Returns(reasonPhrase);

            var request = new PublishRequestMessage();
            var eventId = Guid.NewGuid();
            var routineId = Guid.NewGuid();
            var statusCode = HttpStatusCode.OK;

            var result = AzureResponseExtensions.ToRoutineResponseMessage(response.Object, request, eventId, routineId, statusCode);

            result.Should().BeOfType<ObjectResult>();
            result.Value.Should().BeOfType<ErrorResponseMessage>();
            result.StatusCode.Should().Be((int)responseStatusCode);
            (result.Value as ErrorResponseMessage).Message.Should().Be($"{(int)responseStatusCode}: {reasonPhrase}");
        }

        [DataTestMethod]
        [DataRow(HttpStatusCode.OK, "hello", InterfaceType.PowerAppsApp, "world")]
        [DataRow(HttpStatusCode.Created, "lorem", InterfaceType.TestHarness, "ipsum")]
        [DataRow(HttpStatusCode.Accepted, "dolor", InterfaceType.WebApp, "sit amet")]
        public void Given_RequestMessage_When_ToRoutineResponseMessage_ForPublish_Invoked_Then_It_Should_Return_Result(HttpStatusCode responseStatusCode, string upn, InterfaceType @interface, string routine)
        {
            var response = new Mock<Response>();
            response.SetupGet(p => p.Status).Returns((int)responseStatusCode);

            var correlationId = Guid.NewGuid();
            var spanId = Guid.NewGuid();
            var request = new PublishRequestMessage()
            {
                Upn = upn,
                CorrelationId = correlationId,
                Interface = @interface,
                SpanId = spanId,
                Routine = routine,
            };
            var eventId = Guid.NewGuid();
            var routineId = Guid.NewGuid();
            var statusCode = HttpStatusCode.OK;

            var result = AzureResponseExtensions.ToRoutineResponseMessage(response.Object, request, eventId, routineId, statusCode);

            result.Should().BeOfType<ObjectResult>();
            result.Value.Should().BeOfType<RoutineResponseMessage>();
            result.StatusCode.Should().Be((int)statusCode);
            (result.Value as RoutineResponseMessage).Upn.Should().Be(upn);
            (result.Value as RoutineResponseMessage).CorrelationId.Should().Be(correlationId);
            (result.Value as RoutineResponseMessage).Interface.Should().Be(@interface);
            (result.Value as RoutineResponseMessage).SpanId.Should().Be(spanId);
            (result.Value as RoutineResponseMessage).EventId.Should().Be(eventId);
            (result.Value as RoutineResponseMessage).RoutineId.Should().Be(routineId);
            (result.Value as RoutineResponseMessage).Routine.Should().Be(routine);
            (result.Value as RoutineResponseMessage).Targets.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow(HttpStatusCode.OK, EventType.RoutineCreated)]
        [DataRow(HttpStatusCode.Created, EventType.RoutineCreated)]
        [DataRow(HttpStatusCode.Accepted, EventType.RoutineCreated)]
        [DataRow(HttpStatusCode.BadRequest, EventType.RoutineNotCreated)]
        [DataRow(HttpStatusCode.InternalServerError, EventType.RoutineNotCreated)]
        public void Given_ResponseCode_When_ToRoutineCreatedEventType_Invoked_Then_It_Should_Return_Result(HttpStatusCode statusCode, EventType expected)
        {
            var result = AzureResponseExtensions.ToRoutineCreatedEventType((int)statusCode);

            result.Should().Be(expected);
        }

        [DataTestMethod]
        [DataRow(HttpStatusCode.OK, EventType.RoutineCompleted)]
        [DataRow(HttpStatusCode.Created, EventType.RoutineCompleted)]
        [DataRow(HttpStatusCode.Accepted, EventType.RoutineCompleted)]
        [DataRow(HttpStatusCode.BadRequest, EventType.RoutineNotCompleted)]
        [DataRow(HttpStatusCode.InternalServerError, EventType.RoutineNotCompleted)]
        public void Given_ResponseCode_When_ToRoutineCompletedEventType_Invoked_Then_It_Should_Return_Result(HttpStatusCode statusCode, EventType expected)
        {
            var result = AzureResponseExtensions.ToRoutineCompletedEventType((int)statusCode);

            result.Should().Be(expected);
        }
    }
}

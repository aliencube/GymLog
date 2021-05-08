using System;
using System.Net;

using FluentAssertions;

using GymLog.FunctionApp.ActionResults;
using GymLog.FunctionApp.Models;
using GymLog.FunctionApp.Traces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GymLog.FunctionApp.Tests.ActionResults
{
    [TestClass]
    public class ErrorObjectResultTests
    {
        [DataTestMethod]
        [DataRow("hello world", InterfaceType.PowerAppsApp, "lorem ipsum", HttpStatusCode.InternalServerError)]
        [DataRow("lorem ipsum", InterfaceType.TestHarness, "hello world", HttpStatusCode.NotImplemented)]
        [DataRow("dolor sit amet", InterfaceType.WebApp, "consectetur adipiscing elit", HttpStatusCode.ServiceUnavailable)]
        public void Given_Values_When_Implicitly_Converted_Then_It_Should_Return_Result(string upn, InterfaceType @interface, string message, HttpStatusCode statusCode)
        {
            var correlationId = Guid.NewGuid();
            var spanId = Guid.NewGuid();
            var eventId = Guid.NewGuid();

            var error = new ErrorObjectResult()
            {
                Upn = upn,
                CorrelationId = correlationId,
                Interface = @interface,
                SpanId = spanId,
                EventId = eventId,
                Message = message,
                StatusCode = (int)statusCode,
            };

            ObjectResult result = error;

            result.Should().NotBeNull();
            result.Value.Should().BeOfType<ErrorResponseMessage>();
            (result.Value as ErrorResponseMessage).Upn.Should().Be(upn);
            (result.Value as ErrorResponseMessage).CorrelationId.Should().Be(correlationId);
            (result.Value as ErrorResponseMessage).Interface.Should().Be(@interface);
            (result.Value as ErrorResponseMessage).SpanId.Should().Be(spanId);
            (result.Value as ErrorResponseMessage).EventId.Should().Be(eventId);
            (result.Value as ErrorResponseMessage).Message.Should().Be(message);
            result.StatusCode.Should().Be((int)statusCode);
        }
    }
}

using System;
using System.Reflection;

using FluentAssertions;

using GymLog.FunctionApp.Configurations;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GymLog.FunctionApp.Tests.Configurations
{
    [TestClass]
    public class OpenApiConfigurationOptionsTests
    {
        [TestMethod]
        public void Given_No_OpenApi_DocValues_When_Instantiated_Then_It_Should_Return_Result()
        {
            Environment.SetEnvironmentVariable(AppSettingsKeys.OpenApiDocVersionKey, null);
            Environment.SetEnvironmentVariable(AppSettingsKeys.OpenApiDocTitleKey, null);

            var docVersion = "1.0.0";
            var docTitle = Assembly.GetAssembly(typeof(OpenApiConfigurationOptions)).GetName().Name;

            var options = new OpenApiConfigurationOptions();

            var result = options.Info;

            result.Version.Should().Be(docVersion);
            result.Title.Should().Be(docTitle);
        }

        [DataTestMethod]
        [DataRow("hello world", "lorem ipsum")]
        public void Given_OpenApi_DocValues_When_Instantiated_Then_It_Should_Return_Result(string docVersion, string docTitle)
        {
            Environment.SetEnvironmentVariable(AppSettingsKeys.OpenApiDocVersionKey, docVersion);
            Environment.SetEnvironmentVariable(AppSettingsKeys.OpenApiDocTitleKey, docTitle);

            var options = new OpenApiConfigurationOptions();

            var result = options.Info;

            result.Version.Should().Be(docVersion);
            result.Title.Should().Be(docTitle);
        }

        [TestMethod]
        public void Given_No_OpenApi_Version_When_Instantiated_Then_It_Should_Return_Result()
        {
            Environment.SetEnvironmentVariable(AppSettingsKeys.OpenApiVersionKey, null);

            var version = OpenApiVersionType.V2;

            var options = new OpenApiConfigurationOptions();

            var result = options.OpenApiVersion;

            result.Should().Be(version);
        }

        [DataTestMethod]
        [DataRow(OpenApiVersionType.V2)]
        [DataRow(OpenApiVersionType.V3)]
        public void Given_OpenApi_Version_When_Instantiated_Then_It_Should_Return_Result(OpenApiVersionType version)
        {
            Environment.SetEnvironmentVariable(AppSettingsKeys.OpenApiVersionKey, version.ToString().ToLowerInvariant());

            var options = new OpenApiConfigurationOptions();

            var result = options.OpenApiVersion;

            result.Should().Be(version);
        }
    }
}

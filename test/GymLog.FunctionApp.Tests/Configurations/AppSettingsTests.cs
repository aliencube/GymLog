using System;
using System.Reflection;

using FluentAssertions;

using GymLog.FunctionApp.Configurations;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GymLog.FunctionApp.Tests.Configurations
{
    [TestClass]
    public class AppSettingsTests
    {
        [TestMethod]
        public void Given_No_EnvironmentVariables_When_Instantiated_Then_It_Should_Return_Default_GymLogSettings()
        {
            var settings = new AppSettings();

            var result = settings.GymLog;

            result.Should().NotBeNull();
        }

        [TestMethod]
        public void Given_No_EnvironmentVariables_When_Instantiated_Then_It_Should_Return_Default_StorageAccountSettings()
        {
            Environment.SetEnvironmentVariable(AppSettingsKeys.StorageAccountKey, null);
            Environment.SetEnvironmentVariable(AppSettingsKeys.GymLogTableKey, null);

            var settings = new AppSettings();

            var result = settings.GymLog.StorageAccount;

            result.Should().NotBeNull();
            result.ConnectionString.Should().BeEmpty();
            result.Table.Should().NotBeNull();
            result.Table.TableName.Should().BeEmpty();
        }

        [DataTestMethod]
        [DataRow("hello world", "hello world", "lorem ipsum", "lorem ipsum")]
        [DataRow(null, "", null, "")]
        public void Given_EnvironmentVariables_When_Instantiated_Then_It_Should_Return_StorageAccountSettings(
            string connectionString, string expectedConnectionString,
            string tableName, string expectedTableName)
        {
            Environment.SetEnvironmentVariable(AppSettingsKeys.StorageAccountKey, connectionString);
            Environment.SetEnvironmentVariable(AppSettingsKeys.GymLogTableKey, tableName);

            var settings = new AppSettings();

            var result = settings.GymLog.StorageAccount;

            result.Should().NotBeNull();
            result.ConnectionString.Should().Be(expectedConnectionString);
            result.Table.Should().NotBeNull();
            result.Table.TableName.Should().Be(expectedTableName);
        }

        [TestMethod]
        public void Given_No_EnvironmentVariables_When_Instantiated_Then_It_Should_Return_Default_ServiceBusSettings()
        {
            Environment.SetEnvironmentVariable(AppSettingsKeys.ServiceBusKey, null);
            Environment.SetEnvironmentVariable(AppSettingsKeys.GymLogTopicKey, null);
            Environment.SetEnvironmentVariable(AppSettingsKeys.GymLogSubscriptionKey, null);

            var settings = new AppSettings();

            var result = settings.GymLog.ServiceBus;

            result.Should().NotBeNull();
            result.ConnectionString.Should().BeEmpty();
            result.TopicName.Should().BeEmpty();
            result.SubscriptionName.Should().BeEmpty();
        }

        [DataTestMethod]
        [DataRow("hello world", "hello world", "lorem ipsum", "lorem ipsum", "dolor sit amet", "dolor sit amet")]
        [DataRow(null, "", null, "", null, "")]
        public void Given_EnvironmentVariables_When_Instantiated_Then_It_Should_Return_Default_ServiceBusSettings(
            string connectionString, string expectedConnectionString,
            string topicName, string expectedTopicName,
            string subscriptionName, string expectedSubscriptionName)
        {
            Environment.SetEnvironmentVariable(AppSettingsKeys.ServiceBusKey, connectionString);
            Environment.SetEnvironmentVariable(AppSettingsKeys.GymLogTopicKey, topicName);
            Environment.SetEnvironmentVariable(AppSettingsKeys.GymLogSubscriptionKey, subscriptionName);

            var settings = new AppSettings();

            var result = settings.GymLog.ServiceBus;

            result.Should().NotBeNull();
            result.ConnectionString.Should().Be(expectedConnectionString);
            result.TopicName.Should().Be(expectedTopicName);
            result.SubscriptionName.Should().Be(expectedSubscriptionName);
        }

        [TestMethod]
        public void Given_No_EnvironmentVariables_When_Instantiated_Then_It_Should_Return_Default_CosmosDBSettings()
        {
            Environment.SetEnvironmentVariable(AppSettingsKeys.CosmosDBKey, null);
            Environment.SetEnvironmentVariable(AppSettingsKeys.GymLogDatabaseNameKey, null);
            Environment.SetEnvironmentVariable(AppSettingsKeys.GymLogContainerKey, null);
            Environment.SetEnvironmentVariable(AppSettingsKeys.GymLogPartitionKeyPathKey, null);

            var settings = new AppSettings();

            var result = settings.GymLog.CosmosDB;

            result.Should().NotBeNull();
            result.ConnectionString.Should().BeEmpty();
            result.DatabaseName.Should().BeEmpty();
            result.ContainerName.Should().BeEmpty();
            result.PartitionKeyPath.Should().BeEmpty();
        }

        [DataTestMethod]
        [DataRow("hello world", "hello world", "lorem ipsum", "lorem ipsum", "dolor sit amet", "dolor sit amet", "consectetur adipiscing elit", "consectetur adipiscing elit")]
        [DataRow(null, "", null, "", null, "", null, "")]
        public void Given_EnvironmentVariables_When_Instantiated_Then_It_Should_Return_CosmosDBSettings(
            string connectionString, string expectedConnectionString,
            string databaseName, string expectedDatabaseName,
            string containerName, string expectedContainerName,
            string partitionKeyPath, string expectedPartitionKeyPath)
        {
            Environment.SetEnvironmentVariable(AppSettingsKeys.CosmosDBKey, connectionString);
            Environment.SetEnvironmentVariable(AppSettingsKeys.GymLogDatabaseNameKey, databaseName);
            Environment.SetEnvironmentVariable(AppSettingsKeys.GymLogContainerKey, containerName);
            Environment.SetEnvironmentVariable(AppSettingsKeys.GymLogPartitionKeyPathKey, partitionKeyPath);

            var settings = new AppSettings();

            var result = settings.GymLog.CosmosDB;

            result.Should().NotBeNull();
            result.ConnectionString.Should().Be(expectedConnectionString);
            result.DatabaseName.Should().Be(expectedDatabaseName);
            result.ContainerName.Should().Be(expectedContainerName);
            result.PartitionKeyPath.Should().Be(expectedPartitionKeyPath);
        }

        [TestMethod]
        public void Given_No_EnvironmentVariables_When_Instantiated_Then_It_Should_Return_Default_ApplicationInsightsSettings()
        {
            Environment.SetEnvironmentVariable(AppSettingsKeys.AppInsightsKey, null);

            var settings = new AppSettings();

            var result = settings.GymLog.ApplicationInsights;

            result.Should().NotBeNull();
            result.ConnectionString.Should().BeEmpty();
        }

        [DataTestMethod]
        [DataRow("hello world", "hello world")]
        [DataRow(null, "")]
        public void Given_EnvironmentVariables_When_Instantiated_Then_It_Should_Return_ApplicationInsightsSettings(
            string connectionString, string expectedConnectionString)
        {
            Environment.SetEnvironmentVariable(AppSettingsKeys.AppInsightsKey, connectionString);

            var settings = new AppSettings();

            var result = settings.GymLog.ApplicationInsights;

            result.Should().NotBeNull();
            result.ConnectionString.Should().Be(expectedConnectionString);
        }

        [TestMethod]
        public void Given_No_EnvironmentVariables_When_Instantiated_Then_It_Should_Return_Default_OpenApiSettings()
        {
            Environment.SetEnvironmentVariable(AppSettingsKeys.OpenApiVersionKey, null);
            Environment.SetEnvironmentVariable(AppSettingsKeys.OpenApiDocVersionKey, null);
            Environment.SetEnvironmentVariable(AppSettingsKeys.OpenApiDocTitleKey, null);

            var version = OpenApiVersionType.V2;
            var docVersion = "1.0.0";
            var docTitle = Assembly.GetAssembly(typeof(AppSettings)).GetName().Name;

            var settings = new AppSettings();

            var result = settings.OpenApi;

            result.Should().NotBeNull();
            result.Version.Should().Be(version);
            result.DocumentVersion.Should().Be(docVersion);
            result.DocumentTitle.Should().Be(docTitle);
        }

        [DataTestMethod]
        [DataRow(OpenApiVersionType.V2, "hello world", "lorem ipsum")]
        [DataRow(OpenApiVersionType.V3, "dolor sit amet", "consectetur adipiscing elit")]
        public void Given_EnvironmentVariables_When_Instantiated_Then_It_Should_Return_Default_OpenApiSettings(OpenApiVersionType version, string docVersion, string docTitle)
        {
            Environment.SetEnvironmentVariable(AppSettingsKeys.OpenApiVersionKey, version.ToString().ToLowerInvariant());
            Environment.SetEnvironmentVariable(AppSettingsKeys.OpenApiDocVersionKey, docVersion);
            Environment.SetEnvironmentVariable(AppSettingsKeys.OpenApiDocTitleKey, docTitle);

            var settings = new AppSettings();

            var result = settings.OpenApi;

            result.Should().NotBeNull();
            result.Version.Should().Be(version);
            result.DocumentVersion.Should().Be(docVersion);
            result.DocumentTitle.Should().Be(docTitle);
        }

        [TestMethod]
        public void Given_No_EnvironmentVariables_When_Instantiated_Then_It_Should_Return_Default_ForceErrorSettingsSettings()
        {
            Environment.SetEnvironmentVariable(AppSettingsKeys.ForceErrorPublisherRoutineKey, null);
            Environment.SetEnvironmentVariable(AppSettingsKeys.ForceErrorPublisherExerciseKey, null);
            Environment.SetEnvironmentVariable(AppSettingsKeys.ForceErrorPublisherPublishKey, null);
            Environment.SetEnvironmentVariable(AppSettingsKeys.ForceErrorSubscriberIngestKey, null);

            var settings = new AppSettings();

            var result = settings.ForceError;

            result.Should().NotBeNull();
            result.Publisher.Routine.Should().BeFalse();
            result.Publisher.Exercise.Should().BeFalse();
            result.Publisher.Publish.Should().BeFalse();
            result.Subscriber.Ingest.Should().BeFalse();
        }

        [DataTestMethod]
        [DataRow(true, false, false, false)]
        [DataRow(true, true, false, false)]
        [DataRow(true, true, true, false)]
        [DataRow(true, true, true, true)]
        public void Given_EnvironmentVariables_When_Instantiated_Then_It_Should_Return_ForceErrorSettingsSettings(bool routine, bool exercise, bool publish, bool ingest)
        {
            Environment.SetEnvironmentVariable(AppSettingsKeys.ForceErrorPublisherRoutineKey, routine.ToString().ToLowerInvariant());
            Environment.SetEnvironmentVariable(AppSettingsKeys.ForceErrorPublisherExerciseKey, exercise.ToString().ToLowerInvariant());
            Environment.SetEnvironmentVariable(AppSettingsKeys.ForceErrorPublisherPublishKey, publish.ToString().ToLowerInvariant());
            Environment.SetEnvironmentVariable(AppSettingsKeys.ForceErrorSubscriberIngestKey, ingest.ToString().ToLowerInvariant());

            var settings = new AppSettings();

            var result = settings.ForceError;

            result.Should().NotBeNull();
            result.Publisher.Routine.Should().Be(routine);
            result.Publisher.Exercise.Should().Be(exercise);
            result.Publisher.Publish.Should().Be(publish);
            result.Subscriber.Ingest.Should().Be(ingest);
        }
    }
}

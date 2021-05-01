using System;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace GymLog.FunctionApp.Configurations
{
    /// <summary>
    /// This represents the options entity for OpenAPI metadata configuration.
    /// </summary>
    public class OpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
    {
        private const string OpenApiVersionKey = "OpenApi__Version";

        /// <inheritdoc/>
        public override OpenApiInfo Info { get; set; } = new OpenApiInfo()
        {
            Version = Environment.GetEnvironmentVariable("OpenApi__DocVersion") ?? "1.0.0",
            Title = "GymLog Publisher Interface",
            Description = "Publisher interface that accepts requests from front-end applications including a test harness app, Power Apps app or web app.",
            TermsOfService = new Uri("https://github.com/aliencube/GymLog"),
            Contact = new OpenApiContact()
            {
                Name = "Aliencube",
                Url = new Uri("https://github.com/aliencube/GymLog/issues"),
            },
            License = new OpenApiLicense()
            {
                Name = "MIT",
                Url = new Uri("http://opensource.org/licenses/MIT"),
            }
        };

        /// <inheritdoc/>
        public override OpenApiVersionType OpenApiVersion { get; set; } = Enum.TryParse<OpenApiVersionType>(
                                                                              Environment.GetEnvironmentVariable(OpenApiVersionKey), ignoreCase: true, out var result)
                                                                            ? result
                                                                            : OpenApiVersionType.V2;
    }
}

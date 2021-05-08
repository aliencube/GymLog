using System;
using System.Reflection;

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
        private const string OpenApiDocVersionKey = "OpenApi__DocVersion";
        private const string OpenApiDocTitleKey = "OpenApi__DocTitle";

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiConfigurationOptions"/> class.
        /// </summary>
        public OpenApiConfigurationOptions()
            : base()
        {
            this.Info = new OpenApiInfo()
            {
                Version = Environment.GetEnvironmentVariable(OpenApiDocVersionKey) ?? OpenApiConfigurationOptions.DefaultDocVersion(),
                Title = Environment.GetEnvironmentVariable(OpenApiDocTitleKey) ?? OpenApiConfigurationOptions.DefaultDocTitle(Assembly.GetAssembly(this.GetType())),
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

            this.OpenApiVersion = Enum.TryParse<OpenApiVersionType>(
                                        Environment.GetEnvironmentVariable(OpenApiVersionKey), ignoreCase: true, out var result)
                                      ? result
                                      : OpenApiConfigurationOptions.DefaultVersion();
        }

        /// <inheritdoc/>
        public override OpenApiInfo Info { get; set; }

        /// <inheritdoc/>
        public override OpenApiVersionType OpenApiVersion { get; set; }

        /// <summary>
        /// Gets the default OpenAPI document version.
        /// </summary>
        /// <returns>Returns the default OpenAPI document version.</returns>
        public static string DefaultDocVersion()
        {
            return "1.0.0";
        }

        /// <summary>
        /// Gets the default OpenAPI document title - assembly name.
        /// </summary>
        /// <param name="assembly"><see cref="Assembly"/> instance.</param>
        /// <returns>Returns the default OpenAPI document title - assembly name.</returns>
        public static string DefaultDocTitle(Assembly assembly)
        {
            return assembly.GetName().Name;
        }

        /// <summary>
        /// Gets the default OpenAPI version.
        /// </summary>
        /// <returns>Returns the default OpenAPI version.</returns>
        public static OpenApiVersionType DefaultVersion()
        {
            return OpenApiVersionType.V2;
        }
    }
}

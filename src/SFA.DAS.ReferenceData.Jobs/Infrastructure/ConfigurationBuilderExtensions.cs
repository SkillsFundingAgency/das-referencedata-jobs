using Microsoft.Extensions.Configuration;
using SFA.DAS.Configuration.AzureTableStorage;

namespace SFA.DAS.ReferenceData.Jobs.Infrastructure;

internal static class ConfigurationBuilderExtensions
{
    internal static IConfigurationRoot ConfigureConfiguration(this IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory());
        var preConfig = builder.Build();

        builder.AddEnvironmentVariables();
        if (!preConfig.IsLocalOrDev())
        {
            builder.AddAzureTableStorage(options =>
            {
                options.ConfigurationKeys = preConfig["ConfigNames"].Split(",");
                options.StorageConnectionString = preConfig["ConfigurationStorageConnectionString"];
                options.EnvironmentName = preConfig["EnvironmentName"];
                options.PreFixConfigurationKeys = false;
            });
        }
        builder.AddJsonFile("local.settings.json", optional: true);
        return builder.Build();
    }
}
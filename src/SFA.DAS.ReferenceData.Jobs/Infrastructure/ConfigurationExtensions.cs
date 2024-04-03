using Microsoft.Extensions.Configuration;

namespace SFA.DAS.ReferenceData.Jobs.Infrastructure;

public static class ConfigurationExtensions
{
    public static bool IsLocalOrDev(this IConfiguration config)
    {
        return config["EnvironmentName"].Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase) ||
               config["EnvironmentName"].Equals("DEV", StringComparison.CurrentCultureIgnoreCase);
    }

    public static bool IsDev(this IConfiguration config)
    {
        return config["EnvironmentName"].Equals("DEV", StringComparison.CurrentCultureIgnoreCase);
    }
}
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SFA.DAS.ReferenceData.Jobs.Infrastructure;
using SFA.DAS.ReferenceData.Jobs.Interfaces;

IConfigurationRoot? config = null;
var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration((hostingContext, configBuilder) =>
    {
        config = configBuilder.ConfigureConfiguration();
    })
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.AddLogging(builder =>
        {
            builder.AddFilter<ApplicationInsightsLoggerProvider>(string.Empty, LogLevel.Information);
            builder.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Information);
        }); 
        services.ConfigureFunctionsApplicationInsights();
        services.AddOptions();
        services.Configure<ReferenceDataApimConfiguration>(config.GetSection(ReferenceDataApimConfiguration.ReferenceDataApim));
        services.AddSingleton(cfg => cfg.GetRequiredService<IOptions<ReferenceDataApimConfiguration>>().Value);
        services.AddHttpClient<IOuterApiClient, OuterApiClient>();
    })
    .Build();

host.Run();

using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.ReferenceData.Jobs.Interfaces;

namespace SFA.DAS.ReferenceData.Jobs.Events;

public class StartReferenceDataImports
{
    private readonly IOuterApiClient _client;
    private readonly ILogger<StartReferenceDataImports> _logger;

    public StartReferenceDataImports(ILogger<StartReferenceDataImports> logger, IOuterApiClient client)
    {
        _client = client;
        _logger = logger;
    }

    [Function("StartReferenceDataImports")]
    public async Task Run([TimerTrigger("%ImportSchedule%", RunOnStartup = true)] TimerInfo myTimer)
    {
        try
        {
            _logger.LogInformation("DataLoad function started at: {0}", DateTime.Now);
            await _client.StartDataLoad();
            _logger.LogInformation("DataLoad function completed at: {0}", DateTime.Now);
        }
        catch (Exception ex)
        {
            _logger.LogError("DataLoad function call failed", ex);
            throw;
        }
    }
}



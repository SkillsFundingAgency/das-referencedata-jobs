using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SFA.DAS.ReferenceData.Jobs.Interfaces;

namespace SFA.DAS.ReferenceData.Jobs.Events;

public class StartPublicSectorOrganisationsImport
{
    private readonly IApimClient _client;
    private readonly ILogger<StartPublicSectorOrganisationsImport> _logger;

    public StartPublicSectorOrganisationsImport(ILogger<StartPublicSectorOrganisationsImport> logger, IApimClient client)
    {
        _client = client;
        _logger = logger;
    }

    [Function("StartPublicSectorOrganisationsImport")]
    public async Task Run([TimerTrigger("%PublicSectorOrganisationsImportSchedule%")] TimerInfo myTimer)
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



using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SFA.DAS.ReferenceData.Jobs.Interfaces;

namespace SFA.DAS.ReferenceData.Jobs.Infrastructure;

public class OuterApiClient : IOuterApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<OuterApiClient> _logger;
    private readonly ReferenceDataApimConfiguration _configuration;

    public OuterApiClient(HttpClient httpClient, IOptions<ReferenceDataApimConfiguration> configuration, ILogger<OuterApiClient> logger)
    {
        _configuration = configuration.Value;
        httpClient.BaseAddress = new Uri(_configuration.ApiBaseUrl);
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task StartDataLoad()
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "dataload");
        AddAuthenticationHeader(requestMessage);

        _logger.LogInformation("Making call to start data load"); 
        var response = await _httpClient.SendAsync(requestMessage);

        if (response.StatusCode == HttpStatusCode.GatewayTimeout)
        {
            throw new TimeoutException("Call to APIM 'dataload' has timed out");
        }

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Call to start data load failed with http status code {0}",  response.StatusCode);
            throw new ApplicationException("Call to start data load failed");
        }
    }

    private void AddAuthenticationHeader(HttpRequestMessage httpRequestMessage)
    {
        httpRequestMessage.Headers.Add("Ocp-Apim-Subscription-Key", _configuration.SubscriptionKey);
        httpRequestMessage.Headers.Add("X-Version", _configuration.ApiVersion);
    }
}
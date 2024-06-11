using AutoFixture.NUnit3;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.ReferenceData.Jobs.Infrastructure;
using SFA.DAS.ReferenceData.Jobs.Interfaces;
using SFA.DAS.Testing.AutoFixture;
using System.Net;
using FluentAssertions;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Options;
using Moq.Protected;
using SFA.DAS.ReferenceData.Jobs.Tests.Helpers;

namespace SFA.DAS.ReferenceData.Jobs.Tests.Infrastructure;

public class WhenCallingDataLoad
{
    [Test, MoqAutoData]
    public async Task Then_Calls_Api_endpoint_correctly(
        [Frozen] Mock<ILogger<OuterApiClient>> mockLogger)
    {
        var config = new ReferenceDataApimConfiguration
        {
            ApiBaseUrl = "http://test.com",
            ApiVersion = "1.0",
            SubscriptionKey = "SubKey"
        };

        var options = Options.Create(config);

        var httpMessageHandler = MessageHandlerHelper.SetupMessageHandlerMock(new HttpResponseMessage { StatusCode = HttpStatusCode.Accepted }, "POST");
        var httpClient = new HttpClient(httpMessageHandler.Object);

        var sut = new OuterApiClient(httpClient, options, mockLogger.Object);

        await sut.StartDataLoad();

        httpMessageHandler.Protected()
            .Verify<Task<HttpResponseMessage>>(
                "SendAsync", Times.Once(),
                ItExpr.Is<HttpRequestMessage>(c =>
                    c.Method.Equals(HttpMethod.Post)
                    && c.RequestUri.AbsoluteUri.StartsWith(config.ApiBaseUrl, StringComparison.InvariantCultureIgnoreCase)
                ),
                ItExpr.IsAny<CancellationToken>()
            );
    }
}
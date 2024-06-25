using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.ReferenceData.Jobs.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.ReferenceData.Jobs.Tests.Events.StartReferenceDataImport;

public class WhenStartingDataLoad
{
    [Test, MoqAutoData]
    public async Task Then_Successfully_Call_Client(
        [Frozen] Mock<IOuterApiClient> mockApiClient,
        [Frozen] Mock<ILogger<Jobs.Events.StartReferenceDataImports>> mockLogger,
        [Greedy] Jobs.Events.StartReferenceDataImports job)
    {
        await job.Run(new TimerInfo());

        mockApiClient.Verify(x => x.StartDataLoad(), Times.Once);
    }

    [Test, MoqAutoData]
    public async Task Then_swallows_TimeOutException(
        [Frozen] Mock<IOuterApiClient> mockApiClient,
        [Frozen] Mock<ILogger<Jobs.Events.StartReferenceDataImports>> mockLogger,
        [Greedy] Jobs.Events.StartReferenceDataImports job)
    {

        mockApiClient.Setup(x => x.StartDataLoad()).Throws<TimeoutException>();
        await job.Run(new TimerInfo());

        mockApiClient.Verify(x => x.StartDataLoad(), Times.Once);
    }

    [Test, MoqAutoData]
    public async Task Then_rethrows_ApplicationException(
        [Frozen] Mock<IOuterApiClient> mockApiClient,
        [Frozen] Mock<ILogger<Jobs.Events.StartReferenceDataImports>> mockLogger,
        [Greedy] Jobs.Events.StartReferenceDataImports job)
    {

        mockApiClient.Setup(x => x.StartDataLoad()).Throws<ApplicationException>();
        var act = () => job.Run(new TimerInfo());

        act.Should().ThrowAsync<ApplicationException>();
    }
}
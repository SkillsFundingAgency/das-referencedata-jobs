using AutoFixture.NUnit3;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.ReferenceData.Jobs.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.ReferenceData.Jobs.Tests.Events.StartPublicSectorOrganisationsImport;

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
}
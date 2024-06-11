namespace SFA.DAS.ReferenceData.Jobs.Infrastructure;

public class ReferenceDataApimConfiguration
{
    public const string ReferenceDataApim = "ReferenceDataApim";
    public string ApiBaseUrl { get; set; } = null!;
    public string SubscriptionKey { get; set; } = null!;
    public string ApiVersion { get; set; } = null!;
}

using ECMS.Core.Model;

namespace ECMS.Core.UseCases.Campaign.Get;

public class GetCampaignResponse
{
    public IEnumerable<CampaignResponse> Campaigns { get; init; } = [];
}

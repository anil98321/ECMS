using ECMS.API.DTO;

namespace ECMS.API.Routes.Campaign.Command.GetById;

public sealed class GetByIdCampaignResponse
{
    public CampaignSummaryResonseDTO CampaignSummary { get; internal set; }
}
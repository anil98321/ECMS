using ECMS.API.DTO;

namespace ECMS.API.Routes.Campaign.Command.Get;

public sealed class GetCampaignResponse
{
    public IEnumerable<CampaignResponseDTO> Campaigns { get; internal set; }
}
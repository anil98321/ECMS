using FastEndpoints;

namespace ECMS.API.Routes.Campaign.Command.Get;

public sealed class GetCampaignRequest
{
    public int CampaignId { get; set; }
}
internal sealed class Validator : Validator<GetCampaignRequest>
{
    public Validator()
    {
       
    }
}


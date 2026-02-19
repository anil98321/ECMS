using FastEndpoints;
using FluentValidation;

namespace ECMS.API.Routes.Campaign.Command.GetById;

public sealed class GetByIdCampaignRequest
{
    public int CampaignId { get; set; }
}
internal sealed class Validator : Validator<GetByIdCampaignRequest>
{
    public Validator()
    {
        RuleFor(x => x.CampaignId).NotNull().GreaterThan(0);
    }
}


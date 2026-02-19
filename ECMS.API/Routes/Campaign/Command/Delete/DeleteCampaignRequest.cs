using FastEndpoints;
using FluentValidation;

namespace ECMS.API.Routes.Campaign.Command.Delete;

public sealed class DeleteCampaignRequest
{
    public int CampaignId { get; set; }
}
internal sealed class Validator : Validator<DeleteCampaignRequest>
{
    public Validator()
    {
        RuleFor(x => x.CampaignId).NotNull().GreaterThan(0);
    }
}


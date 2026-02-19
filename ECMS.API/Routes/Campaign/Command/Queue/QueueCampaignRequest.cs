using FastEndpoints;
using FluentValidation;

namespace ECMS.API.Routes.Campaign.Command.Queue;

public sealed class QueueCampaignRequest
{
    public int CampaignId { get; set; }
}
internal sealed class Validator : Validator<QueueCampaignRequest>
{
    public Validator()
    {
        RuleFor(x => x.CampaignId).NotNull().GreaterThan(0);
    }
}


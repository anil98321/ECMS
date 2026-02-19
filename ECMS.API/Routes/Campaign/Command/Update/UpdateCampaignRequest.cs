using FastEndpoints;
using FluentValidation;

namespace ECMS.API.Routes.Campaign.Command.Update;

public sealed class UpdateCampaignRequest
{
    public int CampaignId { get; set; }
    public string CampaignName { get; set; }
    public string Subject { get; set; }
    public string HtmlBody { get; set; }
    public int Status { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public IEnumerable<int> Groups { get; set; }
}
internal sealed class Validator : Validator<UpdateCampaignRequest>
{
    public Validator()
    {
        RuleFor(x => x.CampaignId).NotNull().GreaterThan(0);
        RuleFor(x => x.CampaignName).NotNull().NotEmpty();
        RuleFor(x => x.Subject).NotNull().NotEmpty();
        RuleFor(x => x.HtmlBody).NotNull().NotEmpty();
        RuleFor(x => x.Status).NotNull();
        RuleFor(x => x.ScheduledDate).NotNull();
    }
}


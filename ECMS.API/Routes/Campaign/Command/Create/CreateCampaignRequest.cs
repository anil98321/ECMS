using FastEndpoints;
using FluentValidation;

namespace ECMS.API.Routes.Campaign.Command.Create;

public sealed class CreateCampaignRequest
{
    public string CampaignName { get; set; }
    public string Subject { get; set; }
    public string HtmlBody { get; set; }
    public int Status { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public IEnumerable<int> Groups { get; set; }
}
internal sealed class Validator : Validator<CreateCampaignRequest>
{
    public Validator()
    {
        RuleFor(x => x.CampaignName).NotNull().NotEmpty();
        RuleFor(x => x.Subject).NotNull().NotEmpty();
        RuleFor(x => x.HtmlBody).NotNull().NotEmpty();
        RuleFor(x => x.Status).NotNull();
        RuleFor(x => x.ScheduledDate).NotNull();       
    }
}


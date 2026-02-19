namespace ECMS.Core.Model;

public record Campaign
{
    public int CampaignId { get; set; }

    public string CampaignName { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string HtmlBody { get; set; } = null!;

    public int Status { get; set; }

    public DateTime? ScheduledDate { get; set; }

    public virtual IList<CampaignGroup> CampaignGroups { get; set; }

}

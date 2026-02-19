namespace ECMS.Core.EFStructures.Entities;

public partial class Campaign
{
    public int CampaignId { get; set; }

    public string CampaignName { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string HtmlBody { get; set; } = null!;

    public int Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ScheduledDate { get; set; }

    public virtual ICollection<CampaignGroup> CampaignGroups { get; set; } = new List<CampaignGroup>();

    public virtual ICollection<CampaignLog> CampaignLogs { get; set; } = new List<CampaignLog>();
}

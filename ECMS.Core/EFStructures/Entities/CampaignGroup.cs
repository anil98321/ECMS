namespace ECMS.Core.EFStructures.Entities;

public partial class CampaignGroup
{
    public int CampaignGroupId { get; set; }

    public int CampaignId { get; set; }

    public int GroupId { get; set; }

    public virtual Campaign Campaign { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;
}

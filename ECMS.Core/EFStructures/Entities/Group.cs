namespace ECMS.Core.EFStructures.Entities;

public partial class Group
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<CampaignGroup> CampaignGroups { get; set; } = new List<CampaignGroup>();

    public virtual ICollection<ClientGroup> ClientGroups { get; set; } = new List<ClientGroup>();
}

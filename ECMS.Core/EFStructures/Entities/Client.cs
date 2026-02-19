namespace ECMS.Core.EFStructures.Entities;

public partial class Client
{
    public int ClientId { get; set; }

    public string ClientName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<CampaignLog> CampaignLogs { get; set; } = new List<CampaignLog>();

    public virtual ICollection<ClientGroup> ClientGroups { get; set; } = new List<ClientGroup>();
}

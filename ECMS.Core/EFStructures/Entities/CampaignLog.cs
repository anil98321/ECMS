namespace ECMS.Core.EFStructures.Entities;

public partial class CampaignLog
{
    public long LogId { get; set; }

    public int CampaignId { get; set; }

    public int ClientId { get; set; }

    public int Status { get; set; }

    public DateTime? SentDate { get; set; }

    public string? ErrorMessage { get; set; }

    public virtual Campaign Campaign { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;
}

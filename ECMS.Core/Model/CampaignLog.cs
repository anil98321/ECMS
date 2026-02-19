namespace ECMS.Core.Model;

public record CampaignLog
{
    public long LogId { get; set; }

    public int CampaignId { get; set; }

    public int ClientId { get; set; }

    public int Status { get; set; }

    public DateTime? SentDate { get; set; }

    public string? ErrorMessage { get; set; }
}

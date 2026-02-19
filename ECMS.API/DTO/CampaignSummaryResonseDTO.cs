namespace ECMS.API.DTO;

public record CampaignSummaryResonseDTO
{
    public int CampaignId { get; set; }
    public string CampaignName { get; set; }
    public string Subject { get; set; }
    public string HtmlBody { get; set; }
    public int Status { get; set; }
    public DateTime ScheduledDate { get; set; }

    // Groups
    public List<string> GroupNames { get; set; } = new();

    // Stats
    public int TotalClients { get; set; }
    public int SentCount { get; set; }
    public int PendingCount { get; set; }
    public int FailedCount { get; set; }
    public double ProgressPercentage => TotalClients > 0 ? (double)(SentCount + FailedCount) / TotalClients * 100 : 0;

    // Failed clients (only if failedCount > 0)
    public List<FailedClientDto> FailedClients { get; set; } = new();

}

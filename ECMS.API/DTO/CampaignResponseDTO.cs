namespace ECMS.API.DTO;
public struct CampaignResponseDTO
{
    public int CampaignId { get; set; }
    public string CampaignName { get; set; }
    public string Subject { get; set; }
    public string HtmlBody { get; set; }
    public int Status { get; set; }
    public int TotalClients { get; set; }
    public int SentCount { get; set; }
    public int FailedCount { get; set; }
    public DateTime? ScheduledDate { get; set; }
}

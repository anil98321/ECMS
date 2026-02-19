namespace ECMS.API.DTO;
public struct CampaignDTO
{
    public int CampaignId { get; set; }
    public string CampaignName { get; set; }
    public string Subject { get; set; }
    public string HtmlBody { get; set; }
    public int Status { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public IEnumerable<int> Groups { get; set; }
}

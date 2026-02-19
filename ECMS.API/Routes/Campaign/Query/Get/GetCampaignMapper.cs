using ECMS.API.DTO;
using ECMS.Core.Model;
using FastEndpoints;

namespace ECMS.API.Routes.Campaign.Command.Get;

public sealed class GetCampaignMapper : Mapper<GetCampaignRequest, GetCampaignResponse, IEnumerable<CampaignResponse>>
{
    public override Task<GetCampaignResponse> FromEntityAsync(IEnumerable<CampaignResponse> campaigns, CancellationToken ct = default)
    {
        var response = new GetCampaignResponse
        {
            Campaigns = campaigns.Select(cam => new CampaignResponseDTO
            {
                CampaignId = cam.CampaignId,
                CampaignName = cam.CampaignName,
                FailedCount = cam.FailedCount,
                Subject = cam.Subject,
                HtmlBody = cam.HtmlBody,
                ScheduledDate = cam.ScheduledDate,
                SentCount = cam.SentCount,
                Status = cam.Status,
                TotalClients = cam.TotalClients
            })
        };

        return Task.FromResult(response);
    }
}
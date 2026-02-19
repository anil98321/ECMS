using ECMS.API.DTO;
using ECMS.Core.Model;
using FastEndpoints;

namespace ECMS.API.Routes.Campaign.Command.GetById;

public sealed class GetByIdCampaignMapper : Mapper<GetByIdCampaignRequest, GetByIdCampaignResponse, CampaignSummaryResonse>
{
    public override Task<GetByIdCampaignResponse> FromEntityAsync(CampaignSummaryResonse campaignSummary, CancellationToken ct = default)
    {
        var failedClients = new List<FailedClientDto>();

        if (campaignSummary != null && campaignSummary.FailedClients.Any())
        {
            foreach (var failedClient in campaignSummary.FailedClients)
            {
                failedClients.Add(new FailedClientDto
                {
                    ClientId = failedClient.ClientId,
                    ClientName = failedClient.ClientName,
                    Email = failedClient.Email,
                    ErrorMessage = failedClient.ErrorMessage,
                });
            }
        }
        var response = new GetByIdCampaignResponse() { };
        if (campaignSummary != null) {
            response.CampaignSummary = new CampaignSummaryResonseDTO
            {
                CampaignId = campaignSummary.CampaignId,
                CampaignName = campaignSummary.CampaignName,
                ScheduledDate = campaignSummary.ScheduledDate,
                Subject = campaignSummary.Subject,
                Status = campaignSummary.Status,
                HtmlBody = campaignSummary.HtmlBody,
                FailedCount = campaignSummary.FailedCount,
                FailedClients = failedClients,
                PendingCount = campaignSummary.PendingCount,
                GroupNames = campaignSummary.GroupNames,
                SentCount = campaignSummary.SentCount,
                TotalClients = campaignSummary.TotalClients

            };
        }        

        return Task.FromResult(response);
    }
}
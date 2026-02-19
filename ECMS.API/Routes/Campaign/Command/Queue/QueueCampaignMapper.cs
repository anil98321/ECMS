using FastEndpoints;

namespace ECMS.API.Routes.Campaign.Command.Queue;

public sealed class QueueCampaignMapper : Mapper<QueueCampaignRequest, QueueCampaignResponse, QueueCampaignResponse>
{
    public override Task<QueueCampaignResponse> FromEntityAsync(QueueCampaignResponse responseInput, CancellationToken ct = default)
    {

        var response = new QueueCampaignResponse
        {
            IsSuccess = responseInput.IsSuccess,
        };

        return Task.FromResult(response);
    }
}
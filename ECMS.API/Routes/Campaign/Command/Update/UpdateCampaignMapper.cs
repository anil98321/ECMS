using FastEndpoints;

namespace ECMS.API.Routes.Campaign.Command.Update;

public sealed class UpdateCampaignMapper : Mapper<UpdateCampaignRequest, UpdateCampaignResponse, UpdateCampaignResponse>
{
    public override Task<UpdateCampaignResponse> FromEntityAsync(UpdateCampaignResponse createCampaignResponse, CancellationToken ct = default)
    {

        var response = new UpdateCampaignResponse
        {
            IsSuccess = createCampaignResponse.IsSuccess,
        };

        return Task.FromResult(response);
    }
}
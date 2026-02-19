using FastEndpoints;

namespace ECMS.API.Routes.Campaign.Command.Create;

public sealed class CreateCampaignMapper : Mapper<CreateCampaignRequest, CreateCampaignResponse, CreateCampaignResponse>
{
    public override Task<CreateCampaignResponse> FromEntityAsync(CreateCampaignResponse createCampaignResponse, CancellationToken ct = default)
    {

        var response = new CreateCampaignResponse
        {
            IsSuccess = createCampaignResponse.IsSuccess,
        };

        return Task.FromResult(response);
    }
}
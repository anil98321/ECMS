using ECMS.Core.UseCases.Campaign.Delete;
using FastEndpoints;

namespace ECMS.API.Routes.Campaign.Command.Delete;

public sealed class DeleteCampaignMapper : Mapper<DeleteCampaignRequest, DeleteCampaignResponse, DeleteCampaignResponse>
{
    public override Task<DeleteCampaignResponse> FromEntityAsync(DeleteCampaignResponse responseInput, CancellationToken ct = default)
    {

        var response = new DeleteCampaignResponse
        {
            IsSuccess = responseInput.IsSuccess,
        };

        return Task.FromResult(response);
    }
}
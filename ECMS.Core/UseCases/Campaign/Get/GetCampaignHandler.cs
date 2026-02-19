using ECMS.Core.DataAccess.Campaign;
using FastEndpoints;

namespace ECMS.Core.UseCases.Campaign.Get;

public class GetCampaignHandler(ICampaignRepository campaignRepository) : ICommandHandler<GetCampaignCommand, GetCampaignResponse>
{
    /// <summary>
    /// Executes the create process for a Campaign.
    /// </summary>
    /// <param name="command">The data contract containing the Campaign information to be created.</param>
    /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
    /// <returns>response containing the created Campaign details.</returns>
    public async Task<GetCampaignResponse> ExecuteAsync(GetCampaignCommand command, CancellationToken ct)
    {

        var campaigns = await campaignRepository.GetAllCampaignAsync();
        var response = new GetCampaignResponse
        {
            Campaigns = campaigns
        };

        return response;
    }
}

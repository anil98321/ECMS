using ECMS.Core.DataAccess.Campaign;
using FastEndpoints;

namespace ECMS.Core.UseCases.Campaign.Update;

public class UpdateCampaignHandler(ICampaignRepository campaignRepository) : ICommandHandler<UpdateCampaignCommand, UpdateCampaignResponse>
{
    /// <summary>
    /// Executes the create process for a Campaign.
    /// </summary>
    /// <param name="command">The data contract containing the Campaign information to be created.</param>
    /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
    /// <returns>response containing the Update Campaign details.</returns>
    public async Task<UpdateCampaignResponse> ExecuteAsync(UpdateCampaignCommand command, CancellationToken ct)
    {
        var val = await campaignRepository.UpdateCampaignAsync(command.request);
        var createCampaignResponse = new UpdateCampaignResponse
        {
            IsSuccess = val
        };

        return createCampaignResponse;
    }
}

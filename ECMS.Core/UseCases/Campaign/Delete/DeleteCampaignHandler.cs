using ECMS.Core.DataAccess.Campaign;
using FastEndpoints;

namespace ECMS.Core.UseCases.Campaign.Delete;

public class GetCampaignHandler(ICampaignRepository campaignRepository) : ICommandHandler<DeleteCampaignCommand, DeleteCampaignResponse>
{
    /// <summary>
    /// Executes the create process for a Campaign.
    /// </summary>
    /// <param name="command">The data contract containing the Campaign information to be created.</param>
    /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
    /// <returns>response containing the created Campaign details.</returns>
    public async Task<DeleteCampaignResponse> ExecuteAsync(DeleteCampaignCommand command, CancellationToken ct)
    {

        var val = await campaignRepository.DeleteCampaignAsync(command.id);
        var deleteCampaignResponse = new DeleteCampaignResponse
        {
            IsSuccess = val
        };

        return deleteCampaignResponse;
    }
}

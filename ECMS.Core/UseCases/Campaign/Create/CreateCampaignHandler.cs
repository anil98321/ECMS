using ECMS.Core.DataAccess.Campaign;
using FastEndpoints;

namespace ECMS.Core.UseCases.Campaign.Create;

public class CreateCampaignHandler(ICampaignRepository campaignRepository) : ICommandHandler<CreateCampaignCommand, CreateCampaignResponse>
{
    /// <summary>
    /// Executes the create process for a Campaign.
    /// </summary>
    /// <param name="command">The data contract containing the Campaign information to be created.</param>
    /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
    /// <returns>response containing the created Campaign details.</returns>
    public async Task<CreateCampaignResponse> ExecuteAsync(CreateCampaignCommand command, CancellationToken ct)
    {
        var val = await campaignRepository.CreateCampaignAsync(command.request);
        var createCampaignResponse = new CreateCampaignResponse
        {
            IsSuccess = val
        };

        return createCampaignResponse;
    }
}

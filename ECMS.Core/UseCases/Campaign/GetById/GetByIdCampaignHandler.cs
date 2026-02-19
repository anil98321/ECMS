using ECMS.Core.DataAccess.Campaign;
using FastEndpoints;

namespace ECMS.Core.UseCases.Campaign.GetById;

public class GetByIdCampaignHandler(ICampaignRepository campaignRepository) : ICommandHandler<GetByIdCampaignCommand, GetByIdCampaignResponse>
{
    /// <summary>
    /// Executes the create process for a Campaign.
    /// </summary>
    /// <param name="command">The data contract containing the Campaign information to be created.</param>
    /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
    /// <returns>response containing the created Campaign details.</returns>
    public async Task<GetByIdCampaignResponse> ExecuteAsync(GetByIdCampaignCommand command, CancellationToken ct)
    {

        var campaignSummaryResonse = await campaignRepository.GetCampaignByIdAsync(command.id);
        var response = new GetByIdCampaignResponse
        {
            CampaignSummaryResonse = campaignSummaryResonse,
        };

        return response;
    }
}

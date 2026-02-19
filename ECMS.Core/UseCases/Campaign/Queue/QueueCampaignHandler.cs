using ECMS.Core.DataAccess.Campaign;
using FastEndpoints;

namespace ECMS.Core.UseCases.Campaign.Queue;

public class QueueCampaignHandler(ICampaignRepository campaignRepository) : ICommandHandler<QueueCampaignCommand, QueueCampaignResponse>
{
    /// <summary>
    /// Executes the Queue process for a Campaign.
    /// </summary>
    /// <param name="command">The data contract containing the Campaign information to be created.</param>
    /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
    /// <returns>response containing the Queue Campaign details.</returns>
    public async Task<QueueCampaignResponse> ExecuteAsync(QueueCampaignCommand command, CancellationToken ct)
    {
        var val = await campaignRepository.QueueCampaignAsync(command.id);
        var queueCampaignResponse = new QueueCampaignResponse
        {
            IsSuccess = val
        };

        return queueCampaignResponse;
    }
}

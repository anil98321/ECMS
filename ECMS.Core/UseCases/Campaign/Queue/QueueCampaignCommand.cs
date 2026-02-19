using FastEndpoints;

namespace ECMS.Core.UseCases.Campaign.Queue;
/// <summary>
/// Data contract for the DeleteCampaign operation, representing the required input data.
/// </summary>
public record QueueCampaignCommand(int id) : ICommand<QueueCampaignResponse>
{

};
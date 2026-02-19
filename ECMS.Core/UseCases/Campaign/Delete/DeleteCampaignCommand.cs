using FastEndpoints;

namespace ECMS.Core.UseCases.Campaign.Delete;
/// <summary>
/// Data contract for the DeleteCampaign operation, representing the required input data.
/// </summary>
public record DeleteCampaignCommand(int id) : ICommand<DeleteCampaignResponse>
{

};
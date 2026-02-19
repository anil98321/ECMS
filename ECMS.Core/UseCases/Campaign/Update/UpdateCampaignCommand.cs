using FastEndpoints;

namespace ECMS.Core.UseCases.Campaign.Update;
/// <summary>
/// Data contract for the UpdateCampaign operation, representing the required input data.
/// </summary>
public record UpdateCampaignCommand(Model.Campaign request) : ICommand<UpdateCampaignResponse>
{

};
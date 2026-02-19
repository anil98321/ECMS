using FastEndpoints;

namespace ECMS.Core.UseCases.Campaign.Create;
/// <summary>
/// Data contract for the CreateCampaign operation, representing the required input data.
/// </summary>
public record CreateCampaignCommand(Model.Campaign request) : ICommand<CreateCampaignResponse>
{

};
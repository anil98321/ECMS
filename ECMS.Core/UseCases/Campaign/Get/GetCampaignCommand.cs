using FastEndpoints;

namespace ECMS.Core.UseCases.Campaign.Get;
/// <summary>
/// Data contract for the GetCampaign operation, representing the required input data.
/// </summary>
public record GetCampaignCommand() : ICommand<GetCampaignResponse>
{

};
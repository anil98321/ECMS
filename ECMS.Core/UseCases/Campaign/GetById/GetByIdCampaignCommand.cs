using FastEndpoints;

namespace ECMS.Core.UseCases.Campaign.GetById;
/// <summary>
/// Data contract for the DeleteCampaign operation, representing the required input data.
/// </summary>
public record GetByIdCampaignCommand(int id) : ICommand<GetByIdCampaignResponse>
{

};
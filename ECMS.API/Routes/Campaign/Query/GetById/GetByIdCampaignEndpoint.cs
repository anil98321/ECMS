using ECMS.Core.UseCases.Campaign.GetById;
using FastEndpoints;
using System.ComponentModel;

namespace ECMS.API.Routes.Campaign.Command.GetById;

[Description("Delete campaign information.")]
public sealed class GetByIdCampaignEndpoint : EndpointWithoutRequest<GetByIdCampaignResponse, GetByIdCampaignMapper>
{
    public override void Configure()
    {
        Get("campaigns/{id:int:min(1)}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        try
        {
            var CampaignId = Route<int>("id");
            var getByIdCampaignCommand = new GetByIdCampaignCommand(CampaignId);
            var response = await getByIdCampaignCommand.ExecuteAsync(ct);
            if(response?.CampaignSummaryResonse == null)
            {
                await Send.NotFoundAsync(cancellation: ct);
            }
            await Send.OkAsync(await Map.FromEntityAsync(response.CampaignSummaryResonse, ct), cancellation: ct);
        }
        catch (Exception ex)
        {
            await Send.ErrorsAsync(cancellation: ct);
            Logger.LogError(ex, "Error while getting campaigns");
        }
    }
}
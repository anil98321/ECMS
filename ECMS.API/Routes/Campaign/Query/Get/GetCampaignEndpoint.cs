using ECMS.Core.UseCases.Campaign.Get;
using FastEndpoints;
using System.ComponentModel;

namespace ECMS.API.Routes.Campaign.Command.Get;

[Description("Delete campaign information.")]
public sealed class GetCampaignEndpoint : EndpointWithoutRequest<GetCampaignResponse, GetCampaignMapper>
{
    public override void Configure()
    {
        Get("campaigns");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        try
        {
            var getCampaignCommand = new GetCampaignCommand();
            var response = await getCampaignCommand.ExecuteAsync(ct);
            await Send.OkAsync(await Map.FromEntityAsync(response.Campaigns, ct), cancellation: ct);
        }
        catch (Exception ex)
        {
            await Send.ErrorsAsync(cancellation: ct);
            Logger.LogError(ex, "Error while getting campaigns");
        }
    }
}
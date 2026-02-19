using ECMS.Core.UseCases.Campaign.Delete;
using FastEndpoints;
using System.ComponentModel;

namespace ECMS.API.Routes.Campaign.Command.Delete;

[Description("Delete campaign information.")]
public sealed class DeleteCampaignEndpoint : EndpointWithoutRequest<DeleteCampaignResponse>
{
    public override void Configure()
    {
        Delete("campaigns/{id:int:min(1)}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        try
        {
            var CampaignId = Route<int>("id");
            var createCampaignCommand = new DeleteCampaignCommand(CampaignId);
            var response = await createCampaignCommand.ExecuteAsync(ct);
            await Send.OkAsync(new DeleteCampaignResponse() { IsSuccess = response.IsSuccess }, cancellation: ct);
        }
        catch (Exception ex)
        {
            await Send.ErrorsAsync(cancellation: ct);
            Logger.LogError(ex, "Error while Delete campaigns");
        }
    }
}
using ECMS.Core.UseCases.Campaign.Queue;
using FastEndpoints;
using System.ComponentModel;

namespace ECMS.API.Routes.Campaign.Command.Queue;

[Description("Queue campaign information.")]
public sealed class QueueCampaignEndpoint : EndpointWithoutRequest<QueueCampaignResponse>
{
    public override void Configure()
    {
        Post("campaigns/{id:int:min(1)}/queue");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        try
        {
            var CampaignId = Route<int>("id");
            var command = new QueueCampaignCommand(CampaignId);
            var response = await command.ExecuteAsync(ct);
            await Send.OkAsync(new QueueCampaignResponse() { IsSuccess = response.IsSuccess }, cancellation: ct);
        }
        catch (Exception ex)
        {
            await Send.ErrorsAsync(cancellation: ct);
            Logger.LogError(ex, "Error while Queue campaigns");
        }
    }
}
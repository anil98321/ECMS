using ECMS.Core.UseCases.Campaign.Update;
using FastEndpoints;
using System.ComponentModel;

using CampaignGroupModel = ECMS.Core.Model.CampaignGroup;

namespace ECMS.API.Routes.Campaign.Command.Update;

[Description("Update new campaign information.")]
public sealed class UpdateCampaignEndpoint : Endpoint<UpdateCampaignRequest, UpdateCampaignResponse, UpdateCampaignMapper>
{
    public override void Configure()
    {
        Put("campaigns/{id:int:min(1)}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateCampaignRequest request, CancellationToken ct)
    {
        try
        {
            var CampaignId = Route<int>("id");
            IList<CampaignGroupModel> CampaignGroups = new List<CampaignGroupModel>();
            foreach (var grp in request.Groups)
            {
                CampaignGroups.Add(new CampaignGroupModel() { GroupId = grp });
            }

            var createCampaignCommand = new UpdateCampaignCommand(new Core.Model.Campaign()
            {
                CampaignId = request.CampaignId,
                CampaignName = request.CampaignName,
                HtmlBody = request.HtmlBody,
                ScheduledDate = request.ScheduledDate,
                Status = request.Status,
                Subject = request.Subject,
                CampaignGroups = CampaignGroups
            });
            var response = await createCampaignCommand.ExecuteAsync(ct);
            await Send.OkAsync(await Map.FromEntityAsync(new UpdateCampaignResponse() { IsSuccess = response.IsSuccess }, ct), cancellation: ct);
        }
        catch (Exception ex)
        {
            await Send.ErrorsAsync(cancellation: ct);
            Logger.LogError(ex, "Error while updating campaigns");
        }
    }
}
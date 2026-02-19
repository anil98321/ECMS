using ECMS.Core.UseCases.Campaign.Create;
using FastEndpoints;
using System.ComponentModel;
using CampaignGroupModel = ECMS.Core.Model.CampaignGroup;

namespace ECMS.API.Routes.Campaign.Command.Create;

[Description("Add new campaign information.")]
public sealed class CreateCampaignEndpoint : Endpoint<CreateCampaignRequest, CreateCampaignResponse, CreateCampaignMapper>
{
    public override void Configure()
    {
        Post("/campaigns");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateCampaignRequest request, CancellationToken ct)
    {
        try
        {
            IList<CampaignGroupModel> CampaignGroups = new List<CampaignGroupModel>();
            foreach (var grp in request.Groups)
            {
                CampaignGroups.Add(new CampaignGroupModel() { GroupId = grp });
            }
            var createCampaignCommand = new CreateCampaignCommand(new Core.Model.Campaign()
            {

                CampaignName = request.CampaignName,
                HtmlBody = request.HtmlBody,
                ScheduledDate = request.ScheduledDate,
                Status = request.Status,
                Subject = request.Subject,
                CampaignGroups = CampaignGroups,
            });
            var response = await createCampaignCommand.ExecuteAsync(ct);
            await Send.OkAsync(await Map.FromEntityAsync(new CreateCampaignResponse() { IsSuccess = response.IsSuccess }, ct), cancellation: ct);
        }
        catch (Exception ex)
        {
            await Send.ErrorsAsync(cancellation: ct);
            Logger.LogError(ex, "Error while creating campaigns");
        }
    }
}
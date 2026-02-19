
using ECMS.Core.Model;

namespace ECMS.Core.DataAccess.Campaign;
public interface ICampaignRepository
{
    Task<bool> CreateCampaignAsync(Model.Campaign campaign);
    Task<bool> UpdateCampaignAsync(Model.Campaign campaign);
    Task<bool> DeleteCampaignAsync(int id);
    Task<bool> QueueCampaignAsync(int id);
    Task<IEnumerable<CampaignResponse>> GetAllCampaignAsync();
    Task<CampaignSummaryResonse> GetCampaignByIdAsync(int id);
}
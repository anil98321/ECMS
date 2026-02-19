using ECMS.Core.EFStructures;
using ECMS.Core.Model;
using Microsoft.EntityFrameworkCore;
using CampaignEntity = ECMS.Core.EFStructures.Entities.Campaign;
using CampaignGroupEntity = ECMS.Core.EFStructures.Entities.CampaignGroup;
using CampaignLogEntity = ECMS.Core.EFStructures.Entities.CampaignLog;
using CampaignModel = ECMS.Core.Model.Campaign;

namespace ECMS.Core.DataAccess.Campaign;
public class CampaignRepository(ECMSDbContext context) : ICampaignRepository
{
    private readonly ECMSDbContext _context = context;
    public async Task<bool> CreateCampaignAsync(CampaignModel campaign)
    {
        try
        {
            var camGrp = new List<CampaignGroupEntity>();

            foreach (var grp in campaign.CampaignGroups)
            {
                camGrp.Add(new CampaignGroupEntity { GroupId = grp.GroupId });
            }

            await _context.Campaigns.AddAsync(new CampaignEntity()
            {
                CampaignName = campaign.CampaignName.Trim(),
                Subject = campaign.Subject,
                HtmlBody = campaign.HtmlBody,
                ScheduledDate = campaign.ScheduledDate,
                Status = 0,
                CampaignGroups = camGrp

            });
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public async Task<bool> DeleteCampaignAsync(int id)
    {
        try
        {

            var deletingCamp = await _context.Campaigns
                   .Include(c => c.CampaignGroups)
                    .Include(c => c.CampaignLogs)
                    .FirstOrDefaultAsync(c => c.CampaignId == id && c.Status == 0);

            if (deletingCamp != null)
            {
                _context.Campaigns.Remove(deletingCamp);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public async Task<IEnumerable<CampaignResponse>> GetAllCampaignAsync()
    {
        return await _context.Campaigns
         .AsNoTracking()
         .Select(c => new
         {
             c.CampaignId,
             c.CampaignName,
             c.Status,
             c.ScheduledDate,
             c.Subject,
             c.HtmlBody,
             TotalClients = _context.ClientGroups
                 .Where(cg => cg.Group.CampaignGroups.Any(cg2 => cg2.CampaignId == c.CampaignId))
                 .Count(cg => cg.Client.IsActive),
             SentCount = c.CampaignLogs.Count(l => l.Status == 1),
             FailedCount = c.CampaignLogs.Count(l => l.Status == 2)
         })
         .ToListAsync()
         .ContinueWith(t => t.Result.Select(x => new CampaignResponse
         {
             CampaignId = x.CampaignId,
             CampaignName = x.CampaignName,
             Status = x.Status,
             ScheduledDate = x.ScheduledDate,
             HtmlBody = x.HtmlBody,
             Subject = x.Subject,
             TotalClients = x.TotalClients,
             SentCount = x.SentCount,
             FailedCount = x.FailedCount
         }).ToList());
    }
    public async Task<CampaignSummaryResonse> GetCampaignByIdAsync(int id)
    {
        var summary = await _context.Campaigns
        .AsNoTracking()
        .Where(c => c.CampaignId == id)
        .Select(c => new CampaignSummaryResonse
        {
            CampaignId = c.CampaignId,
            CampaignName = c.CampaignName,
            Subject = c.Subject,
            HtmlBody = c.HtmlBody,
            Status = c.Status,
            ScheduledDate = c.ScheduledDate ?? DateTime.Now,
            GroupNames = c.CampaignGroups.Select(cg => cg.Group.GroupName).ToList(),
            TotalClients = c.CampaignGroups
                .SelectMany(cg => cg.Group.ClientGroups)
                .Count(cg => cg.Client.IsActive),
            SentCount = c.CampaignLogs.Count(l => l.Status == 1),
            PendingCount = c.CampaignLogs.Count(l => l.Status == 0),
            FailedCount = c.CampaignLogs.Count(l => l.Status == 2)
        })
        .FirstOrDefaultAsync();

        if (summary == null) return null;

        // Load failed clients separately (if any)
        if (summary.FailedCount > 0)
        {
            summary.FailedClients = await _context.CampaignLogs
                .AsNoTracking()
                .Where(cl => cl.CampaignId == id && cl.Status == 2)
                .Select(cl => new FailedClient
                {
                    ClientId = cl.ClientId,
                    ClientName = cl.Client.ClientName,
                    Email = cl.Client.Email,
                    ErrorMessage = cl.ErrorMessage
                })
                .ToListAsync();
        }

        return summary;
    }
    public async Task<bool> QueueCampaignAsync(int id)
    {

        try
        {
            var updatingCamp = await _context.Campaigns.FirstOrDefaultAsync(c => c.CampaignId == id && c.Status == 0);

            if (updatingCamp != null)
            {
                updatingCamp.Status = 1;
                _context.Campaigns.Attach(updatingCamp);
                _context.Entry(updatingCamp).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var clients = await _context.Campaigns
                .Where(c => c.CampaignId == id && c.Status == 1)
                .SelectMany(c => c.CampaignGroups)
                .SelectMany(cg => cg.Group.ClientGroups)
                .Select(cg => cg.Client)
                .Where(c => c.IsActive)
                .Distinct()
                .Select(c => new
                {
                    ClientID = c.ClientId,
                    ClientName = c.ClientName,
                    Email = c.Email,
                    CampaignId = id,
                })
                .ToListAsync();

                if (clients.Any())
                {
                    foreach (var client in clients)
                    {
                        _context.CampaignLogs.Add(new CampaignLogEntity()
                        {
                            Status = 0,
                            CampaignId = client.CampaignId,
                            ClientId = client.ClientID
                        });
                    }
                    await _context.SaveChangesAsync();
                }

                return true;
            }
            else
            {
                return false;
            }


        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public async Task<bool> UpdateCampaignAsync(CampaignModel campaign)
    {
        try
        {
            var updatingCamp = await _context.Campaigns
                  .Include(c => c.CampaignGroups)
                  .FirstOrDefaultAsync(c => c.CampaignId == campaign.CampaignId && c.Status == 0);

            if (updatingCamp != null)
            {
                updatingCamp.CampaignName = campaign.CampaignName.Trim();
                updatingCamp.Subject = campaign.Subject.Trim();
                updatingCamp.HtmlBody = campaign.HtmlBody.Trim();
                updatingCamp.ScheduledDate = campaign.ScheduledDate;
                updatingCamp.Status = 0;

                // Replace CampaignGroups (many-to-many)
                var currentGroupIds = updatingCamp.CampaignGroups.Select(cg => cg.GroupId).ToHashSet();

                foreach (var cg in currentGroupIds)
                {
                    var ef = updatingCamp.CampaignGroups.FirstOrDefault(cg => cg.GroupId == cg.GroupId && cg.CampaignId == campaign.CampaignId);
                    if (ef != null)
                    {
                        updatingCamp.CampaignGroups.Remove(ef);
                        _context.Campaigns.Attach(updatingCamp);
                        _context.Entry(updatingCamp).State = EntityState.Modified;
                        var val1 = await _context.SaveChangesAsync(); ;
                    }
                }

                //updatingCamp.CampaignGroups.Clear();

                foreach (var grp in campaign.CampaignGroups)
                {
                    // Verify group exists
                    var group = await _context.Groups.FindAsync(grp.GroupId);
                    if (group != null && group.IsActive)
                    {
                        updatingCamp.CampaignGroups.Add(new CampaignGroupEntity
                        {
                            GroupId = group.GroupId
                        });
                    }
                }
                _context.Campaigns.Attach(updatingCamp);
                _context.Entry(updatingCamp).State = EntityState.Modified;
                var val = await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }


        }
        catch (Exception ex)
        {
            return false;
        }
    }

}
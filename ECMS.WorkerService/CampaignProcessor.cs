using ECMS.Core.EFStructures;
using ECMS.Core.EFStructures.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECMS.WorkerService
{ 
    public class CampaignProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<CampaignProcessor> _logger;
        private readonly int _batchSize = 10;
        private readonly Random _random = new Random();

        public CampaignProcessor(IServiceScopeFactory scopeFactory, ILogger<CampaignProcessor> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ECMS Campaign Processor started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<ECMSDbContext>();

                    // **STEP 1**: Find queued campaigns ready to process
                    var readyCampaigns = await context.Campaigns
                        .Where(c => c.Status == 1 && c.ScheduledDate <= DateTime.Now)
                        .OrderBy(c => c.ScheduledDate)
                        .ToListAsync(stoppingToken);

                    foreach (var campaign in readyCampaigns)
                    {
                        await ProcessCampaignAsync(context, campaign, stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in main loop");
                }

                // **STEP 6**: Wait 10 seconds
                await Task.Delay(10000, stoppingToken);
            }
        }

        private async Task ProcessCampaignAsync(ECMSDbContext context, Campaign campaign, CancellationToken ct)
        {
            _logger.LogInformation($"Processing campaign {campaign.CampaignId}: {campaign.CampaignName}");

            // **STEP 2**: Mark as Processing
            campaign.Status = 2;
            await context.SaveChangesAsync(ct);

            bool isComplete = false;
            while (!isComplete && !ct.IsCancellationRequested)
            {
                // **STEP 3**: Get pending logs (batch of 10)
                var pendingLogs = await context.CampaignLogs
                    .Where(cl => cl.CampaignId == campaign.CampaignId && cl.Status == 0)
                    .Include(cl => cl.Client)
                    .Take(_batchSize)
                    .ToListAsync(ct);

                if (!pendingLogs.Any())
                {
                    // **STEP 5**: No more pending ? Complete
                    isComplete = true;
                    campaign.Status = 3;
                    await context.SaveChangesAsync(ct);
                    _logger.LogInformation($"Campaign {campaign.CampaignId} completed");
                    break;
                }

                // **STEP 4**: Process batch
                foreach (var log in pendingLogs)
                {
                    await ProcessSingleEmailAsync(log, ct);
                }

                await context.SaveChangesAsync(ct);
            }
        }

        private async Task ProcessSingleEmailAsync(CampaignLog log, CancellationToken ct)
        {
            try
            {
                // Simulate email send (100ms delay)
                await Task.Delay(100, ct);

                // Random 10% failure rate (for testing)
                if (_random.NextDouble() < 0.1)
                {
                    // **Failed**
                    log.Status = 2;
                    log.ErrorMessage = $"SMTP Error: Invalid recipient {log.Client.Email}";
                    log.SentDate = null;
                    _logger.LogWarning($"Failed: {log.Client.Email}");
                }
                else
                {
                    // **Success**
                    log.Status = 1;
                    log.SentDate = DateTime.UtcNow;
                    log.ErrorMessage = null;
                    _logger.LogInformation($"Sent to: {log.Client.Email}");
                }
            }
            catch (Exception ex)
            {
                log.Status = 2;
                log.ErrorMessage = ex.Message;
                _logger.LogError(ex, $"Error processing {log.Client.Email}");
            }
        }
    }

}

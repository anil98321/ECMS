using ECMS.Core.EFStructures;
using Microsoft.EntityFrameworkCore;

namespace ECMS.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            // Windows Service
            builder.Services.AddWindowsService(options =>
            {
                options.ServiceName = "ECMS Email Campaign Processor";
            });

            // EF Core DbContext (Scoped)
            builder.Services.AddDbContext<ECMSDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ECMS"),
                    sql => sql.CommandTimeout(60)
            ));

            // Register your processor
            builder.Services.AddHostedService<CampaignProcessor>();

            var host = builder.Build();
            host.Run();
        }
    }
}
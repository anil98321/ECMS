using ECMS.Core.Configuration;
using ECMS.Core.DataAccess.Campaign;
using ECMS.Core.EFStructures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECMS.Core.DependencyConfiguration;

public static class ServiceRegistrations
{
    public static IServiceCollection AddCampaignService(this IServiceCollection services, IConfiguration configuration)
    {

        var ECMSConfiguration = new ECMSConfiguration();
        configuration.GetSection("ECMS").Bind(ECMSConfiguration, o => o.BindNonPublicProperties = true);

        services.AddSingleton(ECMSConfiguration);

        services.AddScoped<ICampaignRepository, CampaignRepository>();

        services.AddDbContext<ECMSDbContext>(opt =>
            opt.UseSqlServer(
              ECMSConfiguration.ConnectionString,
                b =>
                {
                    b.MigrationsHistoryTable(tableName: "MigrationsHistory");
                    b.MigrationsAssembly("ECMS.Core");
                })
            );

        return services;
    }
}
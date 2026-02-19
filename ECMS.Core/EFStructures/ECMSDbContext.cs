using ECMS.Core.EFStructures.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECMS.Core.EFStructures;



public partial class ECMSDbContext : DbContext
{
    public ECMSDbContext()
    {
    }

    public ECMSDbContext(DbContextOptions<ECMSDbContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public virtual DbSet<Campaign> Campaigns { get; set; }

    public virtual DbSet<CampaignGroup> CampaignGroups { get; set; }

    public virtual DbSet<CampaignLog> CampaignLogs { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientGroup> ClientGroups { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("name=ConnectionString:ECMS");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campaign>(entity =>
        {
            entity.HasKey(e => e.CampaignId).HasName("PK__Campaign__3F5E8D79709B114C");

            entity.HasIndex(e => e.CreatedDate, "IX_Campaigns_CreatedDate");

            entity.HasIndex(e => e.ScheduledDate, "IX_Campaigns_ScheduledDate");

            entity.HasIndex(e => e.Status, "IX_Campaigns_Status");

            entity.Property(e => e.CampaignId).HasColumnName("CampaignID");
            entity.Property(e => e.CampaignName).HasMaxLength(500);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Subject).HasMaxLength(1000);
        });

        modelBuilder.Entity<CampaignGroup>(entity =>
        {
            entity.HasKey(e => e.CampaignGroupId).HasName("PK__Campaign__F3865B3B9F2A44C4");

            entity.HasIndex(e => e.CampaignId, "IX_CampaignGroups_CampaignID");

            entity.HasIndex(e => e.GroupId, "IX_CampaignGroups_GroupID");

            entity.HasIndex(e => new { e.CampaignId, e.GroupId }, "UQ__Campaign__8E17224846CC273F").IsUnique();

            entity.Property(e => e.CampaignGroupId).HasColumnName("CampaignGroupID");
            entity.Property(e => e.CampaignId).HasColumnName("CampaignID");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");

            entity.HasOne(d => d.Campaign).WithMany(p => p.CampaignGroups)
                .HasForeignKey(d => d.CampaignId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CampaignG__Campa__49C3F6B7");

            entity.HasOne(d => d.Group).WithMany(p => p.CampaignGroups)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CampaignG__Group__4AB81AF0");
        });

        modelBuilder.Entity<CampaignLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Campaign__5E5499A8863A9533");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.CampaignId).HasColumnName("CampaignID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ErrorMessage).HasMaxLength(1000);

            entity.HasOne(d => d.Campaign).WithMany(p => p.CampaignLogs)
                .HasForeignKey(d => d.CampaignId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CampaignL__Campa__4D94879B");

            entity.HasOne(d => d.Client).WithMany(p => p.CampaignLogs)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CampaignL__Clien__4E88ABD4");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__E67E1A041E242FF9");

            entity.HasIndex(e => e.ClientName, "IX_Clients_ClientName");

            entity.HasIndex(e => e.Email, "IX_Clients_Email");

            entity.HasIndex(e => e.IsActive, "IX_Clients_IsActive");

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ClientName).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<ClientGroup>(entity =>
        {
            entity.HasKey(e => e.ClientGroupId).HasName("PK__ClientGr__1C856DD3EB8F3DB1");

            entity.HasIndex(e => e.ClientId, "IX_ClientGroups_ClientID");

            entity.HasIndex(e => e.GroupId, "IX_ClientGroups_GroupID");

            entity.HasIndex(e => new { e.ClientId, e.GroupId }, "UQ__ClientGr__5737B5356BC637AB").IsUnique();

            entity.Property(e => e.ClientGroupId).HasColumnName("ClientGroupID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientGroups)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__ClientGro__Clien__403A8C7D");

            entity.HasOne(d => d.Group).WithMany(p => p.ClientGroups)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__ClientGro__Group__412EB0B6");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Groups__149AF30A2243CA6F");

            entity.HasIndex(e => e.GroupName, "IX_Groups_GroupName");

            entity.HasIndex(e => e.IsActive, "IX_Groups_IsActive");

            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.GroupName).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });
    }
}



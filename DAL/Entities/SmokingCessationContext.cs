using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL.Entities;

public partial class SmokingCessationContext : DbContext
{
    public SmokingCessationContext()
    {
    }

    public SmokingCessationContext(DbContextOptions<SmokingCessationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommunityPost> CommunityPosts { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<PackageMembership> PackageMemberships { get; set; }

    public virtual DbSet<Phase> Phases { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<PlanDetail> PlanDetails { get; set; }

    public virtual DbSet<Platform> Platforms { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<Ranking> Rankings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    private string? GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnection"];
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__B19E45C90578AAB1");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId).HasColumnName("Account_ID");
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.Password).HasMaxLength(100);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CmtId).HasName("PK__Comment__0EC3BFF8E3A1377E");

            entity.ToTable("Comment");

            entity.Property(e => e.CmtId).HasColumnName("Cmt_ID");
            entity.Property(e => e.AccountId).HasColumnName("Account_ID");
            entity.Property(e => e.Comment1)
                .HasMaxLength(250)
                .HasColumnName("Comment");
            entity.Property(e => e.CreateTime).HasColumnName("Create_Time");
            entity.Property(e => e.PostId).HasColumnName("Post_ID");

            entity.HasOne(d => d.Account).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Comment__Account__4E88ABD4");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__Comment__Post_ID__4F7CD00D");
        });

        modelBuilder.Entity<CommunityPost>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Communit__5875F74D5F2FBC78");

            entity.ToTable("Community_post");

            entity.Property(e => e.PostId).HasColumnName("Post_ID");
            entity.Property(e => e.AccountId).HasColumnName("Account_ID");
            entity.Property(e => e.Content).HasMaxLength(250);
            entity.Property(e => e.CreateTime).HasColumnName("Create_Time");

            entity.HasOne(d => d.Account).WithMany(p => p.CommunityPosts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Community__Accou__4BAC3F29");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Member__42A68F277042AEB9");

            entity.ToTable("Member");

            entity.HasIndex(e => e.AccountId, "UQ__Member__B19E45C82FD73C39").IsUnique();

            entity.Property(e => e.MemberId).HasColumnName("Member_ID");
            entity.Property(e => e.AccountId).HasColumnName("Account_ID");
            entity.Property(e => e.CostPerCigarette).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.FeedbackContent)
                .HasMaxLength(250)
                .HasColumnName("Feedback_content");
            entity.Property(e => e.FeedbackDate).HasColumnName("Feedback_date");
            entity.Property(e => e.FeedbackRating).HasColumnName("Feedback_rating");
            entity.Property(e => e.GoalTime).HasColumnName("Goal_Time");
            entity.Property(e => e.MedicalHistory).HasMaxLength(250);
            entity.Property(e => e.MostSmokingTime).HasMaxLength(250);
            entity.Property(e => e.Reason).HasMaxLength(250);
            entity.Property(e => e.SmokingTime)
                .HasMaxLength(50)
                .HasColumnName("Smoking_time");
            entity.Property(e => e.StatusProcess)
                .HasMaxLength(15)
                .HasColumnName("Status_Process");

            entity.HasOne(d => d.Account).WithOne(p => p.Member)
                .HasForeignKey<Member>(d => d.AccountId)
                .HasConstraintName("FK__Member__Account___3C69FB99");
        });

        modelBuilder.Entity<PackageMembership>(entity =>
        {
            entity.HasKey(e => e.PackageMembershipId).HasName("PK__Package___DE449F5533508FF2");

            entity.ToTable("Package_membership");

            entity.Property(e => e.PackageMembershipId).HasColumnName("Package_membership_ID");
            entity.Property(e => e.Category).HasMaxLength(250);
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.Price).HasColumnType("decimal(14, 2)");
        });

        modelBuilder.Entity<Phase>(entity =>
        {
            entity.HasKey(e => e.PhaseId).HasName("PK__Phase__5BA26D42037BDD62");

            entity.ToTable("Phase");

            entity.Property(e => e.PhaseId).HasColumnName("PhaseID");
            entity.Property(e => e.PlanId).HasColumnName("Plan_ID");
            entity.Property(e => e.StatusPhase).HasMaxLength(15);

            entity.HasOne(d => d.Plan).WithMany(p => p.Phases)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("FK__Phase__Plan_ID__5535A963");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Plan__9BAF9B23BFDA9342");

            entity.ToTable("Plan");

            entity.HasIndex(e => e.MemberId, "UQ__Plan__42A68F26F44F3A6D").IsUnique();

            entity.Property(e => e.PlanId).HasColumnName("Plan_ID");
            entity.Property(e => e.Clock).HasColumnType("datetime");
            entity.Property(e => e.MemberId).HasColumnName("Member_ID");
            entity.Property(e => e.SaveMoney).HasColumnType("decimal(14, 2)");

            entity.HasOne(d => d.Member).WithOne(p => p.Plan)
                .HasForeignKey<Plan>(d => d.MemberId)
                .HasConstraintName("FK__Plan__Member_ID__45F365D3");
        });

        modelBuilder.Entity<PlanDetail>(entity =>
        {
            entity.HasKey(e => e.PlanDetailId).HasName("PK__Plan_det__35F5BA9BB150EC18");

            entity.ToTable("Plan_detail");

            entity.Property(e => e.PlanDetailId).HasColumnName("Plan_detail_ID");
            entity.Property(e => e.PlanId).HasColumnName("Plan_ID");

            entity.HasOne(d => d.Plan).WithMany(p => p.PlanDetails)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("FK__Plan_deta__Plan___48CFD27E");
        });

        modelBuilder.Entity<Platform>(entity =>
        {
            entity.HasKey(e => e.PlatformId).HasName("PK__Platform__56F2F0730FE95A22");

            entity.ToTable("Platform");

            entity.Property(e => e.PlatformId).HasColumnName("Platform_ID");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Last_Updated");
            entity.Property(e => e.News1Content).HasColumnName("News1_Content");
            entity.Property(e => e.News1Link)
                .HasMaxLength(500)
                .HasColumnName("News1_Link");
            entity.Property(e => e.News1Title)
                .HasMaxLength(255)
                .HasColumnName("News1_Title");
            entity.Property(e => e.News2Content).HasColumnName("News2_Content");
            entity.Property(e => e.News2Link)
                .HasMaxLength(500)
                .HasColumnName("News2_Link");
            entity.Property(e => e.News2Title)
                .HasMaxLength(255)
                .HasColumnName("News2_Title");
            entity.Property(e => e.News3Content).HasColumnName("News3_Content");
            entity.Property(e => e.News3Link)
                .HasMaxLength(500)
                .HasColumnName("News3_Link");
            entity.Property(e => e.News3Title)
                .HasMaxLength(255)
                .HasColumnName("News3_Title");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK__Purchase__543E6DA31F2C7217");

            entity.ToTable("Purchase");

            entity.Property(e => e.PurchaseId).HasColumnName("Purchase_ID");
            entity.Property(e => e.EndDate).HasColumnName("End_date");
            entity.Property(e => e.MemberId).HasColumnName("Member_ID");
            entity.Property(e => e.PackageMembershipId).HasColumnName("Package_membership_ID");
            entity.Property(e => e.StartDate).HasColumnName("Start_date");
            entity.Property(e => e.TimeBuy)
                .HasColumnType("datetime")
                .HasColumnName("Time_BUY");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(14, 2)")
                .HasColumnName("Total_price");

            entity.HasOne(d => d.Member).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__Purchase__Member__412EB0B6");

            entity.HasOne(d => d.PackageMembership).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.PackageMembershipId)
                .HasConstraintName("FK__Purchase__Packag__4222D4EF");
        });

        modelBuilder.Entity<Ranking>(entity =>
        {
            entity.HasKey(e => e.RankingId).HasName("PK__Ranking__582DD5971AFD527D");

            entity.ToTable("Ranking");

            entity.Property(e => e.RankingId).HasColumnName("Ranking_ID");
            entity.Property(e => e.Badge).HasMaxLength(100);
            entity.Property(e => e.MemberId).HasColumnName("Member_ID");
            entity.Property(e => e.TotalScore).HasColumnName("Total_score");

            entity.HasOne(d => d.Member).WithMany(p => p.Rankings)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__Ranking__Member___52593CB8");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__User__B19E45C931369F0F");

            entity.ToTable("User");

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("Account_ID");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Role).HasMaxLength(100);

            entity.HasOne(d => d.Account).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__Account_ID__38996AB5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

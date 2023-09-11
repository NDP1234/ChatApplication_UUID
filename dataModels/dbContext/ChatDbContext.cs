using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using dataModels.Entities;

namespace dataModels.dbContext;

public partial class ChatDbContext : DbContext
{
    public ChatDbContext()
    {
    }

    public ChatDbContext(DbContextOptions<ChatDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Inductionuser> Inductionusers { get; set; }

    public virtual DbSet<Smsmsgtoinductionuser> Smsmsgtoinductionusers { get; set; }

    public virtual DbSet<Smsmsgtouser> Smsmsgtousers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=chatbot_obsolete;user=root;password=1234", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.1.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Inductionuser>(entity =>
        {
            entity.HasKey(e => e.Inductionuserguid).HasName("PRIMARY");

            entity.ToTable("inductionusers");

            entity.Property(e => e.Inductionuserguid).HasColumnName("inductionuserguid");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.IsDelete).HasDefaultValueSql("'0'");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.ModificationTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<Smsmsgtoinductionuser>(entity =>
        {
            entity.HasKey(e => e.InductionUserMsgGuid).HasName("PRIMARY");

            entity.ToTable("smsmsgtoinductionuser");

            entity.HasIndex(e => e.InductionUserId, "FK1_inductionUser");

            entity.HasIndex(e => e.CreatorId, "FK2_creator_for_induction");

            entity.HasIndex(e => e.DeletorId, "FK3_deletor_for_induction");

            entity.HasIndex(e => e.ModificationId, "FK4_modifier_for_induction");

            entity.Property(e => e.InductionUserMsgGuid)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("inductionUserMsgGUID");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.CreatorId).HasDefaultValueSql("''");
            entity.Property(e => e.ModificationTime).HasColumnType("timestamp");
            entity.Property(e => e.Sms).HasColumnType("text");

            entity.HasOne(d => d.Creator).WithMany(p => p.SmsmsgtoinductionuserCreators)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK2_creator_for_induction");

            entity.HasOne(d => d.Deletor).WithMany(p => p.SmsmsgtoinductionuserDeletors)
                .HasForeignKey(d => d.DeletorId)
                .HasConstraintName("FK3_deletor_for_induction");

            entity.HasOne(d => d.InductionUser).WithMany(p => p.Smsmsgtoinductionusers)
                .HasForeignKey(d => d.InductionUserId)
                .HasConstraintName("FK1_inductionUser");

            entity.HasOne(d => d.Modification).WithMany(p => p.SmsmsgtoinductionuserModifications)
                .HasForeignKey(d => d.ModificationId)
                .HasConstraintName("FK4_modifier_for_induction");
        });

        modelBuilder.Entity<Smsmsgtouser>(entity =>
        {
            entity.HasKey(e => e.GuidOfUserMessage).HasName("PRIMARY");

            entity.ToTable("smsmsgtousers");

            entity.HasIndex(e => e.CreatorId, "FK1_creator");

            entity.HasIndex(e => e.ModificationId, "FK2_modifier");

            entity.HasIndex(e => e.DeletorId, "FK3_deletor");

            entity.HasIndex(e => e.UserId, "FK4_toUser");

            entity.Property(e => e.GuidOfUserMessage)
                .HasDefaultValueSql("''")
                .HasColumnName("guidOfUserMessage");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.CreatorId).HasDefaultValueSql("''");
            entity.Property(e => e.ModificationTime).HasColumnType("timestamp");
            entity.Property(e => e.Sms).HasColumnType("text");
            entity.Property(e => e.UserId).HasDefaultValueSql("''");

            entity.HasOne(d => d.Creator).WithMany(p => p.SmsmsgtouserCreators)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK1_creator");

            entity.HasOne(d => d.Deletor).WithMany(p => p.SmsmsgtouserDeletors)
                .HasForeignKey(d => d.DeletorId)
                .HasConstraintName("FK3_deletor");

            entity.HasOne(d => d.Modification).WithMany(p => p.SmsmsgtouserModifications)
                .HasForeignKey(d => d.ModificationId)
                .HasConstraintName("FK2_modifier");

            entity.HasOne(d => d.User).WithMany(p => p.SmsmsgtouserUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK4_toUser");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.GuidforUser).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.GuidforUser).HasColumnName("GUIDForUser");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.DeletionTime).HasColumnType("timestamp");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");
            entity.Property(e => e.IsDelete).HasDefaultValueSql("'0'");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.ModificationTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

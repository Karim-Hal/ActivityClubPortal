using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SampleOneWebAPI.Models;

namespace SampleOneWebAPI.Data;

public partial class ActivityPortalDbContext : DbContext
{
    public ActivityPortalDbContext()
    {
    }

    public ActivityPortalDbContext(DbContextOptions<ActivityPortalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventGuide> EventGuides { get; set; }

    public virtual DbSet<EventMember> EventMembers { get; set; }

    public virtual DbSet<FeedbackPost> FeedbackPosts { get; set; }

    public virtual DbSet<Guide> Guides { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MemberPhoto> MemberPhotos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-S4OK7E9\\MSSQLSERVER01;Database=ActivityPortalDB;Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC070B14F680");

            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Event__3214EC07C553B3DF");

            entity.ToTable("Event");

            entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Destination).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Events)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Event__CategoryI__44FF419A");
        });

        modelBuilder.Entity<EventGuide>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Event_Gu__3214EC07AC943F13");

            entity.ToTable("Event_Guide");

            entity.HasOne(d => d.Event).WithMany(p => p.EventGuides)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Event_Gui__Event__4F7CD00D");

            entity.HasOne(d => d.Guide).WithMany(p => p.EventGuides)
                .HasForeignKey(d => d.GuideId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Event_Gui__Guide__4E88ABD4");
        });

        modelBuilder.Entity<EventMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Event_Me__3214EC07CCA95CC0");

            entity.ToTable("Event_Member");

            entity.HasOne(d => d.Event).WithMany(p => p.EventMembers)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Event_Mem__Event__48CFD27E");

            entity.HasOne(d => d.Member).WithMany(p => p.EventMembers)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Event_Mem__Membe__47DBAE45");
        });

        modelBuilder.Entity<FeedbackPost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Feedback__3214EC073C1E6EFD");

            entity.ToTable("FeedbackPost");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Member).WithMany(p => p.FeedbackPosts)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FeedbackPost_Member");
        });

        modelBuilder.Entity<Guide>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Guide__3214EC072A197B93");

            entity.ToTable("Guide");

            entity.HasIndex(e => e.Email, "UQ__Guide__A9D105345472C325").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Profession).HasMaxLength(255);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Member__3214EC07EA2EDCF5");

            entity.ToTable("Member");

            entity.HasIndex(e => e.Email, "UQ__Member__A9D10534CAA0E676").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.EmergencyNumber).HasMaxLength(20);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.MobileNumber).HasMaxLength(20);
            entity.Property(e => e.Nationality).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Profession).HasMaxLength(255);
        });

        modelBuilder.Entity<MemberPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MemberPh__3214EC07DC10B2FE");

            entity.ToTable("MemberPhoto");

            entity.HasOne(d => d.Member).WithMany(p => p.MemberPhotos)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberPhoto_Member");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC0743DC035C");

            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07CCC46E91");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D105348967CA38").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User_Rol__3214EC07E1DCB144");

            entity.ToTable("User_Role");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Role__RoleI__3D5E1FD2");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Role__UserI__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

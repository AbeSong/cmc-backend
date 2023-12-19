using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TodoProject.DTO;

namespace TodoProject.Models;

public partial class SimpleDbContext : DbContext
{
    public SimpleDbContext()
    {
    }

    public SimpleDbContext(DbContextOptions<SimpleDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Todo> Todos { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

     public virtual DbSet<TodoCount> TodoCounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<TodoCount>(e =>
        {
            e.HasNoKey();
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.ToTable("Permission");

            entity.Property(e => e.PermissionName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.PermissionId });

            entity.ToTable("RolePermission");
        });

        modelBuilder.Entity<Todo>(entity =>
        {
            entity.ToTable("Todo");

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.UserProfile).WithMany(p => p.Todos)
                .HasForeignKey(d => d.UserProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Todo_UserProfile");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.ToTable("UserProfile");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProfile_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

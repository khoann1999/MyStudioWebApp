using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyStudioWebApi.Models
{
    public partial class MyStudioAppContext : DbContext
    {
        public MyStudioAppContext()
        {
        }

        public MyStudioAppContext(DbContextOptions<MyStudioAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountNotification> AccountNotification { get; set; }
        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Scene> Scene { get; set; }
        public virtual DbSet<SceneActor> SceneActor { get; set; }
        public virtual DbSet<SceneTool> SceneTool { get; set; }
        public virtual DbSet<Tool> Tool { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccountNotification>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.NoticationId).HasColumnName("NoticationID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Notication)
                    .WithMany(p => p.AccountNotification)
                    .HasForeignKey(d => d.NoticationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountNotification_Notification");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.AccountNotification)
                    .HasForeignKey(d => d.UserName)
                    .HasConstraintName("FK_AccountNotification_Account");
            });

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.HasOne(d => d.UserNameNavigation)
                    .WithOne(p => p.Actor)
                    .HasForeignKey<Actor>(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Actor_Account");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Scene>(entity =>
            {
                entity.Property(e => e.SceneId).HasColumnName("SceneID");

                entity.Property(e => e.DateBegin).HasColumnType("datetime");

                entity.Property(e => e.DateEnd).HasColumnType("datetime");

                entity.Property(e => e.SceneName).HasMaxLength(50);

                entity.Property(e => e.SceneScript).HasMaxLength(50);
            });

            modelBuilder.Entity<SceneActor>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.SceneId).HasColumnName("SceneID");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Scene)
                    .WithMany(p => p.SceneActor)
                    .HasForeignKey(d => d.SceneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SceneActor_Scene");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.SceneActor)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SceneActor_Actor");
            });

            modelBuilder.Entity<SceneTool>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.SceneId).HasColumnName("SceneID");

                entity.Property(e => e.ToolId).HasColumnName("ToolID");

                entity.HasOne(d => d.Scene)
                    .WithMany(p => p.SceneTool)
                    .HasForeignKey(d => d.SceneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SceneTool_Scene1");

                entity.HasOne(d => d.Tool)
                    .WithMany(p => p.SceneTool)
                    .HasForeignKey(d => d.ToolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SceneTool_Tool");
            });

            modelBuilder.Entity<Tool>(entity =>
            {
                entity.Property(e => e.ToolId).HasColumnName("ToolID");

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.ToolName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

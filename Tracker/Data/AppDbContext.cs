using Microsoft.EntityFrameworkCore;
using Tracker.Models;

namespace Tracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
                entity.Property(p => p.Description);
                entity.Property(p => p.CreatedAt)
                .IsRequired();

                entity.HasQueryFilter(p => !p.IsDeleted);

                entity.HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(150);
                entity.Property(t => t.Description);
                entity.Property(t => t.Priority)
                .IsRequired();
                entity.Property(t => t.Status)
                .IsRequired();
                entity.Property(t => t.CreatedAt)
                .IsRequired();
                entity.HasQueryFilter(t => !t.IsDeleted);
            });
        }
    }
}

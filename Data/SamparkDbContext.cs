using Microsoft.EntityFrameworkCore;
using Sampark.Models;

namespace Sampark.Data
{
    public class SamparkDbContext : DbContext
    {
        public SamparkDbContext(DbContextOptions<SamparkDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectKaryakar> ProjectKaryakars { get; set; }
        public DbSet<ProjectKaryakarPair> ProjectKaryakarPairs { get; set; }
        public DbSet<ProjectFamily> ProjectFamilies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Project relationships
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Karyakars)
                .WithOne(k => k.Project)
                .HasForeignKey(k => k.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.KaryakarPairs)
                .WithOne(k => k.Project)
                .HasForeignKey(k => k.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Families)
                .WithOne(f => f.Project)
                .HasForeignKey(f => f.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure ProjectKaryakar to Person relationship
            modelBuilder.Entity<ProjectKaryakar>()
                .HasOne(k => k.KaryakarPerson)
                .WithMany()
                .HasForeignKey(k => k.KaryakarPersonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure ProjectKaryakarPair to Person relationships
            modelBuilder.Entity<ProjectKaryakarPair>()
                .HasOne(p => p.PrimaryKaryakar)
                .WithMany()
                .HasForeignKey(p => p.PrimaryKaryakarPersonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectKaryakarPair>()
                .HasOne(p => p.SecondaryKaryakar)
                .WithMany()
                .HasForeignKey(p => p.SecondaryKaryakarPersonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure ProjectKaryakarPair relationships
            modelBuilder.Entity<ProjectKaryakarPair>()
                .HasMany(p => p.AssignedFamilies)
                .WithOne(f => f.AssignedKaryakarPair)
                .HasForeignKey(f => f.AssignedKaryakarPairId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure enums to be stored as strings
            modelBuilder.Entity<Project>()
                .Property(p => p.ReminderFrequency)
                .HasConversion<string>();

            modelBuilder.Entity<ProjectKaryakarPair>()
                .Property(p => p.PairType)
                .HasConversion<string>();

            modelBuilder.Entity<Entity>()
               .HasOne(e => e.Parent)
               .WithMany(e => e.Children)
               .HasForeignKey(e => e.parent_entity_id)
               .OnDelete(DeleteBehavior.Restrict);
                }
    }
}

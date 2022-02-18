using JiraManagement.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace JiraManagement.Model.Context
{
    public class JiraContext : DbContext
    {
        public JiraContext(DbContextOptions<JiraContext> options) : base(options)
        {

        }

        public DbSet<Dashboard> Dashboards { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Issue> Issues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasNoDiscriminator()
                .ToContainer("Users")
                .HasPartitionKey(da => da.Id)
                .HasKey(da => new { da.Id });

            modelBuilder.Entity<Issue>()
                .HasNoDiscriminator()
                .ToContainer("Issues")
                .HasPartitionKey(da => da.Id)
                .HasKey(da => new { da.Id });

            modelBuilder.Entity<Dashboard>()
                .HasNoDiscriminator()
                .ToContainer("Dashboards")
                .HasPartitionKey(da => da.Id)
                .HasKey(da => new { da.Id });

            base.OnModelCreating(modelBuilder);
        }
    }
}

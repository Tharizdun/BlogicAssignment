using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogicAssignment.Models;

namespace BlogicAssignment.Data
{
    public class BlogicAssignmentDbContext : DbContext
    {
        public BlogicAssignmentDbContext(DbContextOptions<BlogicAssignmentDbContext> options) : base(options)
        {  
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Advisor> Advisors { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<AdvisorContract> AdvisorContracts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdvisorContract>()
                .HasKey(t => new { t.AdvisorID, t.ContractID });

            modelBuilder.Entity<AdvisorContract>()
                .HasOne(pt => pt.Advisor)
                .WithMany(p => p.AdvisedContracts)
                .HasForeignKey(pt => pt.AdvisorID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AdvisorContract>()
                .HasOne(pt => pt.Contract)
                .WithMany(t => t.Advisors)
                .HasForeignKey(pt => pt.ContractID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

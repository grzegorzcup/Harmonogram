using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options):base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public override int SaveChanges()
        {
            var entires = ChangeTracker
                                .Entries()
                                .Where(e => e.Entity is AuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entire in entires)
            {
                ((AuditableEntity)entire.Entity).LastModified = DateTime.UtcNow;

                if(entire.State == EntityState.Added) 
                {
                    ((AuditableEntity)entire.Entity).Created = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                    new Role()
                    {
                        Id = 1,
                        Name = "System",
                    },
                    new Role()
                    {
                        Id = 2,
                        Name = "Admin",
                    },
                    new Role()
                    {
                        Id = 3,
                        Name = "Moderator",
                    },
                    new Role()
                    {
                        Id= 4,
                        Name= "Teacher",
                    },
                    new Role()
                    {
                        Id = 5,
                        Name = "User",
                    }
                );
        }
    }
}

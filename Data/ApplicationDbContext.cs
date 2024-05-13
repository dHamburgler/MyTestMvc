using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EurofinsEvents.Models;
using System.IO;
using Microsoft.Extensions.Logging;

namespace EurofinsEvents.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
       
        //public DbSet<PartyGuest> EventAttendances { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Guests)
                .WithMany(e => e.EventList);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Votes)
                .WithMany(e => e.Votes)
                .UsingEntity<EventVote>();

            modelBuilder.Entity<Event>().ToTable("Event");
        }
    }
}
using Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Game>()
                .HasOne(s => s.Team)
                .WithMany(l => l.Games)
                .HasForeignKey(s => s.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Game>()
                .HasOne(s => s.GuestTeam)
                .WithMany()
                .HasForeignKey(s => s.GuestTeamId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
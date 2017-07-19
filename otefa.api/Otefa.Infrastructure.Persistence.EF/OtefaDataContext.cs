using Otefa.Domain.Model.Entities;
using System.Data.Entity;

namespace Otefa.Infrastructure.Persistence
{
    public class OtefaDataContext : DbContext
    {

        public OtefaDataContext()
            : base("name=Otefa")
        {
        }

        public OtefaDataContext(string connectionStringName)
            : base("name=" + connectionStringName)
        {
        }

        public DbSet<Player> Players { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Headquarter> Headquarters { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<MatchTeam> MatchTeams { get; set; }

        public DbSet<PlayerDetails> PlayerDetails { get; set; }

        public DbSet<Tournament> Tournaments { get; set; }

        public DbSet<TournamentDate> TournamentDates { get; set; }

        public DbSet<TournamentTeamPlayers> TournamentTeamPlayers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Tournament>()
            .HasMany(m => m.TeamPlayersList)
            .WithMany()
            .Map(m => m.ToTable("TournamentTeamsPlayers"));

            modelBuilder.Entity<Tournament>()
            .HasMany(m => m.HeadquartersList)
            .WithMany();

        }

    }
}
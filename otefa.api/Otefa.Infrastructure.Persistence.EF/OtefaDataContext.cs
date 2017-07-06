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
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            
        }

    }
}
using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using System.Linq;

namespace Otefa.Infrastructure.Persistence
{
    public class PlayerRepositoryEF : EFRepositoryBase<Player>, IPlayerRepository
    {

        public PlayerRepositoryEF(IRepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public Player GetByDni(string dni)
        {
            return GetDbSet().Where(x => x.Dni.Equals(dni)).SingleOrDefault();
        }
           
    }
}
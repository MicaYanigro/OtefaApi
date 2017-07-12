using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using System.Linq;

namespace Otefa.Infrastructure.Persistence
{
    public class TournamentRepositoryEF : EFRepositoryBase<Tournament>, ITournamentRepository
    {

        public TournamentRepositoryEF(IRepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public Tournament GetByName(string name)
        {
            return GetDbSet().Where(x => x.Name.Equals(name)).SingleOrDefault();
        }

    }
}
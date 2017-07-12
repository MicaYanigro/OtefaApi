using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.Persistence;
using System.Linq;

namespace Otefa.Infrastructure.Persistence
{
    public class MatchRepositoryEF : EFRepositoryBase<Match>, IMatchRepository
    {

        public MatchRepositoryEF(IRepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        
    }
}
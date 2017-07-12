using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.Persistence;
using System.Linq;

namespace Otefa.Infrastructure.Persistence
{
    public class MatchTeamRepositoryEF : EFRepositoryBase<MatchTeam>, IMatchTeamRepository
    {

        public MatchTeamRepositoryEF(IRepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        
    }
}
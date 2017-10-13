using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Otefa.Infrastructure.Persistence
{
    public class MatchRepositoryEF : EFRepositoryBase<Match>, IMatchRepository
    {

        public MatchRepositoryEF(IRepositoryContext repositoryContext) : base(repositoryContext)
        {

         
        }

        public async Task<List<MatchTeam>> GetOrderedTeams(int matchId)
        {
            var result = await GetDbSet().Where(x => x.Id == matchId).SelectMany(x => x.MatchTeamList).OrderByDescending(mt => mt.Goals).ToListAsync();
            return result;
        }
        
    }
}
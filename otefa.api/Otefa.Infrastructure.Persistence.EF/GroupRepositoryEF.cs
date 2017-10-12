using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Otefa.Infrastructure.Persistence
{
    public class GroupRepositoryEF : EFRepositoryBase<Group>, IGroupRepository
    {

        public GroupRepositoryEF(IRepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public object GetMatchesByTournament(int tournamentID)
        {
            var result = GetDbSet().Where(x => x.Tournament.Id == tournamentID).SelectMany(x => x.MatchesList).OrderBy(x => x.Round).ToList().GroupBy(g => g.Group);
            return result;
        }


    }
}
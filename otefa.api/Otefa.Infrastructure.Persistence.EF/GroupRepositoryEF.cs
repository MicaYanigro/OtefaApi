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

        public List<Team> GetTeams(Group group)
        {
            var result = GetDbSet().Where(x => x.Id == group.Id).SelectMany(x => x.TeamList).ToList();
            return result;
        }

        public List<Match> GetMatches(Group group)
        {
            var result = GetDbSet().Where(x => x.Id == group.Id).SelectMany(x => x.MatchesList).OrderBy(x => x.Round).ToList();
            return result;
        }
    }
}
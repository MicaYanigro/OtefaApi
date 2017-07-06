using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.Persistence;
using System.Linq;

namespace Otefa.Infrastructure.Persistence
{
    public class TeamRepositoryEF : EFRepositoryBase<Team>, ITeamRepository
    {

        public TeamRepositoryEF(IRepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public Team GetByName(string name)
        {
            return GetDbSet().Where(x => x.Name.Equals(name)).SingleOrDefault();
        }
           
    }
}
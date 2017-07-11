using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.Persistence;
using System.Linq;

namespace Otefa.Infrastructure.Persistence
{
    public class HeadquarterRepositoryEF : EFRepositoryBase<Headquarter>, IHeadquarterRepository
    {

        public HeadquarterRepositoryEF(IRepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public Headquarter GetByName(string name)
        {
            return GetDbSet().Where(x => x.Name.Equals(name)).SingleOrDefault();
        }

    }
}
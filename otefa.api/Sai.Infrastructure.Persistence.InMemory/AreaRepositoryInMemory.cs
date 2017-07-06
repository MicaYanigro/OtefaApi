using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Repositories;
using System.Linq;

namespace Sai.Infrastructure.Persistence
{
    public class AreaRepositoryInMemory : InMemoryRepositoryBase<Area>, IAreaRepository
    {
        public Area GetByBriefDescription(string briefDescription)
        {
            return innerList.Where(x => x.BriefDescription.Equals(briefDescription)).SingleOrDefault();
        }
    }
}
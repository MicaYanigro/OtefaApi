using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Repositories;
using System.Linq;

namespace Sai.Infrastructure.Persistence
{
    public class CategoryRepositoryInMemory : InMemoryRepositoryBase<Category>, ICategoryRepository
    {
        public Category GetByBriefDescription(string briefDescription)
        {
            return innerList.Where(x => x.BriefDescription.Equals(briefDescription)).SingleOrDefault();
        }
    }
}
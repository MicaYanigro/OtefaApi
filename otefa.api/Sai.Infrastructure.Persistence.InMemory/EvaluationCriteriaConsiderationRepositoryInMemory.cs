using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Repositories;
using System.Linq;

namespace Sai.Infrastructure.Persistence
{
    public class EvaluationCriteriaConsiderationRepositoryInMemory : InMemoryRepositoryBase<EvaluationCriteriaConsideration>, IEvaluationCriteriaConsiderationRepository
    {
        public EvaluationCriteriaConsideration GetByDescription(string description)
        {
            return innerList.Where(x => x.Description == description).SingleOrDefault();
        }
    }
}
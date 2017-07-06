using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Repositories;
using System.Linq;

namespace Sai.Infrastructure.Persistence
{
    public class AgentRepositoryInMemory : InMemoryRepositoryBase<Agent>, IAgentRepository
    {
        public Agent GetByCuilCuit(string cuilcuit)
        {
            return innerList.Where(x => x.Cuil.Equals(cuilcuit)).SingleOrDefault();
        }
    }
}
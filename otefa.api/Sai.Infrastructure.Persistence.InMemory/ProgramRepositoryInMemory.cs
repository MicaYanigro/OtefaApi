using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Repositories;

namespace Sai.Infrastructure.Persistence
{
    public class ProgramRepositoryInMemory : InMemoryRepositoryBase<Program>, IRepository<Program>
    {
    }
}
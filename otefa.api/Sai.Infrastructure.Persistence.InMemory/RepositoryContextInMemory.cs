using Sai.Domain.Model.Repositories;

namespace Sai.Infrastructure.Persistence
{
    public class RepositoryContextInMemory : IRepositoryContext
    {

        public int Commit()
        {
            return 0;
        }

        public void Dispose()
        {
        }

    }
}
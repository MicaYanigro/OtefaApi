using System;
using System.Threading.Tasks;

namespace Otefa.Domain.Model.Repositories
{
    public interface IRepositoryContext : IDisposable
    {
        Task<int> Commit();

        int CommitNoAsync();
    }
}
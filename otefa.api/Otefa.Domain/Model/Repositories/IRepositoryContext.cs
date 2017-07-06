using System;

namespace Otefa.Domain.Model.Repositories
{
    public interface IRepositoryContext : IDisposable
    {
        int Commit();
    }
}
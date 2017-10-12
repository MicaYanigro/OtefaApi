using Otefa.Domain.Model.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Otefa.Domain.Model.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T GetById(int id);
        Task<T> GetByIDAsync(int? id);
        IQueryable<T> All();
        IQueryable<T> AllReadOnly();
        IRepositoryContext Context { get; }
    }
}
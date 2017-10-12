using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Otefa.Infrastructure.Persistence
{
    public abstract class EFRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {

        protected IRepositoryContext repositoryContext;
        internal OtefaDataContext otefaDataContext;

        public EFRepositoryBase(IRepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
            otefaDataContext = ((RepositoryContextEF)repositoryContext).CreateDataContext();
            otefaDataContext.Database.Initialize(true);


        }

        protected DbSet<TEntity> GetDbSet()
        {
            return otefaDataContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            GetDbSet().Add(entity);
        }

        public void Delete(TEntity entity)
        {

            if (otefaDataContext.Entry(entity).State == EntityState.Detached)
            {
                GetDbSet().Attach(entity);
            }

            GetDbSet().Remove(entity);

        }

        public void Update(TEntity entity)
        {
            GetDbSet().Attach(entity);

            otefaDataContext.Entry(entity).State = EntityState.Modified;
        }

        TEntity IRepository<TEntity>.GetById(int id)
        {
            return GetDbSet().Find(id);
        }

        public async Task<TEntity> GetByIDAsync(int? id)
        {
            return await GetDbSet().FindAsync(id);
        }

        IQueryable<TEntity> IRepository<TEntity>.All()
        {
            return GetDbSet().AsQueryable();
        }

        public IQueryable<TEntity> AllReadOnly()
        {
            return GetDbSet().AsNoTracking();
        }

        public IRepositoryContext Context
        {
            get
            {
                return this.repositoryContext;
            }
        }

    }
}
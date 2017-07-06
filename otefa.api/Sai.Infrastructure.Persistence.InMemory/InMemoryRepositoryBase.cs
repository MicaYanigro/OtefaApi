using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sai.Infrastructure.Persistence
{
    public abstract class InMemoryRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {

        protected List<TEntity> innerList = new List<TEntity>();
        private RepositoryContextInMemory repositoryContextInMemory = new RepositoryContextInMemory();

        public void Add(TEntity entity)
        {
            innerList.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            innerList.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            innerList[innerList.FindIndex(x => x.Id == entity.Id)] = entity;
        }

        TEntity IRepository<TEntity>.GetById(int id)
        {
            return innerList.Where(x => x.Id == id).SingleOrDefault();
        }

        IQueryable<TEntity> IRepository<TEntity>.All()
        {
            return innerList.AsQueryable();
        }

        IQueryable<TEntity> IRepository<TEntity>.AllReadOnly()
        {
            return innerList.AsQueryable();
        }

        public IRepositoryContext Context
        {
            get
            {
                return repositoryContextInMemory;
            }
        }

    }
}
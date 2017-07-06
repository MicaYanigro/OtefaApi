using Otefa.Domain.Model.Repositories;
using System;

namespace Otefa.Infrastructure.Persistence
{
    public class RepositoryContextEF : IRepositoryContext
    {

        private OtefaDataContext otefaDataContext;
        private bool disposed = false;

        internal OtefaDataContext CreateDataContext()
        {
            return (otefaDataContext ?? (otefaDataContext = new OtefaDataContext()));
        }

        public int Commit()
        {
            return otefaDataContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

            if (!disposed)
            {
                if (disposing)
                {
                    otefaDataContext.Dispose();
                }
            }

            disposed = true;

        }

    }
}
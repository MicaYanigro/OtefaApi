using Otefa.Domain.Model.Repositories;
using System;
using System.Threading.Tasks;

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

        public async Task<int> Commit()
        {
            return await otefaDataContext.SaveChangesAsync();
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
using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.Persistence;
using System.Linq;

namespace Otefa.Infrastructure.Persistence
{
    public class NewRepositoryEF : EFRepositoryBase<New>, INewRepository
    {

        public NewRepositoryEF(IRepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

   
    }
}
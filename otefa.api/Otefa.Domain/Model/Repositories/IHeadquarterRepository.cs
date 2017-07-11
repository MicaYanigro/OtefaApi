using Otefa.Domain.Model.Entities;

namespace Otefa.Domain.Model.Repositories
{
    public interface IHeadquarterRepository : IRepository<Headquarter>
    {
        Headquarter GetByName(string name);

    }
}
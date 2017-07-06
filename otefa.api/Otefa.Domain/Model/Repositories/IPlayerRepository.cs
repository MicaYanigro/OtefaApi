using Otefa.Domain.Model.Entities;

namespace Otefa.Domain.Model.Repositories
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Player GetByDni(string dni);

    }
}
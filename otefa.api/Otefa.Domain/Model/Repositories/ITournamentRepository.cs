using Otefa.Domain.Model.Entities;

namespace Otefa.Domain.Model.Repositories
{
    public interface ITournamentRepository : IRepository<Tournament>
    {
        Tournament GetByName(string name);

    }
}
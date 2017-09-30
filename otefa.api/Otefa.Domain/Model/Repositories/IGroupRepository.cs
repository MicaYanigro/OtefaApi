using Otefa.Domain.Model.Entities;

namespace Otefa.Domain.Model.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {
        object GetMatchesByTournament(int tournamentID);

    }
}
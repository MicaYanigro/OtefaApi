using Otefa.Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otefa.Domain.Model.Repositories
{
    public interface IMatchRepository : IRepository<Match>
    {
        Task<List<MatchTeam>> GetOrderedTeams(int matchId);
    }
}
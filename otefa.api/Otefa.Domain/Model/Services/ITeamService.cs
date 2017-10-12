using Otefa.Domain.Model.Entities;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace Otefa.Domain.Model.Services
{
    public interface ITeamService
    {
        Team FindTeamByName(string name);

        Task<Team> Create(string name, string teamDelegate, string shieldImage, string teamImage, IEnumerable<int> playersList);

        Task Update(int teamID, string name, string teamDelegate, string shieldImage, string teamImage, IEnumerable<int> playersList);

        IEnumerable<Team> GetAll();

        Task<Team> GetByID(int id);

        IEnumerable<ExpandoObject> GetTeamStadistics(int teamID);

        ExpandoObject GetHistoricalStadistics(int teamID);
        IEnumerable<Match> GetUpcomingMatches(int teamID);
    }
}
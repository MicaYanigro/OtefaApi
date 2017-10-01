using Otefa.Domain.Model.Entities;
using System.Collections.Generic;
using System.Dynamic;

namespace Otefa.Domain.Model.Repositories
{
    public interface IMatchTeamRepository : IRepository<MatchTeam>
    {
        IEnumerable<ExpandoObject> GetTournamentPositions(int tournamentID);
        IEnumerable<ExpandoObject> GetTeamStadistics(int teamID);
        ExpandoObject GetHistoricalStadistics(int teamID);
        IEnumerable<Match> GetUpcomingMatches(int teamID);
        List<List<ExpandoObject>> GetTournamentPositionsByGroups(int tournamentID);
    }
}
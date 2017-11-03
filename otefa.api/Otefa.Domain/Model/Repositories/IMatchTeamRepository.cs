﻿using Otefa.Domain.Model.Entities;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace Otefa.Domain.Model.Repositories
{
    public interface IMatchTeamRepository : IRepository<MatchTeam>
    {
        IEnumerable<ExpandoObject> GetTournamentPositions(int tournamentID);
        IEnumerable<ExpandoObject> GetTeamStadistics(int teamID);
        ExpandoObject GetHistoricalStadistics(int teamID);
        IEnumerable<Match> GetUpcomingMatches(int teamID);
        List<ExpandoObject> GetTournamentPositionsByGroups(int tournamentID);
        Task<List<ExpandoObject>> GetTournamentMatchesByGroups(int tournamentID);
        List<ExpandoObject> GetScorersByTournament(int tournamentID);
    }
}
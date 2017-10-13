using Otefa.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace Otefa.Domain.Model.Services
{
    public interface IMatchService
    {
        Task<Match> Create(int tournamentID, int groupID, int headquarterID, DateTime date, int round, IEnumerable<int> teamsID);
        void LoadResults(int matchID, int matchTeamID, int goals, int againstGoals, bool hasBonusPoint, int figureID, IEnumerable<ExpandoObject> playersDetails);
        IEnumerable<Match> GetAll();
        Task Update(int matchID, int headquarterID, DateTime date);
        Task<Match> GetById(int matchId);
        object GetByTournamentId(int tournamentId);
    }
}
using Otefa.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Otefa.Domain.Model.Services
{
    public interface IMatchService
    {
        Match Create(int tournamentID, int headquarterID, DateTime date, int round, IEnumerable<int> teamsID);
        void LoadResults(int matchID, int matchTeamID, int goals, int againstGoals, bool hasBonusPoint, int figureID, IEnumerable<ExpandoObject> playersDetails);
        IEnumerable<Match> GetAll();
        void Update(int matchID, int headquarterID, DateTime date);
    }
}
using Otefa.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Otefa.Domain.Model.Services
{
    public interface IMatchService
    {
        Match Create(int headquarterID, DateTime date, IEnumerable<int> teamsID);
        void LoadResults(int matchID, int matchTeamID, int goals, bool hasBonusPoint, int figureID, IEnumerable<ExpandoObject> playersDetails);
        IEnumerable<Match> GetAll();
        void Update(int matchID, int headquarterID, DateTime date);
    }
}
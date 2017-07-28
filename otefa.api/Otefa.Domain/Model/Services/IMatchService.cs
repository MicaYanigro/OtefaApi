using Otefa.Domain.Model.Entities;
using System;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Services
{
    public interface IMatchService
    {
        Match Create(int headquarterID, DateTime date, IEnumerable<int> teamsID);
        void Update(int matchID, int headquarterID, DateTime date);
        IEnumerable<Match> GetAll();
    }
}
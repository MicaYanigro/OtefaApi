using Otefa.Domain.Model.Entities;
using System;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Factories
{
    public interface IMatchFactory
    {
        Match Create(int tournamentID, Headquarter headquarter, DateTime date, IEnumerable<int> teamsID);
    }
}
using Otefa.Domain.Model.Entities;
using System;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Factories
{
    public interface ITournamentFactory
    {
        Tournament Create(string name, int tournamentFormat, int clasificationFormat, string rules, string prices, IEnumerable<int> headquarters, IEnumerable<DateTime> tournamentDates);
    }
}
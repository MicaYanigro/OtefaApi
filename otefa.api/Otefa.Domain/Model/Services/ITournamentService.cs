using Otefa.Domain.Model.Entities;
using System;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Services
{
    public interface ITournamentService
    {
        Tournament FindTournamentByName(string name);

        Tournament Create(string name, int tournamentFormat, int clasificationFormat, string rules, string prices, IEnumerable<int> headquarters, IEnumerable<DateTime> tournamentDates, Dictionary<int, List<int>> teamsPlayers);

        void Update(int tournamentID, string name, int tournamentFormat, int clasificationFormat, string rules, string prices, IEnumerable<int> headquarters, IEnumerable<DateTime> tournamentDates);

        IEnumerable<Tournament> GetAll();
    }
}
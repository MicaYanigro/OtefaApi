using Otefa.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Otefa.Domain.Model.Services
{
    public interface ITournamentService
    {
        Tournament FindTournamentByName(string name);

        Tournament Create(string name, int tournamentFormat, int clasificationFormat, string rules, string prices, IEnumerable<int> headquarters, IEnumerable<DateTime> tournamentDates, Dictionary<int, List<int>> teamsPlayers);

        void Update(int tournamentID, string name, int tournamentFormat, int clasificationFormat, string rules, string prices, IEnumerable<int> headquarters, IEnumerable<DateTime> tournamentDates, Dictionary<int, List<int>> teamsPlayers);

        IEnumerable<Tournament> GetAll();

        Tournament GetByID(int id);

        IEnumerable<ExpandoObject> GetTournamentPositions(int tournamentID);

        List<List<ExpandoObject>> GetTournamentPositionsByGroups(int tournamentID);

        void GenerateFixture(int tournamentID);

        object GetAllMatches(int tournamentID);

        void AddGroups(int tournamentID, string name, List<int> teams);
    }
}
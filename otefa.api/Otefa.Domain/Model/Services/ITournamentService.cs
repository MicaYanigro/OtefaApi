using Otefa.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace Otefa.Domain.Model.Services
{
    public interface ITournamentService
    {
        Tournament FindTournamentByName(string name);

        Task<Tournament> Create(string name, int tournamentFormat, int clasificationFormat, string rules, string prices, IEnumerable<int> headquarters, IEnumerable<DateTime> tournamentDates, Dictionary<int, List<int>> teamsPlayers);

        Task Update(int tournamentID, string name, int tournamentFormat, int clasificationFormat, string rules, string prices, IEnumerable<int> headquarters, IEnumerable<DateTime> tournamentDates, Dictionary<int, List<int>> teamsPlayers);

        IEnumerable<Tournament> GetAll();

        Task<Tournament> GetByID(int id);

        IEnumerable<ExpandoObject> GetTournamentPositions(int tournamentID);

        List<ExpandoObject> GetTournamentPositionsByGroups(int tournamentID);

        void GenerateFixture(int tournamentID);

        void GenerateFixtureByGroup(int tournamentID, int groupID);

        Task<object> GetAllMatchesByTournament(int tournamentID);

        Task AddGroups(int tournamentID, string name, List<int> teams);
    }
}
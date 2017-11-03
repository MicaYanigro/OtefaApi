using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Factories;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Otefa.Domain.Model.Services
{
    public class TournamentService : ServiceBase, ITournamentService
    {

        [Injectable]
        public ITournamentFactory TournamentFactory { get; set; }

        [Injectable]
        public ITournamentRepository TournamentRepository { get; set; }

        [Injectable]
        public IHeadquarterRepository HeadquarterRepository { get; set; }

        [Injectable]
        public IMatchTeamRepository MatchTeamRepository { get; set; }

        [Injectable]
        public IFixtureGenerator FixtureGenerator { get; set; }

        [Injectable]
        public IMatchRepository MatchRepository { get; set; }

        [Injectable]
        public IGroupRepository GroupRepository { get; set; }

        [Injectable]
        public ITeamRepository TeamRepository { get; set; }


        public async Task<Tournament> GetByID(int id)
        {
            return await TournamentRepository.GetByIDAsync(id);
        }

        public Tournament FindTournamentByName(string name)
        {
            return TournamentRepository.GetByName(name);

        }

        public async Task<object> GetAllMatchesByTournament(int tournamentID)
        {
            var tournament = await GetByID(tournamentID);
            var result = await MatchTeamRepository.GetTournamentMatchesByGroups(tournamentID);
            return result;
            //  return TournamentRepository.GetMatchesByTournament(tournamentID);
             
        }

        public async Task AddGroups(int tournamentID, string name, List<int> teams)
        {
            var tournament = await GetByID(tournamentID);
            var group = new Group(name);
            foreach (var teamID in teams)
            {
                var team = await TeamRepository.GetByIDAsync(teamID);
                group.AddTeam(team);
            }

            tournament.AddGroup(group);
            GroupRepository.Add(group);
            TournamentRepository.Update(tournament);
            await TournamentRepository.Context.Commit();
        }


        public async Task<Tournament> Create(string name, int tournamentFormat, int clasificationFormat, string rules, string prices, IEnumerable<int> headquarters, IEnumerable<DateTime> tournamentDates, Dictionary<int, List<int>> teamsPlayers)
        {
            {

                var tournament = TournamentFactory.Create(name, tournamentFormat, clasificationFormat, rules, prices, headquarters, tournamentDates, teamsPlayers);

                TournamentRepository.Add(tournament);
                await TournamentRepository.Context.Commit();

                return tournament;
            }
        }

        public void GenerateFixture(int tournamentID)
        {
            var tournament = TournamentRepository.GetById(tournamentID);

            var result = FixtureGenerator.CreateMatches(tournament);

            foreach (var match in result)
            {
                MatchRepository.Add(match);
            }

            TournamentRepository.Update(tournament);
            TournamentRepository.Context.CommitNoAsync();
        }

        public void GenerateFixtureByGroup(int tournamentID, int groupID)
        {
            var tournament = TournamentRepository.GetById(tournamentID);
            var group = GroupRepository.GetById(groupID);

            var result = FixtureGenerator.CreateMatchesByGroup(tournament, group);

            foreach (var match in result)
            {
                MatchRepository.Add(match);
            }

            TournamentRepository.Update(tournament);
            TournamentRepository.Context.CommitNoAsync();
        }

        public async Task Update(int tournamentID, string name, int tournamentFormat, int clasificationFormat, string rules, string prices, IEnumerable<int> headquarters, IEnumerable<DateTime> tournamentDates, Dictionary<int, List<int>> teamsPlayers)
        {
            var Tournament = await TournamentRepository.GetByIDAsync(tournamentID);

            var headquarterList = new List<Headquarter> { };
            var ttpList = new List<TeamPlayers>();

            foreach (var headquarterID in headquarters)
            {
                var headquarter = await HeadquarterRepository.GetByIDAsync(headquarterID);
                headquarterList.Add(headquarter);
            }

            var tournamentDateList = new List<TournamentDate> { };

            foreach (var tournamentDate in tournamentDates)
            {
                var tournamentDateEntity = new TournamentDate(tournamentDate);
                tournamentDateList.Add(tournamentDateEntity);

            }

            foreach (var dictionary in teamsPlayers)
            {

                var teamID = dictionary.Key;
                var players = dictionary.Value;

                var team = await Container.Current.Resolve<ITeamRepository>().GetByIDAsync(teamID);

                var teamPlayers = new TeamPlayers(team);

                foreach (var playerID in players)
                {
                    var player = await Container.Current.Resolve<IPlayerRepository>().GetByIDAsync(playerID);
                    teamPlayers.AddPlayer(player);
                }
                ttpList.Add(teamPlayers);
            }

            Tournament.Update(name, (TournamentFormat)tournamentFormat, (ClasificationFormat)clasificationFormat, rules, prices, headquarterList, tournamentDateList, ttpList);

            TournamentRepository.Update(Tournament);
            await TournamentRepository.Context.Commit();
        }

        public IEnumerable<Tournament> GetAll()
        {
            return TournamentRepository.All();

        }

        public IEnumerable<ExpandoObject> GetTournamentPositions(int tournamentID)
        {

            return MatchTeamRepository.GetTournamentPositions(tournamentID);

        }

        public List<ExpandoObject> GetTournamentPositionsByGroups(int tournamentID)
        {

            return MatchTeamRepository.GetTournamentPositionsByGroups(tournamentID);

        }

        public List<ExpandoObject> GetTournamentScorers(int tournamentID)
        {

            return MatchTeamRepository.GetScorersByTournament(tournamentID);

        }

    }
}
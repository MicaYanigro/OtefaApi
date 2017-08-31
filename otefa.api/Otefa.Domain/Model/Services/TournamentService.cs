using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Factories;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

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




        public Tournament GetByID(int id)
        {
            return TournamentRepository.GetById(id);

        }

        public Tournament FindTournamentByName(string name)
        {
            return TournamentRepository.GetByName(name);

        }

        public IEnumerable<Match> GetAllMatches(int tournamentID)
        {
            var tournament = GetByID(tournamentID);
            return tournament.GetMatches();
        }

        public Tournament Create(string name, int tournamentFormat, int clasificationFormat, string rules, string prices, IEnumerable<int> headquarters, IEnumerable<DateTime> tournamentDates, Dictionary<int, List<int>> teamsPlayers)
        {
            {

                var tournament = TournamentFactory.Create(name, tournamentFormat, clasificationFormat, rules, prices, headquarters, tournamentDates, teamsPlayers);

                TournamentRepository.Add(tournament);
                TournamentRepository.Context.Commit();

                return tournament;
            }
        }

        public void GenerateFixture(int tournamentID)
        {
            var tournament = TournamentRepository.GetById(tournamentID);
            var teamsPlayersList = tournament.GetTeamPlayers();
            var teamsList = teamsPlayersList.Select(x => x.Team).ToList();
            //var fixture = FixtureGenerator.GenerateRoundRobin(teamsList.Count());

            var result = FixtureGenerator.CreateMatches(teamsList, tournament);

            foreach (var match in result)
            {
                MatchRepository.Add(match);
            }

            TournamentRepository.Update(tournament);
            TournamentRepository.Context.Commit();
        }

        public void Update(int tournamentID, string name, int tournamentFormat, int clasificationFormat, string rules, string prices, IEnumerable<int> headquarters, IEnumerable<DateTime> tournamentDates, Dictionary<int, List<int>> teamsPlayers)
        {
            var Tournament = TournamentRepository.GetById(tournamentID);

            var headquarterList = new List<Headquarter> { };
            var ttpList = new List<TeamPlayers>();

            foreach (var headquarterID in headquarters)
            {
                var headquarter = HeadquarterRepository.GetById(headquarterID);
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

                var team = Container.Current.Resolve<ITeamRepository>().GetById(teamID);

                var teamPlayers = new TeamPlayers(team);

                foreach (var playerID in players)
                {
                    var player = Container.Current.Resolve<IPlayerRepository>().GetById(playerID);
                    teamPlayers.AddPlayer(player);
                }
                ttpList.Add(teamPlayers);
            }

            Tournament.Update(name, (TournamentFormat)tournamentFormat, (ClasificationFormat)clasificationFormat, rules, prices, headquarterList, tournamentDateList, ttpList);

            TournamentRepository.Update(Tournament);
            TournamentRepository.Context.Commit();
        }

        public IEnumerable<Tournament> GetAll()
        {
            return TournamentRepository.All();

        }

        public IEnumerable<ExpandoObject> GetTournamentPositions(int tournamentID)
        {

            return MatchTeamRepository.GetTournamentPositions(tournamentID);

        }

    }
}
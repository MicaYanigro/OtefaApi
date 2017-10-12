using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otefa.Domain.Model.Factories
{
    public class TournamentFactory : ITournamentFactory
    {

        public Tournament Create(string name, int tournamentFormat, int clasificationFormat, string rules, string prices, IEnumerable<int> headquarters, IEnumerable<DateTime> tournamentDates, Dictionary<int, List<int>> teamsPlayers)
        {

            var tournament = new Tournament(name, (TournamentFormat)tournamentFormat, (ClasificationFormat)clasificationFormat, rules, prices);

            foreach (var headquarterID in headquarters)
            {
                var headquarter = Container.Current.Resolve<IHeadquarterRepository>().GetById(headquarterID);
                tournament.AddHeadquarter(headquarter);
            }

            foreach (var tournamentDate in tournamentDates)
            {
                var tournamentDateEntity = new TournamentDate(tournamentDate);
                tournament.AddTournamentDate(tournamentDateEntity);
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

                tournament.AddTeamPlayers(teamPlayers);
            }

            return tournament;
        }
    }
}
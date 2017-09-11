using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Otefa.Infrastructure.Persistence
{
    public class MatchTeamRepositoryEF : EFRepositoryBase<MatchTeam>, IMatchTeamRepository
    {

        public MatchTeamRepositoryEF(IRepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<ExpandoObject> GetTournamentPositions(int tournamentID)
        {

            var TeamGroups = GetDbSet().Where(x => x.Tournament.Id == tournamentID).GroupBy(x => x.Team);
            var items = new List<ExpandoObject>();

            foreach (var team in TeamGroups)
            {
                if (team.Key.Name != "Bye")
                {
                    var teamName = team.Key.Name;
                    int? teamPoints = team.Sum(x => x.FinalPoints);
                    var playedGames = team.Where(x => x.FinalPoints != null).Count();
                    var wonGames = team.Where(x => x.Result == MatchResult.Win).Count();
                    var drawGames = team.Where(x => x.Result == MatchResult.Draw).Count();
                    var looseGames = team.Where(x => x.Result == MatchResult.Loose).Count();
                    var totalGoals = team.Sum(x => x.Goals);
                    var againstGoals = team.Sum(x => x.AgainstGoals);
                    var difGoal = totalGoals - againstGoals;


                    dynamic item = new ExpandoObject();

                    item.Team = teamName;
                    item.FinalPoints = teamPoints;
                    item.PlayedGames = playedGames;
                    item.WonGames = wonGames;
                    item.DrawGames = drawGames;
                    item.LooseGames = looseGames;
                    item.Goals = totalGoals;
                    item.AgainstGoals = againstGoals;
                    item.DifGoal = difGoal;

                    items.Add(item);
                }
            }

            var result = items.OrderByDescending(x => ((IDictionary<string, object>)x)["FinalPoints"]);

            return result;

        }

        public IEnumerable<ExpandoObject> GetTeamStadistics(int teamID)
        {

            var PlayersDetails = GetDbSet().Where(x => x.Team.Id == teamID).SelectMany(x => x.PlayersDetails).GroupBy(x => x.Player);

            var items = new List<ExpandoObject>();

            foreach (var player in PlayersDetails)
            {
                var playerName = player.Key.Name;
                var playedGames = player.Where(x => x.Played == true).Count();
                var totalGoals = player.Sum(x => x.Goals);
                var redCards = player.Where(x => x.Card == Card.Red).Count();
                var yellowCards = player.Where(x => x.Card == Card.Yellow).Count();
                var figure = player.Where(x => x.MatchTeam.Match.Figure.Id == player.Key.Id).Count();


                dynamic item = new ExpandoObject();

                item.Player = playerName;
                item.PlayedGames = playedGames;
                item.Goals = totalGoals;
                item.RedCards = redCards;
                item.YellowCards = yellowCards;
                item.Figure = figure;

                items.Add(item);

            }

            return items;

        }


        public ExpandoObject GetHistoricalStadistics(int teamID)
        {

            var MatchesList = GetDbSet().Where(x => x.Team.Id == teamID);

            var playedGames = MatchesList.Where(x => x.FinalPoints != null).Count();
            var wonGames = MatchesList.Where(x => x.Result == MatchResult.Win).Count();
            var drawGames = MatchesList.Where(x => x.Result == MatchResult.Draw).Count();
            var looseGames = MatchesList.Where(x => x.Result == MatchResult.Loose).Count();
            var totalGoals = MatchesList.Sum(x => x.Goals);
            var againstGoals = MatchesList.Sum(x => x.AgainstGoals);
            var difGoal = totalGoals - againstGoals;


            dynamic item = new ExpandoObject();

            item.PlayedGames = playedGames;
            item.WonGames = wonGames;
            item.DrawGames = drawGames;
            item.LooseGames = looseGames;
            item.Goals = totalGoals;
            item.AgainstGoals = againstGoals;
            item.DifGoal = difGoal;

            return item;
        }

        public IEnumerable<Match> GetUpcomingMatches(int teamID)
        {
            var MatchesList = GetDbSet().Where(x => x.Team.Id == teamID).Select(x => x.Match);
            return MatchesList.Where(x => x.Date >= DateTime.Now).OrderBy(x => x.Date);
        }
    }
}

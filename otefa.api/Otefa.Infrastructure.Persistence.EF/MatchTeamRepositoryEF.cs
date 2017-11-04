using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;


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
                if (team.Key.Name != "Libre")
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
                    var bonus = team.Where(x => x.HasBonusPoint == true).Count();

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
                    item.Bonus = bonus;

                    items.Add(item);
                }
            }

            var result = items.OrderByDescending(x => ((IDictionary<string, object>)x)["FinalPoints"]);

            return result;

        }

        public List<ExpandoObject> GetTournamentPositionsByGroups(int tournamentID)
        {
            var tournament = GetDbSet().Select(x => x.Tournament).Where(x => x.Id == tournamentID).FirstOrDefault();

            var FinalList = new List<ExpandoObject>();
            var groups = tournament.GetGroups();

            foreach (var group in groups)
            {
                var groupList = new List<ExpandoObject>();
                var TeamGroups = GetDbSet().Where(x => x.Tournament.Id == tournamentID && x.Group.Id == group.Id).GroupBy(x => x.Team);

                foreach (var team in TeamGroups)
                {
                    if (team.Key.Name != "Libre")
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
                        var bonus = team.Where(x => x.HasBonusPoint == true).Count();


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
                        item.Bonus = bonus;

                        groupList.Add(item);
                    }
                }

                var result = groupList.OrderByDescending(x => ((IDictionary<string, object>)x)["FinalPoints"])
                                      .ThenByDescending(x => ((IDictionary<string, object>)x)["DifGoal"])
                                      .ThenByDescending(x => ((IDictionary<string, object>)x)["Goals"])
                                      .ThenBy(x => ((IDictionary<string, object>)x)["AgainstGoals"]).ToList();

                dynamic groupList2 = new ExpandoObject();
                groupList2.Group = group.Name;
                groupList2.Positions = result;

                FinalList.Add(groupList2);

            }

            return FinalList;
        }

        public List<ExpandoObject> GetScorersByTournament(int tournamentID)
        {
            var tournament = GetDbSet().Select(x => x.Tournament).Where(x => x.Id == tournamentID).FirstOrDefault();

            var Teams = GetDbSet().Where(x => x.Tournament.Id == tournamentID).GroupBy(x => x.Team);
            var playersList = new List<ExpandoObject>();

            foreach (var team in Teams)
            {
                var players = team.SelectMany(x => x.PlayersDetails.GroupBy(p => p.Player)).ToList();

                foreach (var player in players)
                {
                    if (player.Key.Name != "Libre")
                    {
                        var playerName = player.Key.Name + player.Key.LastName;
                        var teamName = team.Key.Name;
                        int? goals = player.Sum(x => x.Goals);

                        dynamic item = new ExpandoObject();

                        item.Player = playerName;
                        item.Team = teamName;
                        item.Goals = goals;

                        playersList.Add(item);
                    }
                }
            }

            var result = playersList.OrderByDescending(x => ((IDictionary<string, object>)x)["Goals"]).ToList();

            return result;
        }

        public async Task<List<TournamentGroupMatches>> GetTournamentMatchesByGroups(int tournamentID)
        {
            var tournament = await GetDbSet().Select(x => x.Tournament).Where(x => x.Id == tournamentID).FirstOrDefaultAsync();

            var FinalList = new List<ExpandoObject>();
            var resultList = new List<TournamentGroupMatches>();

            var groups = tournament.GetGroups();

            foreach (var group in groups)
            {
                var matchesList = new List<ExpandoObject>();
                var Matches = GetDbSet().Where(x => x.Tournament.Id == tournamentID && x.Group.Id == group.Id).Select(x => x.Match).Distinct();

                var groupMatches = new TournamentGroupMatches(group.Name);
                var matchesGroupList = new List<FixtureGroupMatches>();

                foreach (var match in Matches)
                {

                    var round = match.Round;
                    var team1 = match.MatchTeamList.First().Team.Name;
                    var team2 = match.MatchTeamList.Last().Team.Name;
                    var date = match.Date;
                    var goals1 = match.MatchTeamList.First().Goals;
                    var goals2 = match.MatchTeamList.Last().Goals;
                    var id = match.GetId();

                    dynamic item = new ExpandoObject();

                    item.Id = id;
                    item.Round = round;
                    item.Team1 = team1;
                    item.Team2 = team2;
                    item.Date = date;
                    item.Goals1 = goals1;
                    item.Goals2 = goals2;

                    var fixtureGroupMatch = new FixtureGroupMatches(round, team1, team2, date, goals1, goals2);

                    matchesGroupList.Add(fixtureGroupMatch);

                    //matchesList.Add(item);
                }

                var result2 = matchesGroupList.OrderByDescending(x => x.Round).ToList();
                foreach (var match in result2)
                {
                    groupMatches.addMatch(match);
                }

                ////////////////
                var result = matchesList.OrderByDescending(x => ((IDictionary<string, object>)x)["Round"]).ToList();

                dynamic groupList2 = new ExpandoObject();
                groupList2.Group = group.Name;
                groupList2.Matches = result;

                FinalList.Add(groupList2);
                ////////////////

                resultList.Add(groupMatches);
            }

            return resultList.OrderByDescending(x => x.GroupMatches.Select(m => m.Date).Distinct().FirstOrDefault()).ToList();
            //return resultList;
           // return FinalList;
        }

        public IEnumerable<ExpandoObject> GetTeamStadistics(int teamID)
        {

            var PlayersDetails = GetDbSet().Where(x => x.Team.Id == teamID).SelectMany(x => x.PlayersDetails).GroupBy(x => x.Player);

            var items = new List<ExpandoObject>();

            foreach (var player in PlayersDetails)
            {
                var playerName = player.Key.Name + " " + player.Key.LastName;
                var playedGames = player.Where(x => x.Played == true).Count();
                var totalGoals = player.Sum(x => x.Goals);
                var redCards = player.Where(x => x.Card == Card.Red).Count();
                var yellowCards = player.Where(x => x.Card == Card.Yellow).Count();
                var figure = GetDbSet().Where(x => x.Team.Id == teamID).Where(x => x.Match.Figure.Id == player.Key.Id).Count();

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

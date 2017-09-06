using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otefa.Domain.Model.Services
{
    public class FixtureGenerator : ServiceBase, IFixtureGenerator
    {

        [Injectable]
        public ITeamRepository TeamRepository { get; set; }

        private const int BYE = -1;

        public int[,] GenerateRoundRobin(int num_teams)
        {
            if (num_teams % 2 == 0)
                return GenerateRoundRobinEven(num_teams);
            else
                return GenerateRoundRobinOdd(num_teams);
        }

        // Return an array where results(i, j) gives
        // the opponent of team i in round j.
        // Note: num_teams must be odd.
        private int[,] GenerateRoundRobinOdd(int num_teams)
        {
            int n2 = (int)((num_teams - 1) / 2);
            int[,] results = new int[num_teams, num_teams];

            // Initialize the list of teams.
            int[] teams = new int[num_teams];
            for (int i = 0; i < num_teams; i++) teams[i] = i;

            // Start the rounds.
            for (int round = 0; round < num_teams; round++)
            {
                for (int i = 0; i < n2; i++)
                {
                    int team1 = teams[n2 - i];
                    int team2 = teams[n2 + i + 1];
                    results[team1, round] = team2;
                    results[team2, round] = team1;
                }

                // Set the team with the bye.
                results[teams[0], round] = BYE;

                // Rotate the array.
                RotateArray(teams);
            }


            return results;
        }

        // Rotate the entries one position.
        private void RotateArray(int[] teams)
        {
            int tmp = teams[teams.Length - 1];
            Array.Copy(teams, 0, teams, 1, teams.Length - 1);
            teams[0] = tmp;
        }

        private int[,] GenerateRoundRobinEven(int num_teams)
        {
            // Generate the result for one fewer teams.
            int[,] results = GenerateRoundRobinOdd(num_teams - 1);

            // Copy the results into a bigger array,
            // replacing the byes with the extra team.
            int[,] results2 = new int[num_teams, num_teams - 1];
            for (int team = 0; team < num_teams - 1; team++)
            {
                for (int round = 0; round < num_teams - 1; round++)
                {
                    if (results[team, round] == BYE)
                    {
                        // Change the bye to the new team.
                        results2[team, round] = num_teams - 1;
                        results2[num_teams - 1, round] = team;
                    }
                    else
                    {
                        results2[team, round] = results[team, round];
                    }
                }
            }

            return results2;
        }


        //public IEnumerable<Match> CreateMatchess(int[,] fixture, IEnumerable<Team> teams, Tournament tournament)
        //{
        //    List<Match> MatchesList = new List<Match>();


        //    for (int team1ID = 0; team1ID < teams.Count() - 1; team1ID++)
        //    {
        //        for (int round = 0; round < teams.Count() - 1; round++)
        //        {
        //            var team2ID = fixture[team1ID, round];

        //            var team1 = teams.ElementAt(team1ID);
        //            var team2 = teams.ElementAt(team2ID);

        //            var match = new Match(null, DateTime.Now);

        //            var matchTeam1 = new MatchTeam(tournament, match, team1, null, null, null);
        //            var matchTeam2 = new MatchTeam(tournament, match, team2, null, null, null);

        //            match.AddMatchTeam(matchTeam1);
        //            match.AddMatchTeam(matchTeam2);

        //            MatchesList.Add(match);
        //            tournament.AddMatch(match);
        //        }
        //    }

        //    return MatchesList;
        //}

        public IEnumerable<Match> CreateMatches(List<Team> ListTeam, Tournament tournament)
        {
            List<Match> MatchesList = new List<Match>();

            if (ListTeam.Count() % 2 != 0)
            {
                ListTeam.Add(new Team("Bye", "Bye", null, null));
            }

            int numDays = (ListTeam.Count() - 1);
            int halfSize = ListTeam.Count() / 2;

            List<Team> teams = new List<Team>();

            teams.AddRange(ListTeam); // Copy all the elements.
            teams.RemoveAt(0); // To exclude the first team.

            int teamsSize = teams.Count;

            for (int day = 0; day < numDays; day++)
            {
                Console.WriteLine("Day {0}", (day + 1));

                int teamIdx = day % teamsSize;

                Console.WriteLine("{0} vs {1}", teams[teamIdx], ListTeam[0]);

                for (int idx = 1; idx < halfSize; idx++)
                {
                    int firstTeam = (day + idx) % teamsSize;
                    int secondTeam = (day + teamsSize - idx) % teamsSize;
                    Console.WriteLine("{0} vs {1}", teams[firstTeam], teams[secondTeam]);

                    var match = new Match(null, DateTime.Now, day + 1);

                    var matchTeam1 = new MatchTeam(tournament, match, teams[firstTeam], null, null, null);
                    var matchTeam2 = new MatchTeam(tournament, match, teams[secondTeam], null, null, null);

                    match.AddMatchTeam(matchTeam1);
                    match.AddMatchTeam(matchTeam2);

                    MatchesList.Add(match);
                    tournament.AddMatch(match);

                }
            }

            return MatchesList;
        }

    }
}

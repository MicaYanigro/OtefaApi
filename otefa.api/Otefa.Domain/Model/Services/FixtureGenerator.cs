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
        
        public IEnumerable<Match> CreateMatches(Tournament tournament)
        {
            var headquarterDefault = tournament.GetHeadquarter().FirstOrDefault();
            List<Match> MatchesList = new List<Match>();
            var groupsList = tournament.GetGroups();

            foreach (var group in groupsList)
            {
                var ListTeam = group.GetTeams().ToList();

                if (ListTeam.Count() % 2 != 0)
                {
                    ListTeam.Add(new Team("Libre", "Libre", null, null));
                }

                int numDays = (ListTeam.Count() - 1);
                int halfSize = ListTeam.Count() / 2;

                List<Team> teams = new List<Team>();

                teams.AddRange(ListTeam); // Copy all the elements.
                teams.RemoveAt(0); // To exclude the first team.

                int teamsSize = teams.Count;

                for (int day = 0; day < numDays; day++)
                {
                    var currentDay = day + 1;
                    int teamIdx = day % teamsSize;

                    var firstMatch = new Match(headquarterDefault, DateTime.Now, currentDay);

                    var firstMatchTeam1 = new MatchTeam(tournament, group, firstMatch, teams[teamIdx], null, null, null);
                    var firstMatchTeam2 = new MatchTeam(tournament, group, firstMatch, ListTeam[0], null, null, null);

                    firstMatch.AddMatchTeam(firstMatchTeam1);
                    firstMatch.AddMatchTeam(firstMatchTeam2);

                    MatchesList.Add(firstMatch);
                    group.AddMatch(firstMatch);

                    for (int idx = 1; idx < halfSize; idx++)
                    {
                        int firstTeam = (day + idx) % teamsSize;
                        int secondTeam = (day + teamsSize - idx) % teamsSize;

                        var match = new Match(headquarterDefault, DateTime.Now, currentDay);

                        var matchTeam1 = new MatchTeam(tournament, group, match, teams[firstTeam], null, null, null);
                        var matchTeam2 = new MatchTeam(tournament, group, match, teams[secondTeam], null, null, null);

                        match.AddMatchTeam(matchTeam1);
                        match.AddMatchTeam(matchTeam2);

                        MatchesList.Add(match);
                        group.AddMatch(match);

                    }
                }
            }

            return MatchesList;
        }

        public IEnumerable<Match> CreateMatchesByGroup(Tournament tournament, Group group)
        {
            var headquarterDefault = tournament.GetHeadquarter().FirstOrDefault();
            List<Match> MatchesList = new List<Match>();

            var ListTeam = group.GetTeams().ToList();

            if (ListTeam.Count() % 2 != 0)
            {
                ListTeam.Add(new Team("Libre", "Libre", null, null));
            }

            int numDays = (ListTeam.Count() - 1);
            int halfSize = ListTeam.Count() / 2;

            List<Team> teams = new List<Team>();

            teams.AddRange(ListTeam); // Copy all the elements.
            teams.RemoveAt(0); // To exclude the first team.

            int teamsSize = teams.Count;

            for (int day = 0; day < numDays; day++)
            {
                var currentDay = day + 1;
                int teamIdx = day % teamsSize;

                var firstMatch = new Match(headquarterDefault, DateTime.Now, currentDay);

                var firstMatchTeam1 = new MatchTeam(tournament, group, firstMatch, teams[teamIdx], null, null, null);
                var firstMatchTeam2 = new MatchTeam(tournament, group, firstMatch, ListTeam[0], null, null, null);

                firstMatch.AddMatchTeam(firstMatchTeam1);
                firstMatch.AddMatchTeam(firstMatchTeam2);

                MatchesList.Add(firstMatch);
                group.AddMatch(firstMatch);

                for (int idx = 1; idx < halfSize; idx++)
                {
                    int firstTeam = (day + idx) % teamsSize;
                    int secondTeam = (day + teamsSize - idx) % teamsSize;

                    var match = new Match(headquarterDefault, DateTime.Now, currentDay);

                    var matchTeam1 = new MatchTeam(tournament, group, match, teams[firstTeam], null, null, null);
                    var matchTeam2 = new MatchTeam(tournament, group, match, teams[secondTeam], null, null, null);

                    match.AddMatchTeam(matchTeam1);
                    match.AddMatchTeam(matchTeam2);

                    MatchesList.Add(match);
                    group.AddMatch(match);

                }
            }


            return MatchesList;
        }

    }
}

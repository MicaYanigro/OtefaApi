using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.Persistence;
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
                var teamName = team.Key.Name;
                int? teamPoints = 0;
                var playedGames = team.Count();
                var wonGames = team.Where(x => x.Result == MatchResult.Win).Count();
                var drawGames = team.Where(x => x.Result == MatchResult.Draw).Count();
                var looseGames = team.Where(x => x.Result == MatchResult.Loose).Count();
                var totalGoals = team.Sum(x => x.Goals);
                var againstGoals = team.Sum(x => x.AgainstGoals);

                foreach (var details in team)
                {
                    teamPoints = teamPoints + details.FinalPoints;
                }


                dynamic item = new ExpandoObject();

                item.Team = teamName;
                item.FinalPoints = teamPoints;
                item.PlayedGames = playedGames;
                item.WonGames = wonGames;
                item.DrawGames = drawGames;
                item.LooseGames = looseGames;

                items.Add(item);

            }

            var result = items.OrderByDescending(x => ((IDictionary<string, object>)x)["FinalPoints"]);

            return result;

        }


    }
}

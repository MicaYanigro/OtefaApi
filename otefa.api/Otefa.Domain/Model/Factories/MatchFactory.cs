using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Factories
{
    public class MatchFactory : IMatchFactory
    {

        public Match Create(Headquarter headquarter, DateTime date, IEnumerable<int> teamsID)
        {

            var Match = new Match(headquarter, date);

            foreach (var teamID in teamsID)
            {
                var team = Container.Current.Resolve<ITeamRepository>().GetById(teamID);
                var matchTeam = new MatchTeam(team, null, null, null);
                Match.AddMatchTeam(matchTeam);
            }

            return Match;
        }
    }
}
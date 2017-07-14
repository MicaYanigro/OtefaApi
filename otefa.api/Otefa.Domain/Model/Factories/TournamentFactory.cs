using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Factories
{
    public class TournamentFactory : ITournamentFactory
    {

        public Tournament Create(string name, int tournamentFormat, int clasificationFormat, string rules, string prices, IEnumerable<int> headquarters, IEnumerable<DateTime> tournamentDates)
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


            return tournament;
        }
    }
}
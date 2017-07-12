using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Factories;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;

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


        public Tournament FindTournamentByName(string name)
        {
            return TournamentRepository.GetByName(name);

        }

        public Tournament Create(string name, int tournamentFormat, int clasificationFormat, string rules, string prices, IEnumerable<int> headquarters, IEnumerable<DateTime> tournamentDates)
        {
            {

                var tournament = TournamentFactory.Create(name, tournamentFormat, clasificationFormat, rules, prices, headquarters, tournamentDates);

                TournamentRepository.Add(tournament);
                TournamentRepository.Context.Commit();

                return tournament;
            }
        }

        public void Update(int tournamentID, string name, int tournamentFormat, int clasificationFormat, string rules, string prices, IEnumerable<int> headquarters, IEnumerable<DateTime> tournamentDates)
        {
            var Tournament = TournamentRepository.GetById(tournamentID);

            var headquarterList = new List<Headquarter> { };

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
            Tournament.Update(name, (TournamentFormat)tournamentFormat, (ClasificationFormat)clasificationFormat, rules, prices, headquarterList, tournamentDateList);

            TournamentRepository.Update(Tournament);
            TournamentRepository.Context.Commit();
        }

        public IEnumerable<Tournament> GetAll()
        {
            return TournamentRepository.All();

        }

    }
}
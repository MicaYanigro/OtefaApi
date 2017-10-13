using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Otefa.Infrastructure.Persistence
{
    public class TournamentRepositoryEF : EFRepositoryBase<Tournament>, ITournamentRepository
    {

        public TournamentRepositoryEF(IRepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public Tournament GetByName(string name)
        {
            return GetDbSet().Where(x => x.Name.Equals(name)).SingleOrDefault();
        }

        public object GetMatchesByTournament(int tournamentID)
        {
            var result = GetDbSet().Where(x => x.Id == tournamentID).SelectMany(x => x.GroupList.SelectMany(g => g.MatchesList)).OrderBy(m => m.Round).ToList();
            var groups = result.GroupBy(b => b.Group);
            return groups;
        }
       
    }
}
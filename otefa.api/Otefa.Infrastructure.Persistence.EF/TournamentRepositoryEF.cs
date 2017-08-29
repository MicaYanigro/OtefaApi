using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using System;
using System.Collections.Generic;
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

        //public List<Team> GetPositionList(int tournamentID)
        //{
        //    var MatchTeamList = GetDbSet().Where(x => x.Id == tournamentID).SelectMany(x => x.MatchesList).SelectMany(m => m.MatchTeamList).Sum(s => s.FinalPoints);

        //    var tuple = MatchTeamList.Select(x => new { Team = x.Team, Points = x.FinalPoints })
        //    .AsEnumerable()
        //    .Select(x => Tuple.Create(x.Team, x.Points))
        //    .AsEnumerable();

        //    return tuple;

        //    return GetDbSet().Where(x => x.Id == tournamentID).SelectMany(x => x.MatchesList).SelectMany(m => m.MatchTeamList).OrderBy(mt => mt.FinalPoints).ToList();
        //}
    }
}
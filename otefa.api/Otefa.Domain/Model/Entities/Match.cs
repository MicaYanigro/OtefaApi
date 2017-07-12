using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otefa.Domain.Model.Entities
{

    public partial class Match : Entity
    {

        private Headquarter headquarter;
        private DateTime date;

        public Match(Headquarter headquarter, DateTime Date)
        {
            Headquarter = headquarter;
            Date = date;
        }


        public virtual Headquarter Headquarter
        {

            get
            {
                return headquarter;
            }

            protected set
            {
                headquarter = value;
            }

        }
        public DateTime Date
        {

            get
            {
                return date;
            }

            protected set
            {
                date = value;
            }

        }

        public IEnumerable<MatchTeam> GetTeams()
        {
            return matchTeamList;
        }

        public void AddMatchTeam(MatchTeam matchTeam)
        {
            this.matchTeamList.Add(matchTeam);
        }

        public void Update()
        {

            throw new NotImplementedException();

        }


    }

    public class MatchMetadata
    {
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }
    }

    [MetadataType(typeof(MatchMetadata))]
    public partial class Match
    {
        protected Match()
        { }


        [Obsolete]
        public virtual ICollection<MatchTeam> MatchTeamList { get; set; }
        protected ICollection<MatchTeam> matchTeamList
        {
#pragma warning disable 612, 618
            get
            {
                return MatchTeamList;
            }
            set
            {
                MatchTeamList = value;
            }
#pragma warning restore 612, 618
        }

    }

}
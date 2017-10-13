using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Repositories;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Otefa.Domain.Model.Entities
{

    public partial class Match : Entity
    {

        private Headquarter headquarter;
        private DateTime date;
        private Player figure;
        private int round;


        public Match(Headquarter headquarter, DateTime date, int round)
        {
            Headquarter = headquarter;
            Date = date;
            Round = round;
            matchTeamList = new Collection<MatchTeam>();
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


        public virtual Player Figure
        {

            get
            {
                return figure;
            }

            protected set
            {
                figure = value;
            }

        }

        public int Round
        {

            get
            {
                return round;
            }

            protected set
            {
                round = value;
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

        public void Update(Headquarter headquarter, DateTime date)
        {
            this.headquarter = headquarter;
            this.date = date;
        }


        public void CalculateFinalPoints()
        {
            //var teams = await Container.Current.Resolve<IMatchRepository>().GetOrderedTeams(this.GetId());

            var team1 = matchTeamList.OrderByDescending(x => x.Goals).First();
            var team2 =  matchTeamList.OrderByDescending(x => x.Goals).Last();

        //    var team1 = teams[0]; //matchTeamList.OrderByDescending(x => x.Goals).First();
          //  var team2 = teams[1]; // matchTeamList.OrderByDescending(x => x.Goals).Last();

            if (team1.Goals > team2.Goals)
            {
                team1.SetFinalPoints(MatchResult.Win);
                team2.SetFinalPoints(MatchResult.Loose);
                
            }
            else if (team1.Goals < team2.Goals)
            {
                team1.SetFinalPoints(MatchResult.Loose);
                team2.SetFinalPoints(MatchResult.Win);
            }
            else
            {
                team1.SetFinalPoints(MatchResult.Draw);
                team2.SetFinalPoints(MatchResult.Draw);
            }

        }

        public void UpdateMatchTeam(int matchTeamID, int goals, int againstGoals, bool hasBonusPoint, Player figure, List<PlayerDetails> playerDetailsList)
        {
            if (figure != null)
                Figure = figure;

            var matchTeam = Container.Current.Resolve<IMatchTeamRepository>().GetById(matchTeamID);
            matchTeam.Update(goals, againstGoals, hasBonusPoint, playerDetailsList);
        
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

        [Obsolete]
        public Group Group { get; set; }

        [NotMapped]
        private Group group
        {
#pragma warning disable 612, 618
            get
            {
                return Group;
            }
            set
            {
                Group = value;
            }
#pragma warning restore 612, 618
        }

    }

}
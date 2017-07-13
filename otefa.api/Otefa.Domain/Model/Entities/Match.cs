﻿using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
        
        public void CalculateFinalPoints()
        {
            var team1 = matchTeamList.OrderByDescending(x => x.Goals).First();
            var team2 = matchTeamList.OrderByDescending(x => x.Goals).Last();

            if (team1.Goals > team2.Goals)
            {
                team1.SetFinalPoints(MatchResult.Win);
                team2.SetFinalPoints(MatchResult.Loose);
            }
            else if(team1.Goals < team2.Goals)
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
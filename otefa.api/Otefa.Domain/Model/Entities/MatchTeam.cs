using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Otefa.Domain.Model.Entities
{

    public partial class MatchTeam : Entity
    {

        private Team team;
        private int? goals;
        private bool? hasBonusPoint;
        private int? finalPoints;

        public MatchTeam(Team team, int? goals, bool? hasBonusPoint)
        {
            Team = team;
            Goals = goals;
            HasBonusPoint = hasBonusPoint;

        }

        public virtual Team Team
        {

            get
            {
                return team;
            }

            protected set
            {
                team = value;
            }

        }
        public int? Goals
        {

            get
            {
                return goals;
            }

            protected set
            {
                goals = value;
            }

        }

        public bool? HasBonusPoint
        {

            get
            {
                return hasBonusPoint;
            }

            protected set
            {
                hasBonusPoint = value;
            }

        }

        public int? FinalPoints
        {

            get
            {
                return finalPoints;
            }

            protected set
            {
                finalPoints = value;
            }

        }

        public void Update()
        {

            throw new NotImplementedException();
        }

    }

    public partial class MatchTeam
    {
        protected MatchTeam()
        { }


        [Obsolete]
        public virtual ICollection<PlayerDetails> PlayersDetails { get; set; }
        protected ICollection<PlayerDetails> playersDetails
        {
#pragma warning disable 612, 618
            get
            {
                return PlayersDetails;
            }
            set
            {
                PlayersDetails = value;
            }
#pragma warning restore 612, 618
        }

    }

}
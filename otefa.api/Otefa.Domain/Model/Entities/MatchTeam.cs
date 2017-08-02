using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            playersDetails = new Collection<PlayerDetails>();
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


        public void SetFinalPoints(MatchResult matchResult)
        {
            switch (matchResult)
            {
                case MatchResult.Win:
                    this.FinalPoints = 3;
                    break;
                case MatchResult.Draw:
                    this.FinalPoints = 2;
                    break;
                case MatchResult.Loose:
                    this.FinalPoints = 1;
                    break;

            }
            if (HasBonusPoint == true)
                this.FinalPoints = +1;

        }

        public void AddPlayerDetails(PlayerDetails playerDetails)
        {
            this.playersDetails.Add(playerDetails);
        }


        public void Update(int goals, bool hasBonusPoint, IEnumerable<PlayerDetails> playerDetailsList)
        {
            this.goals = goals;
            this.hasBonusPoint = hasBonusPoint;

            this.playersDetails.Clear();
            foreach (var playerDetail in playerDetailsList)
            {
                playersDetails.Add(playerDetail);
            }
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
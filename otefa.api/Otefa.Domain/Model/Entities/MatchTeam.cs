using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otefa.Domain.Model.Entities
{

    public partial class MatchTeam : Entity
    {

        private Team team;
        private int? goals;
        private int? againstGoals;
        private bool? hasBonusPoint;
        private int? finalPoints;
        private MatchResult result;

        public MatchTeam(Tournament tournament, Group group, Match match, Team team, int? goals, int? againstGoals, bool? hasBonusPoint)
        {
            this.tournament = tournament;
            this.match = match;
            this.group = group;

            Team = team;
            Goals = goals;
            AgainstGoals = againstGoals;
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

        public int? AgainstGoals
        {

            get
            {
                return againstGoals;
            }

            protected set
            {
                againstGoals = value;
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

        public MatchResult Result
        {

            get
            {
                return result;
            }

            protected set
            {
                result = value;
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
                this.FinalPoints = this.FinalPoints + 1;

            this.result = matchResult;
        }

        public void AddPlayerDetails(PlayerDetails playerDetails)
        {
            this.playersDetails.Add(playerDetails);
        }


        public void Update(int goals, int againstGoals, bool hasBonusPoint, IEnumerable<PlayerDetails> playerDetailsList)
        {
            this.goals = goals;
            this.againstGoals = againstGoals;
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

        [Obsolete]
        public virtual Tournament Tournament { get; set; }

        [NotMapped]
        private Tournament tournament
        {
#pragma warning disable 612, 618
            get
            {
                return Tournament;
            }
            set
            {
                Tournament = value;
            }
#pragma warning restore 612, 618
        }

        [Obsolete]
        public virtual Group Group { get; set; }

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

        [Obsolete]
        public virtual Match Match { get; set; }

        [NotMapped]
        private Match match
        {
#pragma warning disable 612, 618
            get
            {
                return Match;
            }
            set
            {
                Match = value;
            }
#pragma warning restore 612, 618
        }




    }

}
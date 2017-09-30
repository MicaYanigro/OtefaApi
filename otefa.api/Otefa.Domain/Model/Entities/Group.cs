using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otefa.Domain.Model.Entities
{

    public partial class Group : Entity
    {

        private string name;


        public Group(string name)
        {
            Name = name;
            teamList = new Collection<Team>();
            matchesList = new Collection<Match>();
        }

        public string Name
        {

            get
            {
                return name;
            }

            protected set
            {
                name = value;
            }

        }
        
        public IEnumerable<Team> GetTeams()
        {
            return teamList;
        }

        public void AddTeam(Team team)
        {
            this.teamList.Add(team);
        }

        public void AddMatch(Match match)
        {
            this.matchesList.Add(match);
        }

        public IEnumerable<Match> GetMatches()
        {
            return matchesList;
        }

    }

    public class GroupMetadata
    {

    }

    [MetadataType(typeof(GroupMetadata))]
    public partial class Group
    {
        protected Group()
        { }


        [Obsolete]
        public virtual ICollection<Team> TeamList { get; set; }
        protected ICollection<Team> teamList
        {
#pragma warning disable 612, 618
            get
            {
                return TeamList;
            }
            set
            {
                TeamList = value;
            }
#pragma warning restore 612, 618
        }

        [Obsolete]
        public virtual ICollection<Match> MatchesList { get; set; }
        protected ICollection<Match> matchesList
        {
#pragma warning disable 612, 618
            get
            {
                return MatchesList;
            }
            set
            {
                MatchesList = value;
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

    }

}
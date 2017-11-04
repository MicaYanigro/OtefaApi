using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Otefa.Domain.Model.Entities
{

    public partial class TournamentGroupMatches
    {

        protected TournamentGroupMatches()
        { }

        private string groupName;
        private List<FixtureGroupMatches> groupMatches;
     
        public TournamentGroupMatches(string groupName)
        {

            GroupName = groupName;
            groupMatches = new List<FixtureGroupMatches>();
        }

        public void addMatch(FixtureGroupMatches match)
        {
            this.groupMatches.Add(match);
        }
  
        public string GroupName
        {

            get
            {
                return groupName;
            }

            protected set
            {
                groupName = value;
            }

        }

        public List<FixtureGroupMatches> GroupMatches
        {

            get
            {
                return groupMatches;
            }

            protected set
            {
                groupMatches = value;
            }

        }


    }

    
}
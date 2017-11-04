using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Otefa.Domain.Model.Entities
{

    public class FixtureGroupMatches
    {
        protected FixtureGroupMatches() { }

        private int round;
        private string team1;
        private string team2;
        private DateTime date;
        private int? goals1;
        private int? goals2;
        private int id;

        public FixtureGroupMatches(int id, int round, string team1, string team2, DateTime date, int? goals1, int? goals2)
        {
            this.Id = id; 
            this.round = round;
            this.team1 = team1;
            this.team2 = team2;
            this.date = date;
            this.goals1 = goals1;
            this.goals2 = goals2;

        }

        public int Id
        {

            get
            {
                return id;
            }

            protected set
            {
                id = value;
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

        public int? Goals2
        {

            get
            {
                return goals2;
            }

            protected set
            {
                goals2 = value;
            }

        }

        public int? Goals1
        {

            get
            {
                return goals1;
            }

            protected set
            {
                goals1 = value;
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

        public string Team1
        {

            get
            {
                return team1;
            }

            protected set
            {
                team1 = value;
            }

        }


        public string Team2
        {

            get
            {
                return team2;
            }

            protected set
            {
                team2 = value;
            }

        }



    }


}
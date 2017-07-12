using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Otefa.Domain.Model.Entities
{

    public partial class PlayerDetails : Entity
    {

        private Player player;
        private int? goals;
        private bool played;
        private Card? card;

        public PlayerDetails(Player player, int? goals, bool played, Card? card)
        {
            Player = player;
            Goals = goals;
            Played = played;
            Card = card;
        }

        public virtual Player Player
        {

            get
            {
                return player;
            }

            protected set
            {
                player = value;
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

        public bool Played
        {

            get
            {
                return played;
            }

            protected set
            {
                played = value;
            }

        }

        public Card? Card
        {

            get
            {
                return card;
            }

            protected set
            {
                card = value;
            }

        }

        public void Update()
        {

            throw new NotImplementedException();
        }

    }

    public partial class PlayerDetails
    {
        protected PlayerDetails()
        { }
        
    }

}
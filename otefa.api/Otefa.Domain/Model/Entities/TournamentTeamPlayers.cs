using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Otefa.Domain.Model.Entities
{

    public partial class TournamentTeamPlayers : Entity
    {

        private Team team;

        public TournamentTeamPlayers(Team team)
        {
            Team = team;

            playersList = new Collection<Player>();

        }
        

        public Team Team
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
      

        public void AddPlayer(Player player)
        {
            this.playersList.Add(player);
        }
        
     

    }

    public class TournamentTeamPlayersMetadata
    {
     
    }

    [MetadataType(typeof(TournamentTeamPlayersMetadata))]
    public partial class TournamentTeamPlayers
    {
        protected TournamentTeamPlayers()
        { }


        [Obsolete]
        public virtual ICollection<Player> PlayersList { get; set; }
        protected ICollection<Player> playersList
        {
#pragma warning disable 612, 618
            get
            {
                return PlayersList;
            }
            set
            {
                PlayersList = value;
            }
#pragma warning restore 612, 618
        }

    }

}
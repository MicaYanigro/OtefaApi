using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Factories
{
    public class TeamFactory : ITeamFactory
    {
        [Injectable]
        public IPlayerRepository PlayerRepository { get; set; }

        public Team Create(string name, string teamDelegate, string shieldImage, string teamImage, IEnumerable<int> playersList)
        {
            var team = new Team(name, teamDelegate, shieldImage, teamImage);

            foreach (var playerID in playersList)
            {
                var player = PlayerRepository.GetById(playerID);
                team.AddPlayer(player);
            }
            
            return team;
        }
    }
}
using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;

namespace Otefa.Domain.Model.Factories
{
    public class PlayerDetailsFactory : IPlayerDetailsFactory
    {

        [Injectable]
        IPlayerRepository PlayerRepository { get; set; }

        public PlayerDetails Create(int playerID, int? goals, bool played, Card? card, string observation)
        {
            var player = PlayerRepository.GetById(playerID);

            return new PlayerDetails(player, goals, played,  card, observation);
        }
    }
}
using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;

namespace Otefa.Domain.Model.Factories
{
    public class PlayerDetailsFactory : IPlayerDetailsFactory
    {

        public PlayerDetails Create(MatchTeam matchTeam, int playerID, int? goals, bool played, Card? card, string observation)
        {
            var player = Container.Current.Resolve<IPlayerRepository>().GetById(playerID);
           
            return new PlayerDetails(matchTeam, player, goals, played,  card, observation);
        }
    }
}
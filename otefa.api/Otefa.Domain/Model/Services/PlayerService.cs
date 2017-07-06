using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Factories;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Services
{
    public class PlayerService : ServiceBase, IPlayerService
    {

        [Injectable]
        public IPlayerFactory PlayerFactory { get; set; }

        [Injectable]
        public IPlayerRepository PlayerRepository { get; set; }

        public Player FindPlayerByDni(string dni)
        {
            var player = PlayerRepository.GetByDni(dni);
            if (player == null)
                throw new InvalidPlayerDniException();
            else
                return player;
        }


        public Player Create(string name, string lastName, string dni, DateTime birthDate, string email, string celNumber, string medicalInsurance)
        {
            {

                var player = PlayerFactory.Create(name, lastName, dni, birthDate, email,
                                                celNumber, medicalInsurance);

                PlayerRepository.Add(player);
                PlayerRepository.Context.Commit();

                return player;
            }
        }

        public void Update(int playerID, string name, string lastName, string dni, DateTime birthDate, string email, string celNumber, string medicalInsurance)
        {
            var player = PlayerRepository.GetById(playerID);

            player.Update(name,
                          lastName,
                          dni,
                          birthDate,
                          email,
                          celNumber,
                          medicalInsurance);

            PlayerRepository.Update(player);
            PlayerRepository.Context.Commit();
        }

        public IEnumerable<Player> GetAll()
        {
            return PlayerRepository.All();

        }

    }
}
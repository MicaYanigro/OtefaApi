using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Factories;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            return PlayerRepository.GetByDni(dni);

        }


        public async Task<Player> Create(string name, string lastName, string dni, DateTime birthDate, string email, string celNumber, string medicalInsurance)
        {
            {

                var player = PlayerFactory.Create(name, lastName, dni, birthDate, email,
                                                celNumber, medicalInsurance);

                PlayerRepository.Add(player);
                await PlayerRepository.Context.Commit();

                return player;
            }
        }

        public async Task Update(int playerID, string name, string lastName, string dni, DateTime birthDate, string email, string celNumber, string medicalInsurance)
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
            await PlayerRepository.Context.Commit();
        }

        public IEnumerable<Player> GetAll()
        {
            return PlayerRepository.All();

        }

    }
}
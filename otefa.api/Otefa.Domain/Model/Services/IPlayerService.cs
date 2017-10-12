using Otefa.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otefa.Domain.Model.Services
{
    public interface IPlayerService
    {
        Player FindPlayerByDni(string dni);

        Task<Player> Create(string name, string lastName, string dni, DateTime birthDate, string email, string celNumber, string medicalInsurance);

        Task Update(int playerID, string name, string lastName, string dni, DateTime birthDate, string email, string celNumber, string medicalInsurance);

        IEnumerable<Player> GetAll();
    }
}
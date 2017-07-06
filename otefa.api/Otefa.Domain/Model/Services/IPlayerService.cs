using Otefa.Domain.Model.Entities;
using System;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Services
{
    public interface IPlayerService
    {
        Player FindPlayerByDni(string dni);

        Player Create(string name, string lastName, string dni, DateTime birthDate, string email, string celNumber, string medicalInsurance);

        void Update(int playerID, string name, string lastName, string dni, DateTime birthDate, string email, string celNumber, string medicalInsurance);

        IEnumerable<Player> GetAll();
    }
}
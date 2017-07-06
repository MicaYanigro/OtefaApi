using Otefa.Domain.Model.Entities;
using System;

namespace Otefa.Domain.Model.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
    
        public Player Create(string name, string lastName, string dni, DateTime birthDate, 
                             string email, string celNumber, string medicalInsurance)
        {
            
            return new Player(name, lastName, dni, birthDate, email,
                            celNumber, medicalInsurance);
        }
    }
}
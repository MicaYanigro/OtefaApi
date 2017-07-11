using Otefa.Domain.Model.Entities;
using System;

namespace Otefa.Domain.Model.Factories
{
    public class HeadquarterFactory : IHeadquarterFactory
    {
    
        public Headquarter Create(string name, string adress, string city)
        {
            
            return new Headquarter(name, adress, city);
        }
    }
}
using Otefa.Domain.Model.Entities;
using System;

namespace Otefa.Domain.Model.Factories
{
    public class HeadquarterFactory : IHeadquarterFactory
    {
    
        public Headquarter Create(string name, string address, string city)
        {
            
            return new Headquarter(name, address, city);
        }
    }
}
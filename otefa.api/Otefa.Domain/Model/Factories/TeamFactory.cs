using Otefa.Domain.Model.Entities;
using System;

namespace Otefa.Domain.Model.Factories
{
    public class TeamFactory : ITeamFactory
    {
    
        public Team Create(string name, string teamDelegate, string shieldImage, string teamImage)
        {
            
            return new Team(name, teamDelegate, shieldImage, teamImage);
        }
    }
}
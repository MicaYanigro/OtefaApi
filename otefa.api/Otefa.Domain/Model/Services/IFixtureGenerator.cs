using Otefa.Domain.Model.Entities;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Services
{
    public interface IFixtureGenerator
    {
        int[,] GenerateRoundRobin(int num_teams);

        IEnumerable<Match> CreateMatches(Tournament tournament);

        IEnumerable<Match> CreateMatchesByGroup(Tournament tournament, Group group);  
    }
}
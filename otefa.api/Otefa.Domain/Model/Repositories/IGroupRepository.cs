using Otefa.Domain.Model.Entities;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {
        List<Team> GetTeams(Group group);
    }
}
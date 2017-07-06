using Otefa.Domain.Model.Entities;

namespace Otefa.Domain.Model.Repositories
{
    public interface ITeamRepository : IRepository<Team>
    {
        Team GetByName(string name);

    }
}
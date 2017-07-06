
using Otefa.Domain.Model.Entities;

namespace Otefa.Domain.Model.Factories
{
    public interface ITeamFactory
    {

        Team Create(string name, string teamDelegate, string shieldImage, string teamImage);

    }
}
using Otefa.Domain.Model.Entities;

namespace Otefa.Domain.Model.Factories
{
    public interface IPlayerDetailsFactory
    {
        PlayerDetails Create(int playerID, int? goals, bool played, Card? card, string observation);
    }
}
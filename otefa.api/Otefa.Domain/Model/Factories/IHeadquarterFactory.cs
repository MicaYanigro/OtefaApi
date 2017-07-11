
using Otefa.Domain.Model.Entities;

namespace Otefa.Domain.Model.Factories
{
    public interface IHeadquarterFactory
    {

        Headquarter Create(string name, string adress, string city);

    }
}
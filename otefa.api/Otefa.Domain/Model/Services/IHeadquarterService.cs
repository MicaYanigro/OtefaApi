using Otefa.Domain.Model.Entities;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Services
{
    public interface IHeadquarterService
    {
        Headquarter FindHeadquarterByName(string name);

        Headquarter Create(string name, string adress, string city);

        void Update(int headquarterID, string name, string adress, string city);

        IEnumerable<Headquarter> GetAll();
    }
}
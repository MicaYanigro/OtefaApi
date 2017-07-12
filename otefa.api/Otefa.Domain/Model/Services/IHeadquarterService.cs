using Otefa.Domain.Model.Entities;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Services
{
    public interface IHeadquarterService
    {
        Headquarter FindHeadquarterByName(string name);

        Headquarter Create(string name, string address, string city);

        void Update(int headquarterID, string name, string address, string city);

        IEnumerable<Headquarter> GetAll();
    }
}
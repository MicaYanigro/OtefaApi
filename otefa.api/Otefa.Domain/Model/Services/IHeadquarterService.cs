using Otefa.Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otefa.Domain.Model.Services
{
    public interface IHeadquarterService
    {
        Headquarter FindHeadquarterByName(string name);

        Task<Headquarter> Create(string name, string address, string city);

        Task Update(int headquarterID, string name, string address, string city);

        IEnumerable<Headquarter> GetAll();
    }
}
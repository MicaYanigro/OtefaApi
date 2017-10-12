using Otefa.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otefa.Domain.Model.Services
{
    public interface INewService
    {
        Task<New> Create(DateTime date, string title, string body, string image);
        Task Update(int NewID, DateTime date, string title, string body, string image);
        IEnumerable<New> GetAll();
        Task Activate(int newID);
        Task Delete(int newID);
    }
}
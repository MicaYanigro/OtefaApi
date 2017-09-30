using Otefa.Domain.Model.Entities;
using System;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Services
{
    public interface INewService
    {
        New Create(DateTime date, string title, string body, string image);
        void Update(int NewID, DateTime date, string title, string body, string image);
        IEnumerable<New> GetAll();
    }
}
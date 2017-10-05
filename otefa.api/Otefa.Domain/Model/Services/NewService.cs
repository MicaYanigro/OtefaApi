using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Factories;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Services
{
    public class NewService : ServiceBase, INewService
    {


        [Injectable]
        public INewRepository NewRepository { get; set; }


        public New Create(DateTime date, string title, string body, string image)
        {
            {

                var New = new New(date, title, body, image);

                NewRepository.Add(New);
                NewRepository.Context.Commit();

                return New;
            }
        }

        public void Update(int NewID, DateTime date, string title, string body, string image)
        {
            var New = NewRepository.GetById(NewID);

            New.Update(date,
                        title,
                        body,
                        image);

            NewRepository.Update(New);
            NewRepository.Context.Commit();
        }

        public IEnumerable<New> GetAll()
        {
            return NewRepository.All();

        }

        public void Delete(int newID)
        {
            var New = NewRepository.GetById(newID);
            New.Delete();

            NewRepository.Update(New);
            NewRepository.Context.Commit();
        }

        public void Activate(int newID)
        {
            var New = NewRepository.GetById(newID);
            New.Active();

            NewRepository.Update(New);
            NewRepository.Context.Commit();
        }

    }
}
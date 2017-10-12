using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Factories;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otefa.Domain.Model.Services
{
    public class NewService : ServiceBase, INewService
    {


        [Injectable]
        public INewRepository NewRepository { get; set; }


        public async Task<New> Create(DateTime date, string title, string body, string image)
        {
            {

                var New = new New(date, title, body, image);

                NewRepository.Add(New);
                await NewRepository.Context.Commit();

                return New;
            }
        }

        public async Task Update(int NewID, DateTime date, string title, string body, string image)
        {
            var New = NewRepository.GetById(NewID);

            New.Update(date,
                        title,
                        body,
                        image);

            NewRepository.Update(New);
            await NewRepository.Context.Commit();
        }

        public IEnumerable<New> GetAll()
        {
            return NewRepository.All();

        }

        public async Task Delete(int newID)
        {
            var New = await NewRepository.GetByIDAsync(newID);
            New.Delete();

            NewRepository.Update(New);
            await NewRepository.Context.Commit();
        }

        public async Task Activate(int newID)
        {
            var New = await NewRepository.GetByIDAsync(newID);
            New.Active();

            NewRepository.Update(New);
            await NewRepository.Context.Commit();
        }

    }
}
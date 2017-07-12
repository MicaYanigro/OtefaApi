using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Factories;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Services
{
    public class HeadquarterService : ServiceBase, IHeadquarterService
    {

        [Injectable]
        public IHeadquarterFactory HeadquarterFactory { get; set; }

        [Injectable]
        public IHeadquarterRepository HeadquarterRepository { get; set; }

        public Headquarter FindHeadquarterByName(string name)
        {
            return HeadquarterRepository.GetByName(name);

        }

        public Headquarter Create(string name, string address, string city)
        {
            {

                var Headquarter = HeadquarterFactory.Create(name, address, city);

                HeadquarterRepository.Add(Headquarter);
                HeadquarterRepository.Context.Commit();

                return Headquarter;
            }
        }

        public void Update(int headquarterID, string name, string address, string city)
        {
            var Headquarter = HeadquarterRepository.GetById(headquarterID);

            Headquarter.Update(name,
                        address,
                        city);

            HeadquarterRepository.Update(Headquarter);
            HeadquarterRepository.Context.Commit();
        }

        public IEnumerable<Headquarter> GetAll()
        {
            return HeadquarterRepository.All();

        }

    }
}
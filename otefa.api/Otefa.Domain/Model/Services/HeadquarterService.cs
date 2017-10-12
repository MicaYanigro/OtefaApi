using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Factories;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<Headquarter> Create(string name, string address, string city)
        {
            {

                var Headquarter = HeadquarterFactory.Create(name, address, city);

                HeadquarterRepository.Add(Headquarter);
                await HeadquarterRepository.Context.Commit();

                return Headquarter;
            }
        }

        public async Task Update(int headquarterID, string name, string address, string city)
        {
            var Headquarter = await HeadquarterRepository.GetByIDAsync(headquarterID);

            Headquarter.Update(name,
                        address,
                        city);

            HeadquarterRepository.Update(Headquarter);
            await HeadquarterRepository.Context.Commit();
        }

        public IEnumerable<Headquarter> GetAll()
        {
            return HeadquarterRepository.All();

        }

    }
}
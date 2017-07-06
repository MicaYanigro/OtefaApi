using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Repositories;
using System.Linq;
using Sai.Infrastructure.IoC;
using Sai.Domain.Model.Entities.Commission;

namespace Sai.Infrastructure.Persistence
{
    public class CommissionRepositoryInMemory : InMemoryRepositoryBase<Commission>, ICommissionRepository
    {

        public Commission GetCommissionByID(int commissionID)
        {
            return Container.Current.Resolve<IActivityRepository>().GetCommissionByID(commissionID);
        }

        public Enrollment GetEnrollmentByID(int enrollmentID)
        {
            return Container.Current.Resolve<IActivityRepository>().GetEnrollmentByID(enrollmentID);
        }

        public int GetLatestCommissionInapNumber()
        {
            return innerList.OrderByDescending(x => x.CreatedDate).Select(x => x.InapNumber).FirstOrDefault();
        }

    }
}
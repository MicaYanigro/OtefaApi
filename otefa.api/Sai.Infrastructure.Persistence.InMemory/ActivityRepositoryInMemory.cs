using Sai.Domain.Model.Entities.Activity;
using Sai.Domain.Model.Entities.Commission;
using Sai.Domain.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sai.Infrastructure.Persistence
{
    public class ActivityRepositoryInMemory : InMemoryRepositoryBase<Activity>, IActivityRepository
    {

        public int GetLastActivityInapNumber()
        {
            return innerList.OrderByDescending(x => x.CreatedDate).Select(r => r.InapNumber).FirstOrDefault();
        }

        public Commission GetCommissionByID(int commissionID)
        {
            return innerList.Select(x => x.GetCommissions().Where(r => r.GetId() == commissionID).SingleOrDefault()).SingleOrDefault();
        }

        public Enrollment GetEnrollmentByID(int enrollmentID)
        {
            return innerList.Select(x => x.GetCommissions().Select(r => r.GetAllEnrollments().Where(a => a.GetId() == enrollmentID))).SingleOrDefault().SingleOrDefault().SingleOrDefault();
        }

        public IEnumerable<Tuple<int, IEnumerable<int>>> GetActivityIdsWithEquivalences()
        {
            return innerList.Select(a => new Tuple<int, IEnumerable<int>>(a.GetId(), GetEquivalenceIds(a)));
        }

        private IEnumerable<int> GetEquivalenceIds(Activity activity)
        {
            return activity.GetEquivalences().Select(e => e.GetId()).AsEnumerable();
        }
        public IEnumerable<Activity> GetApprovedByUserID(int userID)
        {
            return innerList.Where(x => x.GetCommissions().Any(c => c.GetAllEnrollments().Any(e => e.GetUserID() == userID && e.GetEvaluation() == Evaluation.Approved)));
        }

        public Activity GetByInapCode(string inapCode)
        {
            return innerList.Where(x => x.GetInapCode() == inapCode).SingleOrDefault();
        }
        public IEnumerable<Activity> GetEnrolledByUserID(int userID)
        {
            return innerList.Where(x => x.GetCommissions().Any(c => c.GetAllEnrollments().Any(e => e.GetUserID() == userID && e.GetEvaluation() != Evaluation.Approved && IsNotRejectedEnrollment(e))));
        }

        public bool IsNotRejectedEnrollment(Enrollment e)
        {
            if (e.GetType() == typeof(AgentEnrollment))
            {
                AgentEnrollment agent = (AgentEnrollment)e;
                if (agent.Status != AgentEnrollmentStatus.Rejected)
                    return true;
                else
                    return false;
            }
            else
            {
                PersonEnrollment person = (PersonEnrollment)e;
                if (person.Status != PersonEnrollmentStatus.Rejected)
                    return true;
                else
                    return false;
            }
        }
    }
}
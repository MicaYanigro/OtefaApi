using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Activity;
using Sai.Domain.Model.Entities.Commission;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;
using Sai.Infrastructure.Persistence;
using System;
using System.Security.Principal;

namespace Sai.Infrastructure.IntegrationTesting
{
    [TestFixture]
    public class ActivityRepositoryTests
    {

        [Test]
        public void Should_Add_An_Activity()
        {

            var principalMock = new Mock<IPrincipal>();
            principalMock.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);
            var threadWrapperMock = new Mock<IThreadWrapper>();
            threadWrapperMock.Setup(x => x.GetCurrentPrincipal()).Returns(principalMock.Object);

            var userMock = new Mock<User>();

            Container.Current = new NinjectContainer();

            Container.Current.Register<IActivityService, ActivityService>();
            Container.Current.Register<IAreaService, AreaService>();
            Container.Current.Register<ICategoryService, CategoryService>();
            Container.Current.Register<IModalityTypeService, ModalityTypeService>();
            Container.Current.Register<IProgramService, ProgramService>();
            Container.Current.Register(threadWrapperMock.Object);
            Container.Current.Register<IUserService, UserService>();
            Container.Current.Register<IEnrollmentService, EnrollmentService>();
            Container.Current.Register<ICommissionService, CommissionService>();
            Container.Current.Register<IEvaluationCriteriaService, EvaluationCriteriaService>();
            Container.Current.Register<IEvaluationCriteriaConsiderationService, EvaluationCriteriaConsiderationService>();

            Container.Current.Register<IActivityFactory, ActivityFactory>();
            Container.Current.Register<IAreaFactory, AreaFactory>();
            Container.Current.Register<ICategoryFactory, CategoryFactory>();
            Container.Current.Register<ICommissionFactory, CommissionFactory>();
            Container.Current.Register<IPersonFactory, PersonFactory>();
            Container.Current.Register<IAgentFactory, AgentFactory>();
            Container.Current.Register<IEvaluationCriteriaFactory, EvaluationCriteriaFactory>();
            Container.Current.Register<IEvaluationCriteriaConsiderationFactory, EvaluationCriteriaConsiderationFactory>();
            Container.Current.Register<ICommissionScheduleFactory, CommissionScheduleFactory>();

            IRepositoryContext repositoryContext = new RepositoryContextEF();
            Container.Current.Register(repositoryContext);

            Container.Current.Register<ICategoryRepository>(new CategoryRepositoryEF(repositoryContext));
            Container.Current.Register<IRepository<Program>>(new ProgramRepositoryEF(repositoryContext));
            Container.Current.Register<IRepository<ModalityType>>(new ModalityTypeRepositoryEF(repositoryContext));
            Container.Current.Register<IActivityRepository>(new ActivityRepositoryEF(repositoryContext));
            Container.Current.Register<IAreaRepository>(new AreaRepositoryEF(repositoryContext));
            Container.Current.Register<ICommissionRepository>(new CommissionRepositoryEF(repositoryContext));
            Container.Current.Register<IEvaluationCriteriaRepository>(new EvaluationCriteriaRepositoryEF(repositoryContext));
            Container.Current.Register<IEvaluationCriteriaConsiderationRepository>(new EvaluationCriteriaConsiderationRepositoryEF(repositoryContext));

            var area = Container.Current.Resolve<IAreaService>().GetByBriefDescription("ZZ");

            if (area == null)
            {
                area = Container.Current.Resolve<IAreaService>().Create("TEST AREA", "ZZ");
            }

            var category = Container.Current.Resolve<ICategoryService>().GetByBriefDescription("ZZ");

            if (category == null)
            {
                category = Container.Current.Resolve<ICategoryService>().Create("TEST CATEGORY", "ZZ");
            }

            var random = new Random();

            var address = new string('x', 500);
            var attendanceAndApprovalRequirements = new string('x', 500);
            var bibliography = new string('x', 500);
            var caseFileNumber = "EX-2016-02887279-APN-SECMA#MM";
            var city = new string('x', 40);
            var province = new string('x', 40);
            var country = new string('x', 40);
            var content = new string('x', 500);
            var demandOrigin = new string('x', 500);
            var durationHours = random.Next(1, 10);
            var evaluationTools = new string('x', 500);
            var expectedContribution = new string('x', 500);
            var hasAGrant = true;
            var isUniqueInscriptionAllowed = true;
            var justification = new string('x', 500);
            var learningEvaluation = new string('x', 500);
            var methodologicalStrategiesAndDidacticResources = new string('x', 500);
            var modalityTypeDescription = new string('x', 30);
            var modalityType = new ModalityType(modalityTypeDescription);
            var name = new string('x', 500);
            var objectives = new string('x', 500);
            var observation = new string('x', 500);
            var participantProfile = new string('x', 500);
            var place = new string('x', 500);
            var programDescription = new string('x', 30);
            var program = new Program(programDescription);
            var teacherProfile = new string('x', 500);
            var userID = 1;

            var enrollmentCloseHours = 1;

            var activity = new Activity(area,
                                        category,
                                        caseFileNumber,
                                        durationHours,
                                        isUniqueInscriptionAllowed,
                                        modalityType,
                                        name,
                                        observation,
                                        participantProfile,
                                        program,
                                        userMock.Object);

            activity.AddCommission(10, 3, CommissionInscriptionType.Open, enrollmentCloseHours);

            var activityRepository = Container.Current.Resolve<IActivityRepository>();
            activityRepository.Add(activity);

            var number = repositoryContext.Commit();

            int activityId = activity.GetId();

            repositoryContext = new RepositoryContextEF();
            activityRepository = new ActivityRepositoryEF(repositoryContext);
            activity = activityRepository.GetById(activityId);

            activity.AddCommission(20, 5, CommissionInscriptionType.Open, enrollmentCloseHours);

            activityRepository.Update(activity);

            number = repositoryContext.Commit();

        }

    }
}
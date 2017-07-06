using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Activity;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;
using System.Collections.Generic;

namespace Sai.Domain.UnitTesting.Model.Services
{
    [TestFixture]
    public class TeacherServiceTests
    {

        #region Create

        [Test]
        public void Should_Create_Teacher()
        {
            // Arrange

            var cuil = "33-69345023-9";
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = "test@test.com";
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var institutionID = 1;
            var gdeNumber = "132";
            var rlmNumber = "123";
            var institution = new Mock<InstitutionalProvider>();
            var repositoryContextMock = new Mock<IRepositoryContext>();

            var TeacherRepositoryMock = new Mock<ITeachersRepository>();
            TeacherRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var InstitutionalProviderRepositoryMock = new Mock<IInstitutionalProviderRepository>();
            InstitutionalProviderRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(institution.Object);
            InstitutionalProviderRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var TeacherService = new TeacherService();

            TeacherService.teacherRepository = TeacherRepositoryMock.Object;
            TeacherService.institutionalProviderRepository = InstitutionalProviderRepositoryMock.Object;

            var TeacherMock = new Mock<Teacher>();

            var TeacherFactoryMock = new Mock<ITeacherFactory>();
            TeacherFactoryMock.Setup(x => x.Create(cuil, name, lastName, email, teacherInscriptionType, isInstitutional, institution.Object, rlmNumber, gdeNumber)).Returns(TeacherMock.Object);

            TeacherService.teacherFactory = TeacherFactoryMock.Object;

            // Act

            TeacherService.Create(cuil, name, lastName, email, teacherInscriptionType, isInstitutional, institutionID, rlmNumber, gdeNumber);

            // Assert

            TeacherFactoryMock.Verify(x => x.Create(cuil, name, lastName, email, teacherInscriptionType, isInstitutional, institution.Object, rlmNumber, gdeNumber), Times.Once);
            TeacherRepositoryMock.Verify(x => x.Add(TeacherMock.Object), Times.Once);
            repositoryContextMock.Verify(x => x.Commit(), Times.Once);
        }

        #endregion Create

        #region GetAll

        [Test]
        public void Should_Get_All_Teachers()
        {
            // Arrange

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var TeacherRepositoryMock = new Mock<ITeachersRepository>();
            TeacherRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            TeacherService TeacherService = new TeacherService();
            TeacherService.teacherRepository = TeacherRepositoryMock.Object;

            // Act

            var Teachers = TeacherService.GetAll();

            // Assert

            Assert.That(Teachers, Is.InstanceOf<IEnumerable<Teacher>>());
            Assert.That(Teachers, Is.Not.Null);

            TeacherRepositoryMock.Verify(x => x.All(), Times.Once);
        }

        #endregion GetAll

    }
}
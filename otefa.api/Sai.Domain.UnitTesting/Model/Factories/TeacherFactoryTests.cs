using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Activity;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;

namespace Sai.Domain.UnitTesting.Model.Factories
{
    [TestFixture]
    public class TeacherFactoryTests
    {
        [Test]
        public void Should_Create()
        {
            // Arrange

            var cuil = "33-69345023-9";
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = "test@test.com";
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var gdeNumber = "AA-2017-1-X-A";
            var rlmNumber = "AA-2017-1-X-A";

            var institution = new Mock<InstitutionalProvider>();
            var TeacherFactory = new TeacherFactory();



            var TeacherMock = new Mock<Teacher>();
            var TeacherServiceMock = new Mock<ITeacherService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<ITeacherService>()).Returns(TeacherServiceMock.Object);
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;


            // Act

            var Teacher = TeacherFactory.Create(cuil, name, lastName, email, teacherInscriptionType, isInstitutional, institution.Object, rlmNumber, gdeNumber);

            // Assert

            Assert.That(Teacher, Is.InstanceOf<Teacher>());
            Assert.That(Teacher.Cuil, Is.EqualTo(cuil));
            Assert.That(Teacher.LastName, Is.EqualTo(lastName));
            Assert.That(Teacher.Name, Is.EqualTo(name));
            Assert.That(Teacher.Email, Is.EqualTo(email));
            Assert.That(Teacher.IsInstitutional, Is.EqualTo(isInstitutional));
            Assert.That(Teacher.TeacherInscriptionType, Is.EqualTo((TeacherInscriptionType)teacherInscriptionType));


        }
    }
}
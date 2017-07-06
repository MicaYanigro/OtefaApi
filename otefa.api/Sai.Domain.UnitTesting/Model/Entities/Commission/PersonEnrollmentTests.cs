using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Commission;
using Sai.Domain.Model.Exceptions.Enrollment;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;

namespace Sai.Domain.UnitTesting.Model.Entities
{

    [TestFixture]
    public class personEnrollmentTests
    {

        [Test]
        public void Should_Create_A_PersonEnrollment()
        {
            // Arrange
            var personMock = new Mock<Person>();

            // Act
            var personEnrollment = new PersonEnrollment(personMock.Object);

            // Assert
            Assert.That(personEnrollment.Person, Is.EqualTo(personMock.Object));
            Assert.That(personEnrollment.GetStatus(), Is.EqualTo(EnrollmentStatus.Registered));

        }

        [Test]
        public void Should_Create_A_PersonEnrollment_But_Fails_Because_person_Is_Null()
        {
            // Arrange
            var personMock = new Mock<Person>();

            // Act
            var personEnrollment = new PersonEnrollment(personMock.Object);

            // Assert
            Assert.That(personEnrollment.Person, Is.EqualTo(personMock.Object));
            Assert.That(personEnrollment.GetStatus(), Is.EqualTo(EnrollmentStatus.Registered));

        }

        [Test]
        public void Should_Approve_A_PersonEnrollment()
        {
            // Arrange
            var personMock = new Mock<Person>();

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;
            // Act

            var personEnrollment = new PersonEnrollment(personMock.Object);
            personEnrollment.Approve();

            // Assert

            Assert.That(personEnrollment.Person, Is.EqualTo(personMock.Object));
            Assert.That(personEnrollment.GetStatus(), Is.EqualTo(EnrollmentStatus.Approved));

        }


        [Test]
        public void Should_Reject_A_PersonEnrollment()
        {
            // Arrange
            var personMock = new Mock<Person>();

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;
            // Act

            var personEnrollment = new PersonEnrollment(personMock.Object);
            personEnrollment.Reject(null, null);

            // Assert

            Assert.That(personEnrollment.Person, Is.EqualTo(personMock.Object));
            Assert.That(personEnrollment.GetStatus(), Is.EqualTo(EnrollmentStatus.Rejected));

        }

        [Test]
        public void Should_Evaluate()
        {

            // Arrange
            var personMock = new Mock<Person>();

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;
            var evaluation = Evaluation.Approved;
            // Act

            var personEnrollment = new PersonEnrollment(personMock.Object);

            personEnrollment.Evaluate(evaluation);


            // Assert

            Assert.That(personEnrollment.GetEvaluation() == Evaluation.Approved);

        }
    }
}
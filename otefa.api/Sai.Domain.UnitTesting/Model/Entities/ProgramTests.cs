using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions.Program;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;

namespace Sai.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class ProgramTests
    {

        [Test]
        public void Should_Create_A_Program()
        {
            // Arrange

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var description = new string('x', 40);

            // Act

            var program = new Program(description);

            // Assert

            Assert.That(program.Description, Is.EqualTo(description));
        }

        [Test]
        public void Should_Create_A_Program_But_Fails_Because_Description_Length_Is_More_Than_50_Characters()
        {
            // Arrange

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var description = new string('x', 51);

            // Act

            var caughtException = Assert.Catch(() => new Program(description));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<InvalidProgramDescriptionLengthException>());
        }

        [Test]
        public void Should_Create_A_Program_But_Fails_Because_Description_Is_Null()
        {
            // Arrange

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            string description = null;

            // Act

            var caughtException = Assert.Catch(() => new Program(description));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<NullProgramDescriptionException>());
        }

        [Test]
        public void Should_Create_A_Program_But_Fails_Because_Description_Is_Empty()
        {

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;
            // Arrange

            var description = "";

            // Act

            var caughtException = Assert.Catch(() => new Program(description));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<EmptyProgramDescriptionException>());
        }



    }
}
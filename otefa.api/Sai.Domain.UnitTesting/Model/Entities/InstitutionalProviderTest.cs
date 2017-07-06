using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions.InstitutionalProvider;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;

namespace Sai.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class InstitutionalProviderTest
    {

        [Test]
        public void Should_Create_An_InstitutionalProvider()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();

            var cuil = "33-69345023-9";
            var rlmNumber = new string('x', 50);
            var name = new string('x', 50);

            var institutionalProviderServiceMock = new Mock<IInstitutionalProviderService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IInstitutionalProviderService>()).Returns(institutionalProviderServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            // Act

            var institutionalProvider = new InstitutionalProvider(cuil, rlmNumber, name);

            // Assert

            Assert.That(institutionalProvider.Cuil, Is.EqualTo(cuil));
            Assert.That(institutionalProvider.RlmNumber, Is.EqualTo(rlmNumber));
            Assert.That(institutionalProvider.Name, Is.EqualTo(name));

        }

        [Test]
        public void Should_Create_An_InstitutionalProvider_But_Fails_Because_Cuil_Is_Null()
        {
            var userServiceMock = new Mock<IUserService>();

            // Arrange
            var cuil = (string)null;
            var rlmNumber = new string('x', 50);
            var name = new string('x', 50);

            var institutionalProviderServiceMock = new Mock<IInstitutionalProviderService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IInstitutionalProviderService>()).Returns(institutionalProviderServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;


            // Act
            var caughtException = Assert.Catch(() => new InstitutionalProvider(cuil, rlmNumber, name));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<NullInstitutionalProviderCuilException>());
        }

        [Test]
        public void Should_Create_An_InstitutionalProvider_But_Fails_Because_Cuil_Is_Empty()
        {
            var userServiceMock = new Mock<IUserService>();

            // Arrange
            var cuil = "";
            var rlmNumber = new string('x', 50);
            var name = new string('x', 50);

            var InstitutionalProviderMock = new Mock<InstitutionalProvider>();
            var InstitutionalProviderServiceMock = new Mock<IInstitutionalProviderService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IInstitutionalProviderService>()).Returns(InstitutionalProviderServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new InstitutionalProvider(cuil, rlmNumber, name));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<EmptyInstitutionalProviderCuilException>());
        }

        [Test]
        public void Should_Create_An_InstitutionalProvider_But_Fails_Because_Cuil_Format_Is_Invalid()
        {
            var userServiceMock = new Mock<IUserService>();

            // Arrange
            var cuil = new string('x', 50);
            var rlmNumber = new string('x', 50);
            var name = new string('x', 50);

            var InstitutionalProviderMock = new Mock<InstitutionalProvider>();
            var InstitutionalProviderServiceMock = new Mock<IInstitutionalProviderService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IInstitutionalProviderService>()).Returns(InstitutionalProviderServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new InstitutionalProvider(cuil, rlmNumber, name));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<InvalidInstitutionalProviderCuilFormatException>());
        }

        [Test]
        public void Should_Create_A_InstitutionalProvider_But_Fails_Because_RlmNumber_Is_Null()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();


            var cuil = "33-69345023-9";
            var rlmNumber = (string)null;
            var name = new string('x', 50);

            var InstitutionalProviderMock = new Mock<InstitutionalProvider>();
            var InstitutionalProviderServiceMock = new Mock<IInstitutionalProviderService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IInstitutionalProviderService>()).Returns(InstitutionalProviderServiceMock.Object);
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new InstitutionalProvider(cuil, rlmNumber, name));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<NullInstitutionalProviderRlmNumberException>());
        }


        [Test]
        public void Should_Create_A_InstitutionalProvider_But_Fails_Because_LastName_Is_Empty()
        {
            // Arrange

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var cuil = "33-69345023-9";
            var rlmNumber = "";
            var name = new string('x', 50);

            var InstitutionalProviderMock = new Mock<InstitutionalProvider>();
            var InstitutionalProviderServiceMock = new Mock<IInstitutionalProviderService>();
            containerMock.Setup(x => x.Resolve<IInstitutionalProviderService>()).Returns(InstitutionalProviderServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new InstitutionalProvider(cuil, rlmNumber, name));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<EmptyInstitutionalProviderRlmNumberException>());
        }


        [Test]
        public void Should_Create_A_InstitutionalProvider_But_Fails_Because_Name_Is_Null()
        {
            // Arrange


            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var cuil = "33-69345023-9";
            var rlmNumber = "AA-2017-1-X-A";
            var name = (string)null;

            var InstitutionalProviderMock = new Mock<InstitutionalProvider>();
            var InstitutionalProviderServiceMock = new Mock<IInstitutionalProviderService>();
            containerMock.Setup(x => x.Resolve<IInstitutionalProviderService>()).Returns(InstitutionalProviderServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new InstitutionalProvider(cuil, rlmNumber, name));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<NullInstitutionalProviderNameException>());
        }


        [Test]
        public void Should_Create_A_InstitutionalProvider_But_Fails_Because_Name_Is_Empty()
        {
            // Arrange


            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var cuil = "33-69345023-9";
            var rlmNumber = "AA-2017-1-X-A";
            var name = "";

            var InstitutionalProviderMock = new Mock<InstitutionalProvider>();
            var InstitutionalProviderServiceMock = new Mock<IInstitutionalProviderService>();
            containerMock.Setup(x => x.Resolve<IInstitutionalProviderService>()).Returns(InstitutionalProviderServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new InstitutionalProvider(cuil, rlmNumber, name));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<EmptyInstitutionalProviderNameException>());
        }


        [Test]
        public void Should_Create_InstitutionalProvider_But_Fails_Because_Cuil_Already_Exists()
        {
            // Arrange


            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var cuil = "33-69345023-9";
            var rlmNumber = new string('x', 50);
            var name = new string('x', 50);

            var InstitutionalProviderMock = new Mock<InstitutionalProvider>();
            var InstitutionalProviderServiceMock = new Mock<IInstitutionalProviderService>();
            InstitutionalProviderServiceMock.Setup(x => x.IsExistentCuil(cuil)).Returns(true);

             containerMock.Setup(x => x.Resolve<IInstitutionalProviderService>()).Returns(InstitutionalProviderServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act

            var caughtException = Assert.Catch(() => new InstitutionalProvider(cuil, rlmNumber, name));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<ExistantInstitutionalProviderCuilException>());
        }

    }
}
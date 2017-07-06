using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;

namespace Sai.Domain.UnitTesting.Model.Factories
{
    [TestFixture]
    public class InstitutionalProviderFactoryTests
    {
        [Test]
        public void Should_Create()
        {
            // Arrange

           

            var cuil = "33-69345023-9";
            var rlmNumber = new string('x', 50);
            var name = new string('x', 50);

            var InstitutionalProviderFactory = new InstitutionalProviderFactory();

            var InstitutionalProviderMock = new Mock<InstitutionalProvider>();
            var InstitutionalProviderServiceMock = new Mock<IInstitutionalProviderService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<IInstitutionalProviderService>()).Returns(InstitutionalProviderServiceMock.Object);
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act

            var InstitutionalProvider = InstitutionalProviderFactory.Create(cuil, rlmNumber, name);

            // Assert

            Assert.That(InstitutionalProvider, Is.InstanceOf<InstitutionalProvider>());
            Assert.That(InstitutionalProvider.Cuil, Is.EqualTo(cuil));
            Assert.That(InstitutionalProvider.RlmNumber, Is.EqualTo(rlmNumber));
            Assert.That(InstitutionalProvider.Name, Is.EqualTo(name));

        }
    }
}
using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;

namespace Sai.Domain.UnitTesting.Model.Services
{
    [TestFixture]
    public class InstitutionalProviderServiceTests
    {

        #region Get

        [Test]
        public void Should_Get_By_CuilCuit()
        {
            // Arrange
            string cuilcuit = string.Empty;
            var repositoryContextMock = new Mock<IRepositoryContext>();
            var InstitutionalProviderMock = new Mock<InstitutionalProvider>();
            var InstitutionalProviderRepositoryMock = new Mock<IInstitutionalProviderRepository>();
            InstitutionalProviderRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            InstitutionalProviderRepositoryMock.Setup(x => x.GetByCuilCuit(It.IsAny<string>())).Returns(InstitutionalProviderMock.Object);
            InstitutionalProviderService InstitutionalProviderService = new InstitutionalProviderService();
            InstitutionalProviderService.InstitutionalProviderRepository = InstitutionalProviderRepositoryMock.Object;

            // Act

            var InstitutionalProvider = InstitutionalProviderService.FindInstitutionalProviderByCuitCuil(cuilcuit);

            // Assert

            Assert.That(InstitutionalProvider, Is.Not.Null);
            Assert.That(InstitutionalProvider, Is.EqualTo(InstitutionalProviderMock.Object));

            InstitutionalProviderRepositoryMock.Verify(x => x.GetByCuilCuit(cuilcuit), Times.Once);
        }

        #endregion Get


        #region Create

        [Test]
        public void Should_Create()
        {
            // Arrange

            var cuil = "33-69345023-9";
            var rlmNumber = new string('x', 50);
            var name = new string('x', 50);

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var InstitutionalProviderMock = new Mock<InstitutionalProvider>();
            var InstitutionalProviderRepositoryMock = new Mock<IInstitutionalProviderRepository>();
            InstitutionalProviderRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var institutionalProviderService = new InstitutionalProviderService();

            institutionalProviderService.InstitutionalProviderRepository = InstitutionalProviderRepositoryMock.Object;

            var institutionalProviderFactoryMock = new Mock<IInstitutionalProviderFactory>();
            institutionalProviderFactoryMock.Setup(x => x.Create(cuil, rlmNumber, name)).Returns(InstitutionalProviderMock.Object);

            institutionalProviderService.InstitutionalProviderFactory = institutionalProviderFactoryMock.Object;

            // Act

            var createdInstitutionalProvider = institutionalProviderService.Create(cuil, rlmNumber, name);

            // Assert

            institutionalProviderFactoryMock.Verify(x => x.Create(cuil, rlmNumber, name), Times.Once);
            InstitutionalProviderRepositoryMock.Verify(x => x.Add(InstitutionalProviderMock.Object), Times.Once);
            repositoryContextMock.Verify(x => x.Commit(), Times.Once);
        }
        #endregion
    }
}
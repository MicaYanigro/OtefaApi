using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Factories;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;

namespace Sai.Domain.UnitTesting.Model.Services
{
    [TestFixture]
    public class OrganismServiceTests
    {

        #region Create

        [Test]
        public void Should_Create_An_Organism()
        {
            // Arrange

            var description = new string('x', 30);
            var briefDescription = new string('x', 4);
            int sector = 1;
            int level = 1;
            int ctc = 1;
            int province = 1;
            int municipality = 1;
            bool commissionWithoutPAC = true;
            bool attendanceConstancyWithoutPAC = false;

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var OrganismRepositoryMock = new Mock<IOrganismRepository>();
            OrganismRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var OrganismService = new OrganismService();

            OrganismService.OrganismRepository = OrganismRepositoryMock.Object;

            var OrganismMock = new Mock<Organism>();

            var OrganismFactoryMock = new Mock<IOrganismFactory>();
            OrganismFactoryMock.Setup(x => x.Create(description, briefDescription, sector, level, ctc, province, municipality, commissionWithoutPAC, attendanceConstancyWithoutPAC)).Returns(OrganismMock.Object);

            OrganismService.OrganismFactory = OrganismFactoryMock.Object;

            var userServiceMock = new Mock<IUserService>();
            OrganismService.UserService = userServiceMock.Object;

            // Act

            var createdOrganism = OrganismService.Create(description, briefDescription, sector, level, ctc, province, municipality, commissionWithoutPAC, attendanceConstancyWithoutPAC);

            // Assert

            OrganismFactoryMock.Verify(x => x.Create(description, briefDescription, sector, level, ctc, province, municipality, commissionWithoutPAC, attendanceConstancyWithoutPAC), Times.Once);
            OrganismRepositoryMock.Verify(x => x.Add(OrganismMock.Object), Times.Once);
            repositoryContextMock.Verify(x => x.Commit(), Times.Once);
        }


        #endregion Create

        #region GetAll

        [Test]
        public void Should_Get_All_Organisms()
        {
            // Arrange

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var OrganismRepositoryMock = new Mock<IOrganismRepository>();
            OrganismRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            OrganismService OrganismService = new OrganismService();
            OrganismService.OrganismRepository = OrganismRepositoryMock.Object;

            // Act

            var Organisms = OrganismService.GetAll();

            // Assert

            Assert.That(Organisms, Is.InstanceOf<List<Organism>>());
            Assert.That(Organisms, Is.Not.Null);

            OrganismRepositoryMock.Verify(x => x.All(), Times.Once);
        }

        #endregion GetAll

    }
}
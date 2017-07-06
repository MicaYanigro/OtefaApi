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
    public class AreaServiceTests
    {

        #region Create

        [Test]
        public void Should_Create_An_Area()
        {
            // Arrange

            var description = new string('x', 30);
            var briefDescription = new string('x', 2);

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var areaRepositoryMock = new Mock<IAreaRepository>();
            areaRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var areaService = new AreaService();

            areaService.AreaRepository = areaRepositoryMock.Object;

            var areaMock = new Mock<Area>();

            var areaFactoryMock = new Mock<IAreaFactory>();
            areaFactoryMock.Setup(x => x.Create(description, briefDescription)).Returns(areaMock.Object);

            areaService.AreaFactory = areaFactoryMock.Object;

            var userServiceMock = new Mock<IUserService>();
            areaService.UserService = userServiceMock.Object;

            // Act

            var createdArea = areaService.Create(description, briefDescription);

            // Assert

            areaFactoryMock.Verify(x => x.Create(description, briefDescription), Times.Once);
            areaRepositoryMock.Verify(x => x.Add(areaMock.Object), Times.Once);
            repositoryContextMock.Verify(x => x.Commit(), Times.Once);
        }

        #endregion Create

        #region GetAll

        [Test]
        public void Should_Get_All_Areas()
        {
            // Arrange

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var areaRepositoryMock = new Mock<IAreaRepository>();
            areaRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            AreaService areaService = new AreaService();
            areaService.AreaRepository = areaRepositoryMock.Object;

            // Act

            var areas = areaService.GetAll();

            // Assert

            Assert.That(areas, Is.InstanceOf<List<Area>>());
            Assert.That(areas, Is.Not.Null);

            areaRepositoryMock.Verify(x => x.All(), Times.Once);
        }

        #endregion GetAll

    }
}
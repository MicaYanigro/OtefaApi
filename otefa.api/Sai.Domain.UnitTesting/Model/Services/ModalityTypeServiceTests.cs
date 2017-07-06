using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Repositories;
using Sai.Domain.Model.Services;
using System;
using System.Collections.Generic;

namespace Sai.Domain.UnitTesting.Model.Services
{
    [TestFixture]
    public class ModalityTypeServiceTests
    {

        #region Create

        [Test]
        public void Should_Create_A_ModalityType()
        {
            // Arrange

            var description = new string('x', 30);

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var modalityTypeRepositoryMock = new Mock<IModalityTypeRepository>();
            modalityTypeRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var modalityTypeService = new ModalityTypeService();

            modalityTypeService.ModalityTypeRepository = modalityTypeRepositoryMock.Object;

            var modalityTypeServiceMock = new Mock<IUserService>();
            modalityTypeService.UserService = modalityTypeServiceMock.Object;

            // Act

            var createdProgram = modalityTypeService.Create(description);

            // Assert

            repositoryContextMock.Verify(x => x.Commit(), Times.Once);
        }


        #endregion Create

        #region GetAll

        [Test]
        public void Should_Get_All_ModalityType()
        {
            // Arrange

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var modalityTypeRepositoryMock = new Mock<IModalityTypeRepository>();
            modalityTypeRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            ModalityTypeService modalityTypeService = new ModalityTypeService();
            modalityTypeService.ModalityTypeRepository = modalityTypeRepositoryMock.Object;

            // Act

            var categories = modalityTypeService.GetAll();

            // Assert

            Assert.That(categories, Is.InstanceOf<List<ModalityType>>());
            Assert.That(categories, Is.Not.Null);

            modalityTypeRepositoryMock.Verify(x => x.All(), Times.Once);
        }

        #endregion GetAll

    }
}
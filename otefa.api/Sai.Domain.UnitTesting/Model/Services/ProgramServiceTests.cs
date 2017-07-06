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
    public class ProgramServiceTests
    {

        #region Create

        [Test]
        public void Should_Create_A_Program()
        {
            // Arrange

            var description = new string('x', 40);

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var programRepositoryMock = new Mock<IProgramRepository>();
            programRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var programService = new ProgramService();

            programService.ProgramRepository = programRepositoryMock.Object;

            var userServiceMock = new Mock<IUserService>();
            programService.UserService = userServiceMock.Object;

            // Act

            var createdProgram = programService.Create(description);

            // Assert

            repositoryContextMock.Verify(x => x.Commit(), Times.Once);
        }



        #endregion Create

        #region GetAll

        [Test]
        public void Should_Get_All_Programs()
        {
            // Arrange

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var programRepositoryMock = new Mock<IProgramRepository>();
            programRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            ProgramService programService = new ProgramService();
            programService.ProgramRepository = programRepositoryMock.Object;

            // Act

            var categories = programService.GetAll();

            // Assert

            Assert.That(categories, Is.InstanceOf<List<Program>>());
            Assert.That(categories, Is.Not.Null);

            programRepositoryMock.Verify(x => x.All(), Times.Once);
        }

        #endregion GetAll

    }
}
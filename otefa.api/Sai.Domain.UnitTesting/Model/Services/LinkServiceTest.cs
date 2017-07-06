using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Commission;
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
    public class LinkServiceTests
    {

        #region Create

        [Test]
        public void Should_Create()
        {
            // Arrange

            var url = new string('x', 30);

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var linkRepositoryMock = new Mock<ILinkRepository>();
            linkRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);

            var linkService = new LinkService();

            linkService.LinkRepository = linkRepositoryMock.Object;

            // Act

            var createdLink = linkService.Create(url);

            // Assert

            linkRepositoryMock.Verify(x => x.Add(It.IsAny<Link>()), Times.Once);
            repositoryContextMock.Verify(x => x.Commit(), Times.Once);
        }

        #endregion Create

        [Test]
        public void Should_GetByHash()
        {
            // Arrange

            var hash = new string('x', 20);
            var linkMock = new Mock<Link>();

            var repositoryContextMock = new Mock<IRepositoryContext>();

            var linkRepositoryMock = new Mock<ILinkRepository>();
            linkRepositoryMock.SetupGet(x => x.Context).Returns(repositoryContextMock.Object);
            linkRepositoryMock.Setup(x => x.GetByHash(It.IsAny<string>())).Returns(linkMock.Object);

            var linkService = new LinkService();

            linkService.LinkRepository = linkRepositoryMock.Object;

            // Act

            var link = linkService.GetByHash(hash);

            // Assert

            linkRepositoryMock.Verify(x => x.GetByHash(It.IsAny<string>()), Times.Once);
            Assert.That(link, Is.EqualTo(linkMock.Object));
        }




    }
}
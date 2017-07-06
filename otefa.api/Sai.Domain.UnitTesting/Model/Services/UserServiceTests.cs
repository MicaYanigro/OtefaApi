using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Services;
using Moq;
using System.Security.Principal;
using Sai.Domain.Model.Exceptions;

namespace Sai.Domain.UnitTesting.Model.Services
{
    public class UserServiceTests
    {

        #region Authorize

        [Test]
        public void Should_Authorize()
        {
            // Arrange

            var role = Roles.GeneralAdministrator;

            var principalMock = new Mock<IPrincipal>();

            principalMock.Setup(x => x.IsInRole(role.ToString())).Returns(true);

            var threadWrapperMock = new Mock<IThreadWrapper>();

            threadWrapperMock.Setup(x => x.GetCurrentPrincipal()).Returns(principalMock.Object);

            var userService = new UserService();

            userService.ThreadWrapper = threadWrapperMock.Object;

            // Act

            userService.Authorize(role);
        }

        [Test]
        public void Should_Authorize_But_Fails_Because_User_Is_Not_In_Role()
        {
            // Arrange

            var role = Roles.GeneralAdministrator;

            var principalMock = new Mock<IPrincipal>();

            principalMock.Setup(x => x.IsInRole(role.ToString())).Returns(false);

            var threadWrapperMock = new Mock<IThreadWrapper>();

            threadWrapperMock.Setup(x => x.GetCurrentPrincipal()).Returns(principalMock.Object);

            var userService = new UserService();

            userService.ThreadWrapper = threadWrapperMock.Object;

            // Act

            var exception = Assert.Catch(() => userService.Authorize(role));

            // Assert

            Assert.That(exception, Is.InstanceOf<AuthorizationException>());
        }

        #endregion Authorize

    }
}
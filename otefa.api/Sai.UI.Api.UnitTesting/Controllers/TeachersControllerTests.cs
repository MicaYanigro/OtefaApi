using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities.Activity;
using Sai.Domain.Model.Exceptions;
using Sai.Domain.Model.Services;
using Sai.UI.Api.Controllers;
using Sai.UI.Api.ViewModel.Teacher;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace Sai.UI.Api.UnitTesting.Controllers
{
    [TestFixture]
    public class TeachersControllerTests
    {

        #region Post

        [Test]
        public void Should_Post()
        {

            // Arrange

            var TeacherDomainMock = new Mock<Teacher>();
            var TeacherServiceMock = new Mock<ITeacherService>();
            var TeachersController = new TeachersController(TeacherServiceMock.Object);

            var cuil = new string('x', 30);
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = new string('x', 30);
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var institutionID = 1;
            var gdeNumber = "132";
            var rlmNumber = "123";

            var TeacherViewModelMock = new TeacherViewModel
            {
                Cuil = cuil,
                Name = name,
                LastName = lastName,
                Email = email,
                TeacherInscriptionType = teacherInscriptionType,
                IsInstitutional = isInstitutional,
                InstitutionID = institutionID,
                GdeNumber = gdeNumber,
                RlmNumber = rlmNumber
            };

            //  TeacherServiceMock.Setup(x => x.Create(cuil, name, lastName, email, teacherInscriptionType, isInstitutional, institutionID, rlmNumber, gdeNumber)).Returns(TeacherDomainMock.Object);

            TeachersController.Request = new HttpRequestMessage();
            TeachersController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = TeachersController.Post(TeacherViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            TeacherServiceMock.Verify(x => x.Create(cuil, name, lastName, email, teacherInscriptionType, isInstitutional, institutionID, rlmNumber, gdeNumber), Times.Once);

        }

        [Test]
        public void Should_Post_But_Fails()
        {

            // Arrange

            var TeacherDomainMock = new Mock<Teacher>();
            var TeacherServiceMock = new Mock<ITeacherService>();
            var TeachersController = new TeachersController(TeacherServiceMock.Object);

            var cuil = new string('x', 30);
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = new string('x', 30);
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var institutionID = 1;
            var gdeNumber = "132";
            var rlmNumber = "123";

            var TeacherViewModelMock = new TeacherViewModel
            {
                Cuil = cuil,
                Name = name,
                LastName = lastName,
                Email = email,
                TeacherInscriptionType = teacherInscriptionType,
                IsInstitutional = isInstitutional,
                InstitutionID = institutionID,
                GdeNumber = gdeNumber,
                RlmNumber = rlmNumber
            };

            TeacherServiceMock.Setup(x => x.Create(cuil, name, lastName, email, teacherInscriptionType, isInstitutional, institutionID, rlmNumber, gdeNumber)).Throws(new ExceptionBase());

            TeachersController.Request = new HttpRequestMessage();
            TeachersController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            // Act

            var response = TeachersController.Post(TeacherViewModelMock);

            // Assert

            Assert.That(response, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

            TeacherServiceMock.Verify(x => x.Create(cuil, name, lastName, email, teacherInscriptionType, isInstitutional, institutionID, rlmNumber, gdeNumber), Times.Once);

        }

        #endregion Post

    }
}
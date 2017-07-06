using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Activity;
using Sai.Domain.Model.Exceptions.Teacher;
using Sai.Domain.Model.Services;
using Sai.Infrastructure.IoC;

namespace Sai.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class TeacherTests
    {

        [Test]
        public void Should_Create_Teacher()
        {
            // Arrange

            var cuil = "33-69345023-9";
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = "test@test.com";
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var institutionID = 1;
            var gdeNumber = "132";
            var rlmNumber = "AA-2017-1-X-A";
            var institution = new Mock<InstitutionalProvider>();

            var TeacherMock = new Mock<Teacher>();
            var TeacherServiceMock = new Mock<ITeacherService>();
            var containerMock = new Mock<IContainer>();
            containerMock.Setup(x => x.Resolve<ITeacherService>()).Returns(TeacherServiceMock.Object);
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);

            Container.Current = containerMock.Object;

            // Act

            var teacher = new Teacher(cuil, name, lastName, email, (TeacherInscriptionType)teacherInscriptionType, isInstitutional, institution.Object, rlmNumber, gdeNumber);

            // Assert

            Assert.That(teacher.Cuil, Is.EqualTo(cuil));
            Assert.That(teacher.LastName, Is.EqualTo(lastName));
            Assert.That(teacher.Name, Is.EqualTo(name));
            Assert.That(teacher.Email, Is.EqualTo(email));
            Assert.That(teacher.TeacherInscriptionType, Is.EqualTo((TeacherInscriptionType)teacherInscriptionType));
            Assert.That(teacher.IsInstitutional, Is.EqualTo(isInstitutional));
            Assert.That(teacher.Institution, Is.EqualTo(institution.Object));

        }

        [Test]
        public void Should_Create_An_Teacher_But_Fails_Because_Cuil_Is_Null()
        {
            //Arrange
            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var cuil = (string)null;
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = "test@test.com";
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var institutionID = 1;
            var gdeNumber = "132";
            var rlmNumber = "123";
            var institution = new Mock<InstitutionalProvider>();

            var TeacherMock = new Mock<Teacher>();
            var TeacherServiceMock = new Mock<ITeacherService>();
            containerMock.Setup(x => x.Resolve<ITeacherService>()).Returns(TeacherServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Teacher(cuil, name, lastName, email, (TeacherInscriptionType)teacherInscriptionType, isInstitutional, institution.Object, rlmNumber, gdeNumber));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<NullTeacherCuilException>());
        }

        [Test]
        public void Should_Create_An_Teacher_But_Fails_Because_Cuil_Is_Empty()
        {
            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            // Arrange
            var cuil = "";
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = "test@test.com";
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var institutionID = 1;
            var gdeNumber = "132";
            var rlmNumber = "123";
            var institution = new Mock<InstitutionalProvider>();

            var TeacherMock = new Mock<Teacher>();
            var TeacherServiceMock = new Mock<ITeacherService>();
            containerMock.Setup(x => x.Resolve<ITeacherService>()).Returns(TeacherServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Teacher(cuil, name, lastName, email, (TeacherInscriptionType)teacherInscriptionType, isInstitutional, institution.Object, rlmNumber, gdeNumber));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<EmptyTeacherCuilException>());
        }

        [Test]
        public void Should_Create_An_Teacher_But_Fails_Because_Cuil_Format_Is_Invalid()
        {
            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            // Arrange
            var cuil = new string('x', 50);
            var lastName = new string('x', 30);
            var name = new string('x', 30);
            var email = "test@test.com";
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var institutionID = 1;
            var gdeNumber = "132";
            var rlmNumber = "123";
            var institution = new Mock<InstitutionalProvider>();

            var TeacherMock = new Mock<Teacher>();
            var TeacherServiceMock = new Mock<ITeacherService>();
           containerMock.Setup(x => x.Resolve<ITeacherService>()).Returns(TeacherServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Teacher(cuil, name, lastName, email, (TeacherInscriptionType)teacherInscriptionType, isInstitutional, institution.Object, rlmNumber, gdeNumber));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<InvalidTeacherCuilFormatException>());
        }

        [Test]
        public void Should_Create_A_Teacher_But_Fails_Because_LastName_Is_Null()
        {
            // Arrange
            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var cuil = "33-69345023-9";
            var lastName = (string)null;
            var name = new string('x', 30);
            var email = "test@test.com";
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var institutionID = 1;
            var gdeNumber = "132";
            var rlmNumber = "123";
            var institution = new Mock<InstitutionalProvider>();

            var TeacherMock = new Mock<Teacher>();
            var TeacherServiceMock = new Mock<ITeacherService>();
            containerMock.Setup(x => x.Resolve<ITeacherService>()).Returns(TeacherServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Teacher(cuil, name, lastName, email, (TeacherInscriptionType)teacherInscriptionType, isInstitutional, institution.Object, rlmNumber, gdeNumber));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<NullTeacherLastNameException>());
        }


        [Test]
        public void Should_Create_A_Teacher_But_Fails_Because_LastName_Is_Empty()
        {
            // Arrange

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var cuil = "33-69345023-9";
            var lastName = "";
            var name = new string('x', 30);
            var email = "test@test.com";
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var institutionID = 1;
            var gdeNumber = "132";
            var rlmNumber = "123";
            var institution = new Mock<InstitutionalProvider>();

            var TeacherMock = new Mock<Teacher>();
            var TeacherServiceMock = new Mock<ITeacherService>();
           containerMock.Setup(x => x.Resolve<ITeacherService>()).Returns(TeacherServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Teacher(cuil, name, lastName, email, (TeacherInscriptionType)teacherInscriptionType, isInstitutional, institution.Object, rlmNumber, gdeNumber));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<EmptyTeacherLastNameException>());
        }

        [Test]
        public void Should_Create_A_Teacher_But_Fails_Because_LastName_Lenght_Is_Invalid()
        {
            // Arrange

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var cuil = "33-69345023-9";
            var lastName = new string('x', 51);
            var name = new string('x', 50);
            var email = "test@test.com";
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var institutionID = 1;
            var gdeNumber = "132";
            var rlmNumber = "123";
            var institution = new Mock<InstitutionalProvider>();

            var TeacherMock = new Mock<Teacher>();
            var TeacherServiceMock = new Mock<ITeacherService>();
            containerMock.Setup(x => x.Resolve<ITeacherService>()).Returns(TeacherServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Teacher(cuil, name, lastName, email, (TeacherInscriptionType)teacherInscriptionType, isInstitutional, institution.Object, rlmNumber, gdeNumber));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<InvalidTeacherLastNameLenghtException>());
        }

        [Test]
        public void Should_Create_A_Teacher_But_Fails_Because_Name_Is_Null()
        {
            // Arrange

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var cuil = "33-69345023-9";
            var lastName = new string('x', 50);
            var name = (string)null;
            var email = "test@test.com";
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var institutionID = 1;
            var gdeNumber = "132";
            var rlmNumber = "123";
            var institution = new Mock<InstitutionalProvider>();

            var TeacherMock = new Mock<Teacher>();
            var TeacherServiceMock = new Mock<ITeacherService>();
           containerMock.Setup(x => x.Resolve<ITeacherService>()).Returns(TeacherServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Teacher(cuil, name, lastName, email, (TeacherInscriptionType)teacherInscriptionType, isInstitutional, institution.Object, rlmNumber, gdeNumber));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<NullTeacherNameException>());
        }


        [Test]
        public void Should_Create_A_Teacher_But_Fails_Because_Name_Is_Empty()
        {
            // Arrange

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var cuil = "33-69345023-9";
            var lastName = new string('x', 50);
            var name = "";
            var email = "test@test.com";
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var institutionID = 1;
            var gdeNumber = "132";
            var rlmNumber = "123";
            var institution = new Mock<InstitutionalProvider>();

            var TeacherMock = new Mock<Teacher>();
            var TeacherServiceMock = new Mock<ITeacherService>();
            containerMock.Setup(x => x.Resolve<ITeacherService>()).Returns(TeacherServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Teacher(cuil, name, lastName, email, (TeacherInscriptionType)teacherInscriptionType, isInstitutional, institution.Object, rlmNumber, gdeNumber));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<EmptyTeacherNameException>());
        }

        [Test]
        public void Should_Create_A_Teacher_But_Fails_Because_Name_Lenght_Is_Invalid()
        {
            // Arrange

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var cuil = "33-69345023-9";
            var lastName = new string('x', 50);
            var name = new string('x', 51);
            var email = "test@test.com";
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var institutionID = 1;
            var gdeNumber = "132";
            var rlmNumber = "123";
            var institution = new Mock<InstitutionalProvider>();

            var TeacherMock = new Mock<Teacher>();
            var TeacherServiceMock = new Mock<ITeacherService>();
           containerMock.Setup(x => x.Resolve<ITeacherService>()).Returns(TeacherServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Teacher(cuil, name, lastName, email, (TeacherInscriptionType)teacherInscriptionType, isInstitutional, institution.Object, rlmNumber, gdeNumber));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<InvalidTeacherNameLenghtException>());
        }

        [Test]
        public void Should_Create_A_Teacher_But_Fails_Because_Email_Is_Null()
        {
            // Arrange

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var cuil = "33-69345023-9";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = (string)null;
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var institutionID = 1;
            var gdeNumber = "132";
            var rlmNumber = "123";
            var institution = new Mock<InstitutionalProvider>();

            var TeacherMock = new Mock<Teacher>();
            var TeacherServiceMock = new Mock<ITeacherService>();
           containerMock.Setup(x => x.Resolve<ITeacherService>()).Returns(TeacherServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Teacher(cuil, name, lastName, email, (TeacherInscriptionType)teacherInscriptionType, isInstitutional, institution.Object, rlmNumber, gdeNumber));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<NullTeacherEmailException>());
        }


        [Test]
        public void Should_Create_A_Teacher_But_Fails_Because_Email_Is_Empty()
        {
            // Arrange


            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var cuil = "33-69345023-9";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = "";
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var institutionID = 1;
            var gdeNumber = "132";
            var rlmNumber = "123";
            var institution = new Mock<InstitutionalProvider>();

            var TeacherMock = new Mock<Teacher>();
            var TeacherServiceMock = new Mock<ITeacherService>();
             containerMock.Setup(x => x.Resolve<ITeacherService>()).Returns(TeacherServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Teacher(cuil, name, lastName, email, (TeacherInscriptionType)teacherInscriptionType, isInstitutional, institution.Object, rlmNumber, gdeNumber));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<EmptyTeacherEmailException>());
        }

        [Test]
        public void Should_Create_A_Teacher_But_Fails_Because_Email_Lenght_Is_Invalid()
        {
            // Arrange

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var cuil = "33-69345023-9";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = new string('x', 51);
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var institutionID = 1;
            var gdeNumber = "132";
            var rlmNumber = "123";
            var institution = new Mock<InstitutionalProvider>();

            var TeacherMock = new Mock<Teacher>();
            var TeacherServiceMock = new Mock<ITeacherService>();
            containerMock.Setup(x => x.Resolve<ITeacherService>()).Returns(TeacherServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Teacher(cuil, name, lastName, email, (TeacherInscriptionType)teacherInscriptionType, isInstitutional, institution.Object, rlmNumber, gdeNumber));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<InvalidEmailLenghtException>());
        }

        [Test]
        public void Should_Create_A_Teacher_But_Fails_Because_Email_Format_Is_Invalid()
        {
            // Arrange


            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var cuil = "33-69345023-9";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = new string('x', 50);
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var institutionID = 1;
            var gdeNumber = "132";
            var rlmNumber = "123";
            var institution = new Mock<InstitutionalProvider>();

            var TeacherMock = new Mock<Teacher>();
            var TeacherServiceMock = new Mock<ITeacherService>();
            containerMock.Setup(x => x.Resolve<ITeacherService>()).Returns(TeacherServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act
            var caughtException = Assert.Catch(() => new Teacher(cuil, name, lastName, email, (TeacherInscriptionType)teacherInscriptionType, isInstitutional, institution.Object, rlmNumber, gdeNumber));

            // Assert
            Assert.That(caughtException, Is.InstanceOf<InvalidTeacherEmailFormatException>());
        }

        [Test]
        public void Should_Create_Teacher_But_Fails_Because_Cuil_Already_Exists()
        {
            // Arrange

            var containerMock = new Mock<IContainer>();
            var userServiceMock = new Mock<IUserService>();
            containerMock.Setup(x => x.Resolve<IUserService>()).Returns(userServiceMock.Object);
            Container.Current = containerMock.Object;

            var cuil = "33-69345023-9";
            var lastName = new string('x', 50);
            var name = new string('x', 50);
            var email = "test@123.com";
            var teacherInscriptionType = 1;
            var isInstitutional = true;
            var institutionID = 1;
            var gdeNumber = "132";
            var rlmNumber = "123";
            var institution = new Mock<InstitutionalProvider>();

            var TeacherMock = new Mock<Teacher>();
            var TeacherServiceMock = new Mock<ITeacherService>();
            TeacherServiceMock.Setup(x => x.IsExistentCuil(cuil)).Returns(true);

            containerMock.Setup(x => x.Resolve<ITeacherService>()).Returns(TeacherServiceMock.Object);
            Container.Current = containerMock.Object;

            // Act

            var caughtException = Assert.Catch(() => new Teacher(cuil, name, lastName, email, (TeacherInscriptionType)teacherInscriptionType, isInstitutional, institution.Object, rlmNumber, gdeNumber));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<ExistantTeacherCuilException>());
        }

    }
}
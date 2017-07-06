using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Exceptions.ModalityType;

namespace Sai.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class ModalityTypeTests
    {

        [Test]
        public void Should_Create_A_ModalityType()
        {
            // Arrange

            var description = new string('x', 30);

            // Act

            var modalityType = new ModalityType(description);

            // Assert

            Assert.That(modalityType.Description, Is.EqualTo(description));
        }

        [Test]
        public void Should_Create_A_ModalityType_But_Fails_Because_Description_Length_Is_More_Than_30_Characters()
        {
            // Arrange

            var description = new string('x', 31);

            // Act

            var caughtException = Assert.Catch(() => new ModalityType(description));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<InvalidModalityTypeDescriptionLengthException>());
        }

        [Test]
        public void Should_Create_A_ModalityType_But_Fails_Because_Description_Is_Null()
        {
            // Arrange

            string description = null;

            // Act

            var caughtException = Assert.Catch(() => new ModalityType(description));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<NullModalityTypeDescriptionException>());
        }

        [Test]
        public void Should_Create_A_ModalityType_But_Fails_Because_Description_Is_Empty()
        {
            // Arrange

            var description = "";

            // Act

            var caughtException = Assert.Catch(() => new ModalityType(description));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<EmptyModalityTypeDescriptionException>());
        }

    }
}
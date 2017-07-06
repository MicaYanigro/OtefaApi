using NUnit.Framework;
using Sai.Domain.Model.Entities.Activity;
using Sai.Domain.Model.Exceptions.Activity.Duration;
using System;

namespace Sai.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class DurationTests
    {

        [Test]
        public void Should_Create_An_Duration()
        {
            // Arrange

            var random = new Random();

            var hours = random.Next(1, 10);
            var details = new string('x', 500);

            // Act

            var duration = new Duration(hours, details);

            // Assert

            Assert.That(duration.Hours, Is.EqualTo(hours));
            Assert.That(duration.Details, Is.EqualTo(details));
        }

        [Test]
        public void Should_Create_An_Duration_But_Fails_Because_Hours_Is_Less_Than_0()
        {
            // Arrange

            var hours = -1;
            var details = new string('x', 500);

            // Act

            var caughtException = Assert.Catch(() => new Duration(hours, details));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<DurationHoursLessThan0Exception>());
        }

        [Test]
        public void Should_Create_An_Duration_But_Fails_Because_Hours_Is_Greater_Than_99999()
        {
            // Arrange

            var hours = 100000;
            var details = new string('x', 500);

            // Act

            var caughtException = Assert.Catch(() => new Duration(hours, details));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<DurationHoursGreaterThan99999Exception>());
        }

        [Test]
        public void Should_Create_An_Duration_But_Fails_Because_Details_Length_Is_More_Than_500_Characters()
        {
            // Arrange

            var random = new Random();

            var hours = random.Next(1, 10);
            var details = new string('x', 501);

            // Act

            var caughtException = Assert.Catch(() => new Duration(hours, details));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<InvalidDurationDetailsLengthException>());
        }

        [Test]
        public void Should_Create_An_Duration_But_Fails_Because_Details_Is_Null()
        {
            // Arrange

            var random = new Random();

            var hours = random.Next(1, 10);
            string details = null;

            // Act

            var caughtException = Assert.Catch(() => new Duration(hours, details));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<NullDurationDetailsException>());
        }

        [Test]
        public void Should_Create_An_Duration_But_Fails_Because_Details_Is_Empty()
        {
            //Arrange

            var random = new Random();

            var hours = random.Next(1, 10);
            var details = "";

            // Act

            var caughtException = Assert.Catch(() => new Duration(hours, details));

            // Assert

            Assert.That(caughtException, Is.InstanceOf<EmptyDurationDetailsException>());
        }

    }
}
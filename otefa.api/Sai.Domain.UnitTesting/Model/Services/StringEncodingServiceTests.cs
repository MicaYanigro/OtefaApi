using Sai.Domain.Model.Services;
using System;
using NUnit.Framework;

namespace Sai.Domain.UnitTesting.Model.Services
{
    [TestFixture]
    public class StringEncodingServiceTests
    {

        #region Encode

        [Test]
        public void Should_Encode()
        {
            // Arrange

            var stringEncodingService = new StringEncodingService();

            var plainText = "TEST";

            // Act

            var encodedText = stringEncodingService.Encode(plainText);

            // Assert

            Assert.That(encodedText, Is.EqualTo("VEVTVA%3d%3d"));
        }

        [Test]
        public void Should_Encode_But_Fails_Because_Plain_Text_Is_Null()
        {
            // Arrange

            var stringEncodingService = new StringEncodingService();

            string plainText = null;

            // Act

            var exception = Assert.Catch(() => stringEncodingService.Encode(plainText));

            // Assert

            Assert.That(exception, Is.InstanceOf<ArgumentException>());
            Assert.That(exception.Message, Is.EqualTo("Plain text cannot be null"));
        }

        [Test]
        public void Should_Encode_But_Fails_Because_Plain_Text_Is_Empty()
        {
            // Arrange

            var stringEncodingService = new StringEncodingService();

            var plainText = "";

            // Act

            var exception = Assert.Catch(() => stringEncodingService.Encode(plainText));

            // Assert

            Assert.That(exception, Is.InstanceOf<ArgumentException>());
            Assert.That(exception.Message, Is.EqualTo("Plain text cannot be empty"));
        }

        #endregion Encode

        #region Decode

        [Test]
        public void Should_Decode()
        {
            // Arrange

            var stringEncodingService = new StringEncodingService();

            var encodedText = "VEVTVA%3d%3d";

            // Act

            var decodedText = stringEncodingService.Decode(encodedText);

            // Assert

            Assert.That(decodedText, Is.EqualTo("TEST"));
        }

        [Test]
        public void Should_Decode_But_Fails_Because_Encoded_Text_Is_Null()
        {
            // Arrange

            var stringEncodingService = new StringEncodingService();

            string encodedText = null;

            // Act

            var exception = Assert.Catch(() => stringEncodingService.Decode(encodedText));

            // Assert

            Assert.That(exception, Is.InstanceOf<ArgumentException>());
            Assert.That(exception.Message, Is.EqualTo("Encoded text cannot be null"));
        }

        [Test]
        public void Should_Decode_But_Fails_Because_Encoded_Text_Is_Empty()
        {
            // Arrange

            var stringEncodingService = new StringEncodingService();

            var encodedText = "";

            // Act

            var exception = Assert.Catch(() => stringEncodingService.Decode(encodedText));

            // Assert

            Assert.That(exception, Is.InstanceOf<ArgumentException>());
            Assert.That(exception.Message, Is.EqualTo("Encoded text cannot be empty"));
        }

        #endregion Decode

    }
}
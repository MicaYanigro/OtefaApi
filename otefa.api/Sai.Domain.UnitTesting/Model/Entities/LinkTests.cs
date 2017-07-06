using Moq;
using NUnit.Framework;
using Sai.Domain.Model.Entities;
using Sai.Domain.Model.Entities.Commission;

namespace Sai.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class LinkTests
    {
        [Test]
        public void Should_Create()
        {
            // Arrange

            var url = "/v1/commission/enroll";

            // Act

            var link = new Link(url);

            // Assert

            Assert.That(link.Url, Is.SameAs(url));
            Assert.That(link.Hash, Is.Not.Null);
        }
    }
}
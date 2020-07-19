using System;
using NUnit.Framework;
using EventStore.Inspector.Common.Validation;

namespace EventStore.Inspector.Common.Tests.Validation
{
    [TestFixture]
    public class StreamNameValidatorTests
    {
        [TestCase("")]
        [TestCase("  ")]
        [TestCase(null)]
        public void OnNullOrEmptyStreamThrowsException(string stream)
        {
            var validator = new StreamNameValidator();

            Assert.Throws<ArgumentNullException>(() => validator.Validate(stream));
        }

        [TestCase("Test-123")]
        [TestCase("Test.Something-123")]
        [TestCase("$ce-Test")]
        [TestCase("$ce-Test.Something")]
        [TestCase("$et-Test")]
        public void OnValidStreamNameReturnsStreamName(string stream)
        {
            var validator = new StreamNameValidator();

            Assert.That(validator.Validate(stream), Is.EqualTo(stream));
        }
    }
}

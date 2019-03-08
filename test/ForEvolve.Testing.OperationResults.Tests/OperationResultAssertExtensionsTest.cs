using Moq;
using System;
using Xunit;
using Xunit.Sdk;

namespace ForEvolve.OperationResults.Tests
{
    public class OperationResultAssertExtensionsTest
    {
        private readonly Mock<IOperationResult> _operationResultMock = new Mock<IOperationResult>();
        public class AssertSucceeded : OperationResultAssertExtensionsTest
        {
            [Fact]
            public void Should_pass_when_result_is_Successful()
            {
                // Arrange
                _operationResultMock.Setup(x => x.Succeeded).Returns(true);

                // Act & Assert
                _operationResultMock.Object.AssertSucceeded();
            }

            [Fact]
            public void Should_throw_a_TrueException_when_the_result_is_not_Successful()
            {
                // Arrange
                _operationResultMock.Setup(x => x.Succeeded).Returns(false);

                // Act & Assert
                Assert.Throws<TrueException>(() => _operationResultMock.Object.AssertSucceeded());
            }
        }

        public class AssertFailed : OperationResultAssertExtensionsTest
        {
            [Fact]
            public void Should_throw_a_TrueException_when_result_is_Successful()
            {
                // Arrange
                _operationResultMock.Setup(x => x.Succeeded).Returns(true);

                // Act & Assert
                Assert.Throws<FalseException>(() => _operationResultMock.Object.AssertFailed());
            }

            [Fact]
            public void Should_pass_when_the_result_is_not_Successful()
            {
                // Arrange
                _operationResultMock.Setup(x => x.Succeeded).Returns(false);

                // Act & Assert
                _operationResultMock.Object.AssertFailed();
            }
        }
    }
}

using Moq;
using System;
using Xunit;

namespace ForEvolve.OperationResults.Tests
{
    public class OperationResultAssertExtensionsTest
    {
        public class Should : OperationResultAssertExtensionsTest
        {
            [Fact]
            public void Should_return_OperationResultAssertion()
            {
                // Arrange
                var operationResultMock = new Mock<IOperationResult>();

                // Act
                var result = operationResultMock.Object.Should();

                // Assert
                Assert.IsType<OperationResultAssertion>(result);
            }
        }

        public class Should_TResult : OperationResultAssertExtensionsTest
        {
            [Fact]
            public void Should_return_a_OperationResultAssertion_TResult()
            {
                // Arrange
                var operationResultMock = new Mock<IOperationResult<string>>();

                // Act
                var result = operationResultMock.Object.Should();

                // Assert
                Assert.IsType<OperationResultAssertion<string>>(result);
            }
        }
    }
}
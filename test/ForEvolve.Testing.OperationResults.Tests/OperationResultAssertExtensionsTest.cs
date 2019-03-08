using Moq;
using System;
using Xunit;
using Xunit.Sdk;

namespace ForEvolve.OperationResults.Tests
{
    public class OperationResultAssertExtensionsTest
    {
        private readonly Mock<IOperationResult> _operationResultMock = new Mock<IOperationResult>();
        private readonly Mock<IOperationResult<string>> _operationResultValueMock = new Mock<IOperationResult<string>>();

        public class Should : OperationResultAssertExtensionsTest
        {
            public class HaveSucceeded : Should
            {
                [Fact]
                public void Should_not_throw_when_result_is_Successful()
                {
                    // Arrange
                    _operationResultMock.Setup(x => x.Succeeded).Returns(true);

                    // Act & Assert
                    _operationResultMock.Object.Should().HaveSucceeded();
                }

                [Fact]
                public void Should_throw_a_TrueException_when_the_result_is_not_Successful()
                {
                    // Arrange
                    _operationResultMock.Setup(x => x.Succeeded).Returns(false);

                    // Act & Assert
                    Assert.Throws<TrueException>(()
                        => _operationResultMock.Object.Should().HaveSucceeded());
                }
            }

            public class HaveFailed : Should
            {
                [Fact]
                public void Should_throw_a_FalseException_when_result_is_Successful()
                {
                    // Arrange
                    _operationResultMock.Setup(x => x.Succeeded).Returns(true);

                    // Act & Assert
                    Assert.Throws<FalseException>(()
                        => _operationResultMock.Object.Should().HaveFailed());
                }

                [Fact]
                public void Should_not_throw_when_the_result_is_not_Successful()
                {
                    // Arrange
                    _operationResultMock.Setup(x => x.Succeeded).Returns(false);

                    // Act & Assert
                    _operationResultMock.Object.Should().HaveFailed();
                }
            }

            public class HaveAValue : Should
            {
                [Fact]
                public void Should_not_throw_when_the_result_has_a_value()
                {
                    // Arrange
                    _operationResultValueMock.Setup(x => x.HasValue()).Returns(true);

                    // Act & Assert
                    _operationResultValueMock.Object.Should().HaveAValue();
                }

                [Fact]
                public void Should_throw_a_TrueException_when_the_result_has_no_value()
                {
                    // Arrange
                    _operationResultValueMock.Setup(x => x.HasValue()).Returns(false);

                    // Act & Assert
                    Assert.Throws<TrueException>(()
                        => _operationResultValueMock.Object.Should().HaveAValue());
                }
            }

            public class HaveNoValue : Should
            {
                [Fact]
                public void Should_throw_a_FalseException_when_the_result_has_a_value()
                {
                    // Arrange
                    _operationResultValueMock.Setup(x => x.HasValue()).Returns(true);

                    // Act & Assert
                    Assert.Throws<FalseException>(()
                        => _operationResultValueMock.Object.Should().HaveNoValue());
                }

                [Fact]
                public void Should_not_throw_when_the_result_has_no_value()
                {
                    // Arrange
                    _operationResultValueMock.Setup(x => x.HasValue()).Returns(false);

                    // Act & Assert
                    _operationResultValueMock.Object.Should().HaveNoValue();
                }
            }
        }
    }
}
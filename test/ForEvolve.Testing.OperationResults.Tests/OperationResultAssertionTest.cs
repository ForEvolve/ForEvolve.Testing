using Moq;
using Xunit;
using Xunit.Sdk;

namespace ForEvolve.OperationResults.Tests
{
    public class OperationResultAssertionTest
    {
        private readonly OperationResultAssertion should;
        private readonly Mock<IOperationResult> _operationResultMock = new Mock<IOperationResult>();

        public OperationResultAssertionTest()
        {
            should = new OperationResultAssertion(_operationResultMock.Object);
        }

        public class HaveSucceeded : OperationResultAssertionTest
        {
            [Fact]
            public void Should_not_throw_when_result_is_Successful()
            {
                _operationResultMock.Setup(x => x.Succeeded).Returns(true);
                should.HaveSucceeded();
            }

            [Fact]
            public void Should_throw_a_TrueException_when_the_result_is_not_Successful()
            {
                _operationResultMock.Setup(x => x.Succeeded).Returns(false);
                Assert.Throws<TrueException>(() => should.HaveSucceeded());
            }
        }

        public class HaveFailed : OperationResultAssertionTest
        {
            [Fact]
            public void Should_throw_a_FalseException_when_result_is_Successful()
            {
                _operationResultMock.Setup(x => x.Succeeded).Returns(true);
                Assert.Throws<FalseException>(() => should.HaveFailed());
            }

            [Fact]
            public void Should_not_throw_when_the_result_is_not_Successful()
            {
                _operationResultMock.Setup(x => x.Succeeded).Returns(false);
                should.HaveFailed();
            }
        }
    }
}
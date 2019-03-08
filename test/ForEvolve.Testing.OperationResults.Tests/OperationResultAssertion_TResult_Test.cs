using Moq;
using Xunit;
using Xunit.Sdk;

namespace ForEvolve.OperationResults.Tests
{
    public class OperationResultAssertion_TResult_Test
    {
        private readonly OperationResultAssertion<string> should;
        private readonly Mock<IOperationResult<string>> _operationResultMock = new Mock<IOperationResult<string>>();

        public OperationResultAssertion_TResult_Test()
        {
            should = new OperationResultAssertion<string>(_operationResultMock.Object);
        }

        public class HaveSucceeded : OperationResultAssertion_TResult_Test
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

        public class HaveFailed : OperationResultAssertion_TResult_Test
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

        public class HaveAValue : OperationResultAssertion_TResult_Test
        {
            [Fact]
            public void Should_not_throw_when_the_result_has_a_value()
            {
                _operationResultMock.Setup(x => x.HasValue()).Returns(true);
                should.HaveAValue();
            }

            [Fact]
            public void Should_throw_a_TrueException_when_the_result_has_no_value()
            {
                _operationResultMock.Setup(x => x.HasValue()).Returns(false);
                Assert.Throws<TrueException>(() => should.HaveAValue());
            }

        }

        public class HaveNoValue : OperationResultAssertion_TResult_Test
        {
            [Fact]
            public void Should_throw_a_FalseException_when_the_result_has_a_value()
            {
                _operationResultMock.Setup(x => x.HasValue()).Returns(true);
                Assert.Throws<FalseException>(() => should.HaveNoValue());
            }

            [Fact]
            public void Should_not_throw_when_the_result_has_no_value()
            {
                _operationResultMock.Setup(x => x.HasValue()).Returns(false);
                should.HaveNoValue();
            }
        }
    }
}
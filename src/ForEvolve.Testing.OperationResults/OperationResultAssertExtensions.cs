using System;
using Xunit;

namespace ForEvolve.OperationResults
{
    public static class OperationResultAssertExtensions
    {
        public static OperationResultAssertion Should(this IOperationResult operationResult)
        {
            return new OperationResultAssertion(operationResult);
        }

        public static OperationResultWithValueAssertion<TResult> Should<TResult>(this IOperationResult<TResult> operationResult)
        {
            return new OperationResultWithValueAssertion<TResult>(operationResult);
        }
    }

    public class OperationResultAssertion
    {
        private readonly IOperationResult _operationResult;
        public OperationResultAssertion(IOperationResult operationResult)
        {
            _operationResult = operationResult ?? throw new ArgumentNullException(nameof(operationResult));
        }

        public void HaveSucceeded()
        {
            Assert.True(_operationResult.Succeeded, "Expected Succeeded to be true but it was false.");
        }

        public void HaveFailed()
        {
            Assert.False(_operationResult.Succeeded, "Expected Succeeded to be false but it was true.");
        }
    }

    public class OperationResultWithValueAssertion<TResult>
    {
        private readonly IOperationResult<TResult> _operationResult;

        public OperationResultWithValueAssertion(IOperationResult<TResult> operationResult)
        {
            _operationResult = operationResult ?? throw new ArgumentNullException(nameof(operationResult));
        }

        public void HaveAValue()
        {
            Assert.True(_operationResult.HasValue(), "The OperationResult was expected to have a Value.");
        }

        public void HaveNoValue()
        {
            Assert.False(_operationResult.HasValue(), "The OperationResult was not expected to have a Value.");
        }
    }
}
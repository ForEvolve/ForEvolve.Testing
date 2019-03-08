using System;
using Xunit;

namespace ForEvolve.OperationResults
{
    public static class OperationResultAssertExtensions
    {
        public static void ShouldHaveSucceeded(this IOperationResult operationResult)
        {
            Assert.True(operationResult.Succeeded, "Expected Succeeded to be true but it was false.");
        }

        public static void ShouldHaveFailed(this IOperationResult operationResult)
        {
            Assert.False(operationResult.Succeeded, "Expected Succeeded to be false but it was true.");
        }

        public static void ShouldHaveAValue<TResult>(this IOperationResult<TResult> operationResult)
        {
            Assert.True(operationResult.HasValue(), "The OperationResult was expected to have a Value.");
        }

        public static void ShouldHaveNoValue<TResult>(this IOperationResult<TResult> operationResult)
        {
            Assert.False(operationResult.HasValue(), "The OperationResult was not expected to have a Value.");
        }
    }
}
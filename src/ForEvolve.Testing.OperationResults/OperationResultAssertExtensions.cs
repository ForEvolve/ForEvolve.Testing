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

        public static OperationResultAssertion<TResult> Should<TResult>(this IOperationResult<TResult> operationResult)
        {
            return new OperationResultAssertion<TResult>(operationResult);
        }
    }
}
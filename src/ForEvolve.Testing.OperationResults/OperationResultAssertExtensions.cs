﻿using System;
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
    }
}

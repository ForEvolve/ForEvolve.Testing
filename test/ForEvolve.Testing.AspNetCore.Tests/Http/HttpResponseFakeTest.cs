using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.Testing.AspNetCore.Http
{
    public class HttpResponseFakeTest
    {
        [Fact]
        public async Task Should_support_WriteAsync()
        {
            // Arrange
            var helper = new HttpContextHelper();
            var sut = helper.HttpResponseFake;

            // Act
            await sut.WriteAsync("Some Text");

            // Assert
            sut.BodyShouldEqual("Some Text");
        }
    }
}

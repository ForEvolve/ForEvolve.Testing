using Microsoft.AspNetCore.Http;
using System.IO;
using Xunit;

namespace Microsoft.AspNetCore.Http
{
    public static class HttpResponseExtensions
    {
        public static void BodyShouldEqual(this HttpResponse httpResponse, string expectedBody)
        {
            httpResponse.Body.ShouldEqual(expectedBody);
        }
    }
}
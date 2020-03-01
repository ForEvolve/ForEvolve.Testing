using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Http.Features;

namespace ForEvolve.Testing.AspNetCore.Http
{
    public class HttpContextHelper
    {
        public HttpContext HttpContext => HttpContextMock?.Object;

        public Mock<IHttpContextAccessor> Mock { get; }
        public Mock<HttpContext> HttpContextMock { get; }
        public HttpRequest HttpRequest { get; }
        private HeaderDictionary HeaderDictionary { get; }
        public Mock<IResponseCookies> ResponseCookiesMock { get; set; }
        public HttpResponse HttpResponse { get; }
        public Mock<IFeatureCollection> FeaturesMock { get; }

        public HttpContextHelper()
        {
            // Create options
            Options = new HttpContextHelperOptions();

            // Create mocks
            Mock = new Mock<IHttpContextAccessor>();
            HttpContextMock = new Mock<HttpContext>();
            HeaderDictionary = new HeaderDictionary();
            HttpRequest = new HttpRequestFake(HttpContextMock.Object, HeaderDictionary);
            ResponseCookiesMock = new Mock<IResponseCookies>();
            HttpResponse = new HttpResponseFake(
                HttpContextMock.Object, 
                HeaderDictionary, 
                ResponseCookiesMock.Object
            );
            HttpResponse.Body = new MemoryStream();
            FeaturesMock = new Mock<IFeatureCollection>();

            Mock
                .Setup(x => x.HttpContext)
                .Returns(HttpContextMock.Object);
            HttpContextMock
                .Setup(x => x.Request)
                .Returns(() => HttpRequest);
            HttpContextMock
                .Setup(x => x.Response)
                .Returns(() => HttpResponse);
            HttpContextMock
                .Setup(x => x.Features)
                .Returns(() => FeaturesMock.Object);


            //HeaderDictionaryMock
            //    .Setup(x => x["Authorization"])
            //    .Returns(() => Options.ExpectedAuthorizationHeader);
        }

        public HttpContextHelperOptions Options { get; set; }
    }
}

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
        public HttpRequestFake HttpRequestFake { get; }
        public HeaderDictionary HeaderDictionary { get; }
        public Mock<IResponseCookies> ResponseCookiesMock { get; set; }
        public HttpResponseFake HttpResponseFake { get; }
        public Mock<IFeatureCollection> FeaturesMock { get; }

        public HttpContextHelper()
        {
            // Create options
            Options = new HttpContextHelperOptions();

            // Create mocks
            Mock = new Mock<IHttpContextAccessor>();
            HttpContextMock = new Mock<HttpContext>();
            HeaderDictionary = new HeaderDictionary();
            HttpRequestFake = new HttpRequestFake(HttpContextMock.Object, HeaderDictionary);
            ResponseCookiesMock = new Mock<IResponseCookies>();
            HttpResponseFake = new HttpResponseFake(
                HttpContextMock.Object, 
                HeaderDictionary, 
                ResponseCookiesMock.Object
            );
            HttpResponseFake.Body = new MemoryStream();
            FeaturesMock = new Mock<IFeatureCollection>();

            Mock
                .Setup(x => x.HttpContext)
                .Returns(HttpContextMock.Object);
            HttpContextMock
                .Setup(x => x.Request)
                .Returns(() => HttpRequestFake);
            HttpContextMock
                .Setup(x => x.Response)
                .Returns(() => HttpResponseFake);
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

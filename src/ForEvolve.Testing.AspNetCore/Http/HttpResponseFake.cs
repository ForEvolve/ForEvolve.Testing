using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;

namespace ForEvolve.Testing.AspNetCore.Http
{
    public class HttpResponseFake : HttpResponse
    {
        public HttpResponseFake(HttpContext httpContext, IHeaderDictionary headers, IResponseCookies cookies)
        {
            HttpContext = httpContext;
            Headers = headers;
            Cookies = cookies;
        }

        public override HttpContext HttpContext { get; }
        public override IHeaderDictionary Headers { get; }

        public override int StatusCode { get; set; }
        public override Stream Body { get; set; }
        public override long? ContentLength { get; set; }
        public override string ContentType { get; set; }

        public override IResponseCookies Cookies { get; }
        public override bool HasStarted { get; }

        public Action<Func<object, Task>, object> OnCompletedTwoArgsAction { get; set; }
        public Action<Func<object, Task>, object> OnStartingTwoArgsAction { get; set; }
        public Action<string, bool> RedirectAction { get; set; }

        public Action<Func<Task>> OnCompletedOneArgAction { get; set; }
        public Action<Func<Task>> OnStartingOneArgAction { get; set; }

        public Func<CancellationToken, Task> StartAsyncAction => (c) => Task.CompletedTask;

        public override void OnCompleted(Func<object, Task> callback, object state)
        {
            OnCompletedTwoArgsAction?.Invoke(callback, state);
        }

        public override void OnStarting(Func<object, Task> callback, object state)
        {
            OnStartingTwoArgsAction?.Invoke(callback, state);
        }

        public override void Redirect(string location, bool permanent)
        {
            RedirectAction?.Invoke(location, permanent);
        }

        public override void OnStarting(Func<Task> callback)
        {
            OnStartingOneArgAction?.Invoke(callback);
        }

        public override void OnCompleted(Func<Task> callback)
        {
            OnCompletedOneArgAction?.Invoke(callback);
        }

        public override Task StartAsync(CancellationToken cancellationToken = default)
        {
            return StartAsyncAction(cancellationToken);
        }

        private PipeWriter _bodyWriter = default;
        public override PipeWriter BodyWriter
        {
            get
            {
                if (_bodyWriter == default)
                {
                    _bodyWriter = PipeWriter.Create(Body);
                }
                return _bodyWriter;
            }
        }
    }
}

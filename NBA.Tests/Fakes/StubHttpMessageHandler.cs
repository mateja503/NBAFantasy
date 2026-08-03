using System.Net;
using System.Text;

namespace NBA.Tests.Fakes
{
    /// <summary>
    /// Test double for the transport under <see cref="HttpClient"/>. It is the seam that makes
    /// <c>BallDontLieClient</c> unit testable: every request is recorded and every response is
    /// scripted, so the tests never touch the network or the real balldontlie API.
    /// </summary>
    public sealed class StubHttpMessageHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, int, HttpResponseMessage> _respond;

        /// <param name="respond">Builds the response from the request and the zero-based attempt number.</param>
        public StubHttpMessageHandler(Func<HttpRequestMessage, int, HttpResponseMessage> respond)
        {
            _respond = respond;
        }

        /// <summary>Uri of every request the client sent, in order.</summary>
        public List<Uri> RequestUris { get; } = new();

        public int CallCount => RequestUris.Count;

        public Uri LastRequestUri => RequestUris.Count > 0
            ? RequestUris[^1]
            : throw new InvalidOperationException("The client sent no request.");

        /// <summary>Query of the last request with %5B%5D and friends decoded, so `dates[]=` can be asserted literally.</summary>
        public string LastRequestQuery => Uri.UnescapeDataString(LastRequestUri.Query);

        public static StubHttpMessageHandler RespondsWith(HttpStatusCode status, string json) =>
            new((_, _) => JsonResponse(status, json));

        public static StubHttpMessageHandler AlwaysFails(HttpStatusCode status) =>
            new((_, _) => JsonResponse(status, "{}"));

        public static HttpResponseMessage JsonResponse(HttpStatusCode status, string json) =>
            new(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") };

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            RequestUris.Add(request.RequestUri!);
            return Task.FromResult(_respond(request, RequestUris.Count - 1));
        }
    }
}

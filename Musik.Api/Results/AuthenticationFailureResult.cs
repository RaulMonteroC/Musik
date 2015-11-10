using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using System.Threading;
using System.Net;

namespace Musik.Api.Results
{
    public class AuthenticationFailureResult : IHttpActionResult
    {
        public string Reason { get; private set; }
        public HttpRequestMessage Request { get; private set; }

        public AuthenticationFailureResult(string reason, HttpRequestMessage request)
        {
            this.Reason = reason;
            this.Request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            var error = "{\"message\": \"" + Reason + "\"}";
            
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                RequestMessage = Request,
                ReasonPhrase = Reason,
                Content = new StringContent(error, Encoding.UTF8, "application/json")
            };
        

            return response;
        }
    }
}

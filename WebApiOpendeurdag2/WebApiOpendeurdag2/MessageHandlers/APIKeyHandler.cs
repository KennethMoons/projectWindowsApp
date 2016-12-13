using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApiOpendeurdag2.MessageHandlers
{
    public class APIKeyHandler : DelegatingHandler
    {
        private const string API_KEY = "mGI4VHw6fmhsPSG44oa8yhB9xlZGKyps";

        public APIKeyHandler()
        {

        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool isValidAPIKey = false;
            IEnumerable<string> lsHeaders;

            var checkApiKeyExists = request.Headers.TryGetValues("API_KEY", out lsHeaders);

            if (checkApiKeyExists)
            {
                if (lsHeaders.FirstOrDefault().Equals(API_KEY))
                {
                    isValidAPIKey = true;
                }
            }

            if (!isValidAPIKey)
            {
                return request.CreateResponse(HttpStatusCode.Forbidden, "Bad API Key");
            }

            var response = await base.SendAsync(request, cancellationToken);

            return response;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebApiOpendeurdag2.Models;
using WebApiOpendeurdag2.Security;

namespace WebApiOpendeurdag2.MessageHandlers
{
    public class AuthHandler : DelegatingHandler
    {
        private WebApiOpendeurdag2Context db = new WebApiOpendeurdag2Context();

        Gebruiker _user = null;

        private bool ValidateCredentials(AuthenticationHeaderValue authenticationHeaderValue)
        {
            try
            {
                // Ensure authentication header is supplied
                if (authenticationHeaderValue != null && !String.IsNullOrEmpty(authenticationHeaderValue.Parameter))
                {
                    // Retrieve username & password from Base64 encoded string
                    string[] decodedCredentials = Encoding.ASCII.GetString(Convert.FromBase64String(authenticationHeaderValue.Parameter)).Split(new[] { ':' });

                    string userName = decodedCredentials[0];
                    string password = decodedCredentials[1];

                    // Check if user exists in database with supplier username & password
                    var user = db.Gebruikers
                        .Where(g => g.Email == userName && g.Wachtwoord.Equals(password))
                        .FirstOrDefault();

                    // If all goes well, save user and return true
                    if (user != null)
                    {
                        _user = user;

                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Validate supplied credentials
            if (ValidateCredentials(request.Headers.Authorization))
            {
                // If successful, set current principal & user
                Thread.CurrentPrincipal = new APIPrincipal(_user);
                HttpContext.Current.User = new APIPrincipal(_user);
            }

            // Await response
            var response = await base.SendAsync(request, cancellationToken);

            // If header did not contain authentication header, add it.
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized && !response.Headers.Contains("WwwAuthenticate"))
            {
                response.Headers.Add("WwwAuthenticate", "Basic");
            }

            return response;
        }
    }
}
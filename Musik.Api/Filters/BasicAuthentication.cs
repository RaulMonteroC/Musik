using Musik.Api.Results;
using Musik.Core.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Security.Claims;
using System.Net.Http.Headers;

namespace Musik.Api.Filters
{
    public class BasicAuthentication : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple
        {
            get { return false; }
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            // No credentials where supplied
            if (authorization == null)
                return;

            // The server does not recognize the authorization scheme.
            if (authorization.Scheme != "Basic")
                return;

            // The credentials were bad.
            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing credentials", request);
                return;
            }

            // Credentials were invalid
            var credentials = GetCredentialsFromRequest(authorization.Parameter);            
            if(credentials == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid credentials", request);
                return;
            }

            // Credentials were supplied and were either correct or not.
            var principal = await AuthenticateAsync(credentials, cancellationToken);
            if (principal == null)
                context.ErrorResult = new AuthenticationFailureResult("Invalid username or password", request);
            else
                context.Principal = principal;
        }


        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var challenge = new AuthenticationHeaderValue("Basic");
            context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);

            return Task.FromResult(0);
        }

        private async Task<IPrincipal> AuthenticateAsync(Tuple<string,string> credentials, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var username = credentials.Item1;
            var password = credentials.Item2;

            // No user account found with the given credentials
            if (!AreValidCrendentials(credentials)) return null;

            var nameClaim = new Claim(ClaimTypes.Name, username);
            var claims = new List<Claim> { nameClaim };
            var identity = new ClaimsIdentity(claims, AuthenticationTypes.Basic);
            
            var principal = new ClaimsPrincipal(identity);

            return principal;

        }

        private bool AreValidCrendentials(Tuple<string,string> credentials)
        {            
            var username = credentials.Item1;
            var password = credentials.Item2;

            return AuthorizationHandler.AreValidCredentials(username, password);
        }

        //TODO Clean up GetCredentialsFromRequest method
        private Tuple<string, string> GetCredentialsFromRequest(string authorizationParameter)
        {
            byte[] credentialBytes;
            string decodedCredentials;

            try
            {
                credentialBytes = Convert.FromBase64String(authorizationParameter);
            }
            catch (FormatException)
            {
                return null;
            }

            Encoding encoding = Encoding.ASCII;
            encoding = (Encoding)encoding.Clone();
            encoding.DecoderFallback = DecoderFallback.ExceptionFallback;
                        

            try
            {
                decodedCredentials = encoding.GetString(credentialBytes);
            }
            catch (DecoderFallbackException)
            {
                return null;
            }

            if (String.IsNullOrEmpty(decodedCredentials))
            {
                return null;
            }

            int colonIndex = decodedCredentials.IndexOf(':');

            if (colonIndex == -1)
            {
                return null;
            }

            string userName = decodedCredentials.Substring(0, colonIndex);
            string password = decodedCredentials.Substring(colonIndex + 1);

            return new Tuple<string, string>(userName, password);
        }
    }
}

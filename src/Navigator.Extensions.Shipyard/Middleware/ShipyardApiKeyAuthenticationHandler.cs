using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Navigator.Extensions.Shipyard.Middleware
{
    internal class ShipyardApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly string _shipyardApiKey;

        public ShipyardApiKeyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IConfiguration configuration) : base(options, logger, encoder, clock)
        {
            _shipyardApiKey = configuration["SHIPYARD_API_KEY"];

        }

        /// <inheritdoc />
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Context.Request.Headers.TryGetValue("SHIPYARD-API-KEY", out var headerApiKey) && headerApiKey == _shipyardApiKey)
            {
                string username = "manager";
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, username, ClaimValueTypes.String, Options.ClaimsIssuer),
                    new Claim(ClaimTypes.Name, username, ClaimValueTypes.String, Options.ClaimsIssuer)
                };

                ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(claims, Scheme.Name));

                AuthenticationTicket ticket = new(principal, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }

            return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
        }
    }
}
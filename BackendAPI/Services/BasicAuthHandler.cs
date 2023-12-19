using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using BackendAPI.Models.Repository.IRepository;

namespace BackendAPI.Services
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IBasicLoginRepository _loginBasicRepositorio;

        public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IBasicLoginRepository loginBasicRepositorio
            ) : base(options, logger, encoder, clock) { _loginBasicRepositorio = loginBasicRepositorio; }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Error autenticacion");
            bool result;
            try
            {
                var autHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(autHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];
                result = _loginBasicRepositorio.IsUser(username, password);
            }
            catch
            {
                return AuthenticateResult.Fail("Username o password vacio");
            }

            if (!result)
                return AuthenticateResult.Fail("Username o password invalido");

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "id"),
                new Claim(ClaimTypes.Name, "user")
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}

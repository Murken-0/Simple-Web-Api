using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using Users.Application.Interfaces;
using Users.Domain;
using Microsoft.EntityFrameworkCore;

namespace Users.WebApi.Authentication
{
	public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		private readonly IUsersDbContext _dbContext;

		public BasicAuthenticationHandler(
			IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder,
			ISystemClock clock,
			IUsersDbContext context)
			: base(options, logger, encoder, clock)
		{
			_dbContext = context;
		}

		protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			if (!Request.Headers.ContainsKey("Authorization"))
				return AuthenticateResult.Fail("Authorization header not found");

			var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

			if (authHeader.Scheme != "Basic")
				return AuthenticateResult.Fail("Invalid authentication scheme");

			var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
			var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
			var login = credentials[0];
			var password = credentials[1];

			if (!await ValidateCredentials(login, password))
				return AuthenticateResult.Fail("Invalid login or password");

			var claims = new[] {
				new Claim(ClaimTypes.NameIdentifier, login),
				new Claim(ClaimTypes.Name, login),
			};
			var identity = new ClaimsIdentity(claims, Scheme.Name);
			var principal = new ClaimsPrincipal(identity);
			var ticket = new AuthenticationTicket(principal, Scheme.Name);

			return AuthenticateResult.Success(ticket);
		}

		public async Task<bool> ValidateCredentials(string login, string password)
		{
			var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == login);

			return user != null && user.Password == password;
		}
	}
}

using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;

namespace Users.Identity
{
	public class BasicAuthenticationHandler
	{
		public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
		{
			private readonly IUserService _userService;

			public BasicAuthenticationHandler(
				IOptionsMonitor<AuthenticationSchemeOptions> options,
				ILoggerFactory logger,
				UrlEncoder encoder,
				ISystemClock clock,
				IUserService userService)
				: base(options, logger, encoder, clock)
			{
				_userService = userService;
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
				var username = credentials[0];
				var password = credentials[1];

				if (!_userService.ValidateCredentials(username, password))
					return AuthenticateResult.Fail("Invalid username or password");

				var claims = new[] {
			new Claim(ClaimTypes.NameIdentifier, username),
			new Claim(ClaimTypes.Name, username),
		};
				var identity = new ClaimsIdentity(claims, Scheme.Name);
				var principal = new ClaimsPrincipal(identity);
				var ticket = new AuthenticationTicket(principal, Scheme.Name);

				return AuthenticateResult.Success(ticket);
			}
		}
	}
}
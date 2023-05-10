using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Users.Application;
using Users.Persistense;
using Users.WebApi.Middleware;
using Users.WebApi.Authentication;

namespace Users.WebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddApplication();
			builder.Services.AddPersistense(builder.Configuration);
			builder.Services.AddControllers();
			builder.Services.AddAuthentication("BasicAuthentication")
				.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
			builder.Services.AddAuthorization();
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", policy =>
				{
					policy.AllowAnyHeader();
					policy.AllowAnyMethod();
					policy.AllowAnyOrigin();
				});
			});

			var app = builder.Build();

			using (var scope = app.Services.CreateScope())
			{
				var servises = scope.ServiceProvider;

				try
				{
					var context = servises.GetRequiredService<UsersDbContext>();
					context.Database.EnsureCreated();
				}
				catch (Exception ex)
				{
					var logger = servises.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred while creating the database.");
				}
			}
			app.UseAuthentication();
			app.UseCostomExceptionHandler();
			app.UseRouting();
			app.UseHttpsRedirection();
			app.UseCors("AllowAll");
			app.UseAuthorization();
			app.MapControllers();

			app.Run();
			
		}
	}
}
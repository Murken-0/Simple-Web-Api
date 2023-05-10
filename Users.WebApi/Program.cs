using Users.Application;
using Users.Persistense;

namespace Users.WebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddApplication();
			builder.Services.AddPersistense(builder.Configuration);
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

			app.UseRouting();
			app.UseHttpsRedirection();
			app.UseCors("AllowAll");
			app.MapControllers();

			app.Run();
		}
	}
}
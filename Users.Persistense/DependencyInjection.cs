using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Interfaces;

namespace Users.Persistense
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistense(this IServiceCollection services,
			IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<UsersDbContext>(options =>
			{
				options.UseNpgsql(connectionString);
			});
			services.AddScoped<IUsersDbContext>(provider => 
				provider.GetService<UsersDbContext>());
			return services;
		}
	}
}

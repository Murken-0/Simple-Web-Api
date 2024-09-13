using Microsoft.EntityFrameworkCore;
using Users.Domain;

namespace Users.Application.Interfaces
{
	public interface IUsersDbContext
	{
		DbSet<User> Users { get; set; }
		DbSet<UserGroup> UserGroups { get; set; }
		DbSet<UserState> UserStates { get; set; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}

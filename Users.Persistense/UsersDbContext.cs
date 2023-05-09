using Microsoft.EntityFrameworkCore;
using Users.Application.Interfaces;
using Users.Domain;
using Users.Persistense.EntityTypeConfiguration;

namespace Users.Persistense
{
	public class UsersDbContext : DbContext, IUsersDbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<UserGroup> UserGroups { get; set; }
		public DbSet<UserState> UserStates { get; set; }

		public UsersDbContext(DbContextOptions<UsersDbContext> options)
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new UserGroupConfiguration());
			modelBuilder.ApplyConfiguration(new UserStateConfiguration());
			base.OnModelCreating(modelBuilder);
		}
	}
}

using Microsoft.EntityFrameworkCore;
using Users.Persistense;
using Users.Domain;

namespace Users.Tests.Common
{
	public class UsersContextFactory
	{
		public static int UserForDelete = 315;
		public static int UserForGet = 808;
		public static List<User> Users = new List<User>
		{
				new User
				{
					Id = UserForDelete,
					CreationTime = DateTime.Today,
					Login = "zrauba",
					Password = "password",
					GroupId = (int)UserGroup.Values.User,
					StateId = (int)UserState.Values.Active
				},
				new User
				{
					Id = UserForGet,
					CreationTime = DateTime.Today,
					Login = "abobus",
					Password = "password",
					GroupId = (int)UserGroup.Values.User,
					StateId = (int)UserState.Values.Active
				},
				new User
				{
					CreationTime = DateTime.Today,
					Login = "klown",
					Password = "password",
					GroupId = (int)UserGroup.Values.User,
					StateId = (int)UserState.Values.Active
				}
		};
		public static List<UserGroup> Groups = new List<UserGroup>
		{
			new UserGroup
			{
				Id = 1,
				Code = "Admin",
				Description = "Description"
			},
			new UserGroup
			{
				Id = 2,
				Code = "User",
				Description = "Description"
			}
		};

		public static List<UserState> States = new List<UserState>
		{
			new UserState
			{
				Id = 1,
				Code = "Active",
				Description = "Description"
			},
			new UserState
			{
				Id = 2,
				Code = "Blocked",
				Description = "Description"
			}
		};

		public static UsersDbContext Create()
		{
			var options = new DbContextOptionsBuilder<UsersDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString())
				.Options;
			var context = new UsersDbContext(options);
			context.Database.EnsureCreated();
			context.UserGroups.AddRange(Groups);
			context.UserStates.AddRange(States);
			context.Users.AddRange(Users);
			context.SaveChanges();
			return context;
		}

		public static void Destroy(UsersDbContext context)
		{
			context.Database.EnsureDeleted();
			context.Dispose();
		}
	}
}

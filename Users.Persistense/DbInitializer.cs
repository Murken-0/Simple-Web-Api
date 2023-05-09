namespace Users.Persistense
{
	public class DbInitializer
	{
		public static void Initialize(UsersDbContext context) 
		{
			context.Database.EnsureCreated();
		}
	}
}

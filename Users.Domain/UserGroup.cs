
namespace Users.Domain
{
	public class UserGroup
	{
		public enum Values
		{
			Admin = 1,
			User = 2
		}

		public int Id { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
		public ICollection<User> Users { get; set; }
	}
}

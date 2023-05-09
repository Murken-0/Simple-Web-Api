#nullable disable
namespace Users.Domain
{
	public class UserState
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
		public ICollection<User> Users { get; set; }
	}
}

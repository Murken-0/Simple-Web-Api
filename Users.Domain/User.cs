#nullable disable
namespace Users.Domain
{
	public class User
	{
		public Guid Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public DateTime CreationTime { get; set; }
		public Guid GroupId { get; set; }
        public UserGroup GroupRelation { get; set; }
		public UserGroup.Group Group { get; set; }
		public Guid StateId { get; set; }
		public UserState StateRelation { get; set; }
		public UserState.State State { get; set; }
    }
}

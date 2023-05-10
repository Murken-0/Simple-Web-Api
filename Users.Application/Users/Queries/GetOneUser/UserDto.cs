
namespace Users.Application.Users.Queries.GetOneUser
{
    public class UserDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreationTime { get; set; }
        public string GroupCode { get; set; }
        public string StateCode { get; set; }
    }
}

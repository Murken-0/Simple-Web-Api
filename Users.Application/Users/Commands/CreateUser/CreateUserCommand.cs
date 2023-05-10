using MediatR;


namespace Users.Application.Users.Commands.CreateUser
{
	public class CreateUserCommand : IRequest<int>
	{
		public string Login { get; set; }
        public string Password { get; set; }
        public int GroupId { get; set; }
    }
}

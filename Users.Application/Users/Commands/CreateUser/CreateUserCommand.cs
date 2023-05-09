using MediatR;

#nullable disable
namespace Users.Application.Users.Commands.CreateUser
{
	public class CreateUserCommand : IRequest<Guid>
	{
		public string Login { get; set; }
        public string Password { get; set; }
        public Guid GroupId { get; set; }
    }
}

using MediatR;

#nullable disable
namespace Users.Application.Users.Commands.DeleteUser
{
	public class DeleteUserCommand : IRequest
	{
		public int Id { get; set; }
    }
}

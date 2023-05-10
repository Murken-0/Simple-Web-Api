using MediatR;

#nullable disable
namespace Users.Application.Users.Commands.DeleteUser
{
	public class DaleteUserCommand : IRequest
	{
		public int Id { get; set; }
    }
}

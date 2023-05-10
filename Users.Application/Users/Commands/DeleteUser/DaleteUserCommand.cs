using MediatR;

#nullable disable
namespace Users.Application.Users.Commands.UpdateUser
{
	public class DaleteUserCommand : IRequest
	{
		public Guid Id { get; set; }
    }
}

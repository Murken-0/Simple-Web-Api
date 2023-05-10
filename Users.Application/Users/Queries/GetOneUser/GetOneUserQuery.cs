using MediatR;

namespace Users.Application.Users.Queries.GetOneUser
{
	public class GetOneUserQuery : IRequest<UserDto>
	{
		public int Id { get; set; }
	}
}

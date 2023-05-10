using MediatR;
using Users.Domain;

namespace Users.Application.Users.Queries.GetOneUser
{
	public class GetOneUserQuery : IRequest<User>
	{
		public Guid Id { get; set; }
	}
}

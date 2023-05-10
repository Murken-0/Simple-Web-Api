using MediatR;
using Users.Domain;

namespace Users.Application.Users.Queries.GetUserList
{
	public class GetUserListQuery : IRequest<IList<User>>
	{
	}
}

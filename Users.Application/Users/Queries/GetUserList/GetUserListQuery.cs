using MediatR;
using Users.Application.Users.Queries.GetOneUser;
using Users.Domain;

namespace Users.Application.Users.Queries.GetUserList
{
    public class GetUserListQuery : IRequest<IList<UserDto>>
	{
	}
}

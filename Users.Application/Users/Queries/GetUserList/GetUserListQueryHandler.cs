using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Application.Interfaces;
using Users.Application.Users.Queries.GetOneUser;

namespace Users.Application.Users.Queries.GetUserList
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, IList<UserDto>>
	{
		private readonly IUsersDbContext _dbContext;

		public GetUserListQueryHandler(IUsersDbContext context)
		{
			_dbContext = context;
		}

		public async Task<IList<UserDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
		{
			var entities = await _dbContext.Users
				.Include(u => u.State)
				.Include(u => u.Group)
				.Select(u => new UserDto()
				{
					Login = u.Login,
					Password = u.Password,
					CreationTime = u.CreationTime,
					GroupCode = u.Group.Code,
					StateCode = u.State.Code
				})
				.ToListAsync(cancellationToken);

			return entities;
		}
	}
}

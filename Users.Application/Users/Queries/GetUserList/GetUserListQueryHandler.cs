using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Application.Interfaces;
using Users.Domain;

namespace Users.Application.Users.Queries.GetUserList
{
	public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, IList<User>>
	{
		private readonly IUsersDbContext _dbContext;

		public GetUserListQueryHandler(IUsersDbContext context)
		{
			_dbContext = context;
		}

		public async Task<IList<User>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
		{
			var entities = await _dbContext.Users.ToListAsync();

			return entities;
		}
	}
}

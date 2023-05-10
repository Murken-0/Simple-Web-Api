using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Application.Interfaces;
using Users.Domain;
using Users.Application.Common.Exceptions;

namespace Users.Application.Users.Queries.GetOneUser
{
	internal class GetOneUserQueryHandler : IRequestHandler<GetOneUserQuery, User>
	{
		private readonly IUsersDbContext _dbContext;

		public GetOneUserQueryHandler(IUsersDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<User> Handle(GetOneUserQuery request, CancellationToken cancellationToken)
		{
			var entity = await _dbContext.Users
				.FirstOrDefaultAsync(u => u.Id == request.Id);

			if (entity == null) 
				throw new NotFoundException(nameof(User), request.Id);

			return entity;
		}
	}
}

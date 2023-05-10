using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Application.Interfaces;
using Users.Domain;
using Users.Application.Common.Exceptions;

namespace Users.Application.Users.Queries.GetOneUser
{
	internal class GetOneUserQueryHandler : IRequestHandler<GetOneUserQuery, UserDto>
	{
		private readonly IUsersDbContext _dbContext;

		public GetOneUserQueryHandler(IUsersDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<UserDto> Handle(GetOneUserQuery request, CancellationToken cancellationToken)
		{
			var entity = await _dbContext.Users
				.Include(u => u.Group)
				.Include(u => u.State)
				.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

			if (entity == null) 
				throw new NotFoundException(nameof(User), request.Id);

			return new UserDto()
			{
				Login = entity.Login,
				Password = entity.Password,
				CreationTime = entity.CreationTime,
				GroupCode = entity.Group.Code,
				StateCode = entity.State.Code
			};
		}
	}
}

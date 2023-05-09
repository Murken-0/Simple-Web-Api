using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Application.Interfaces;
using Users.Application.Common.Exceptions;
using Users.Domain;
using System.Net.Http.Headers;

namespace Users.Application.Users.Commands.UpdateUser
{
	public class DaleteUserCommandHandler : IRequestHandler<DaleteUserCommand>
	{
		private readonly IUsersDbContext _dbContext;

		public DaleteUserCommandHandler(IUsersDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Handle(DaleteUserCommand request, CancellationToken cancellationToken)
		{
			var entity = await _dbContext.Users
				.FindAsync(new object[] {request.Id}, cancellationToken);
			if (entity == null || entity.GroupId != request.GroupId || entity.StateId != request.StateId) 
			{
				throw new NotFoundException(nameof(User), request.Id);
			}

			entity.State = UserState.State.Blocked;
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}

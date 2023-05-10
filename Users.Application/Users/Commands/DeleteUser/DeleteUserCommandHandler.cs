using MediatR;
using Users.Application.Interfaces;
using Users.Application.Common.Exceptions;
using Users.Domain;
using Microsoft.EntityFrameworkCore;

namespace Users.Application.Users.Commands.DeleteUser
{
	public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
	{
		private readonly IUsersDbContext _dbContext;

		public DeleteUserCommandHandler(IUsersDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			var entity = await _dbContext.Users
				.FindAsync(new object[] {request.Id}, cancellationToken);
			if (entity == null) 
			{
				throw new NotFoundException(nameof(User), request.Id);
			}

			entity.State = await _dbContext.UserStates.FirstOrDefaultAsync(s => s.Code == "Blocked", cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}

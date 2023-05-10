using MediatR;
using Users.Application.Interfaces;
using Users.Application.Common.Exceptions;
using Users.Domain;
using Microsoft.EntityFrameworkCore;

namespace Users.Application.Users.Commands.DeleteUser
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
			if (entity == null) 
			{
				throw new NotFoundException(nameof(User), request.Id);
			}

			entity.State = await _dbContext.UserStates.FirstOrDefaultAsync(s => s.Code == "Blocked", cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}

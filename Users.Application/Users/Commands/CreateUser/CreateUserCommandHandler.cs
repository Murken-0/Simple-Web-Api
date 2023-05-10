using MediatR;
using Users.Application.Interfaces;
using Users.Domain;
using Users.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Users.Application.Users.Commands.CreateUser
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
	{
		private readonly IUsersDbContext _dbContext;

		public CreateUserCommandHandler(IUsersDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var admin = await _dbContext.Users.FirstOrDefaultAsync(u => 
				u.GroupId == (int)UserGroup.Values.Admin 
				&& u.StateId == (int)UserState.Values.Active, cancellationToken);
			if (admin != null && request.GroupId == (int)UserGroup.Values.Admin)
				throw new ManyAdminsException();

			var sameLogin = await _dbContext.Users.FirstOrDefaultAsync(u => 
				u.Login == request.Login, cancellationToken);
			if (sameLogin != null)
				throw new LoginAlreadyExistExeption(request.Login);

			var user = new User
			{
				Login = request.Login,
				Password = request.Password,
				CreationTime = DateTime.UtcNow,
				GroupId = request.GroupId,
				StateId = (int)UserState.Values.Active
			};

			await _dbContext.Users.AddAsync(user, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);
			await Task.Delay(5000, cancellationToken);

			return user.Id;
		}
	}
}

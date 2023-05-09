using MediatR;
using Users.Application.Interfaces;
using Users.Domain;

namespace Users.Application.Users.Commands.CreateUser
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
	{
		private readonly IUsersDbContext _dbContext;

		public CreateUserCommandHandler(IUsersDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var user = new User
			{
				Id = Guid.NewGuid(),
				Login = request.Login,
				Password = request.Password,
				Created = DateTime.Now,
				GroupId = request.GroupId,
				StateId = request.StateId
			};

			await _dbContext.Users.AddAsync(user, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return user.Id;
		}
	}
}

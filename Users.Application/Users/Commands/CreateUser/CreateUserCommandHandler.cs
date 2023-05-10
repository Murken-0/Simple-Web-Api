using MediatR;
using Users.Application.Interfaces;
using Users.Domain;
using Users.Application.Common.Exceptions;

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
			// TODO: Сделать фичу с 5 секундами
			var user = new User
			{
				Id = Guid.NewGuid(),
				Login = request.Login,
				Password = request.Password,
				CreationTime = DateTime.Now,
				GroupId = request.GroupId,
				State = UserState.State.Active
			};

			var adminsCount = _dbContext.Users.Where(u => u.Group == UserGroup.Group.Admin).Count();
			if (adminsCount > 0)
				throw new ManyAdminsException();

			await _dbContext.Users.AddAsync(user, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return user.Id;
		}
	}
}

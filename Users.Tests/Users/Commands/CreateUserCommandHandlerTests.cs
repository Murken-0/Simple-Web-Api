using Microsoft.EntityFrameworkCore;
using Users.Application.Common.Exceptions;
using Users.Application.Users.Commands.CreateUser;
using Users.Application.Users.Commands.DeleteUser;
using Users.Application.Users.Queries.GetOneUser;
using Users.Application.Users.Queries.GetUserList;
using Users.Domain;
using Users.Tests.Common;
using Xunit;

namespace Users.Tests.Users.Commands
{
	public class CreateUserCommandHandlerTests : TestCommandBase
	{
		[Fact]
		public async Task CreateUserCommandHandler_Success()
		{
			var handler = new CreateUserCommandHandler(Context);
			var login = "new_login";
			var password = "new_password";
			var groupId = (int)UserGroup.Values.User;
			var stateId = (int)UserState.Values.Active;

			var userId = await handler.Handle(
				new CreateUserCommand
				{
					Login = login,
					Password = password,
					GroupId = groupId
				}, CancellationToken.None);

			Assert.NotNull(
				await Context.Users.SingleOrDefaultAsync(u =>
					u.Id == userId && u.Login == login && u.Password == password 
					&& u.GroupId == groupId && u.StateId == stateId, CancellationToken.None));
		}

		[Fact]
		public async Task CreateUserCommandHandler_FailOnSameLogin()
		{
			var handler = new CreateUserCommandHandler(Context);
			var login = "login";
			var password = "password";
			var groupId = (int)UserGroup.Values.User;

			var userId = await handler.Handle(
				new CreateUserCommand
				{
					Login = login,
					Password = password,
					GroupId = groupId
				}, CancellationToken.None);

			await Assert.ThrowsAsync<LoginAlreadyExistExeption>(async () =>
				await handler.Handle(
					new CreateUserCommand
					{
						Login = login,
						Password = password,
						GroupId = groupId
					}, CancellationToken.None));
		}
	}
}

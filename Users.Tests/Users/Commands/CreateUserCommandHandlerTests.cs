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
			var login = "login";
			var password = "password";
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

			Assert.ThrowsAsync<LoginAlreadyExistExeption>(async () =>
				await handler.Handle(
					new CreateUserCommand
					{
						Login = login,
						Password = password,
						GroupId = groupId
					}, CancellationToken.None));
		}

		[Fact]
		public async Task DeleteUserCommandHandler_Success()
		{
			var handler = new DeleteUserCommandHandler(Context);

			await handler.Handle(new DeleteUserCommand
			{
				Id = UsersContextFactory.UserForDelete
			}, CancellationToken.None);
			var user = await Context.Users.SingleOrDefaultAsync(u =>
					u.Id == UsersContextFactory.UserForDelete, CancellationToken.None);

			Assert.NotNull(user);
			Assert.Equal(user.StateId, (int)UserState.Values.Blocked);
		}

		[Fact]
		public async Task DeleteUserCommandHandler_FailOnWrongId()
		{

			var handler = new DeleteUserCommandHandler(Context);

			await Assert.ThrowsAsync<NotFoundException>(async () =>
				await handler.Handle(
					new DeleteUserCommand
					{
						Id = int.MaxValue
					},
					CancellationToken.None));
		}

		public async Task GetOneUserCommandHandler_Success()
		{
			var handler = new GetOneUserQueryHandler(Context);

			var user = await handler.Handle(new GetOneUserQuery
			{
				Id = UsersContextFactory.UserForGet
			}, CancellationToken.None);

			Assert.NotNull(user);
		}

		[Fact]
		public async Task GetOneUserQueryHandler_FailOnNotFound()
		{
			var handler = new GetOneUserQueryHandler(Context);

			await Assert.ThrowsAsync<NotFoundException>(async () =>
				await handler.Handle(
					new GetOneUserQuery
					{
						Id = int.MaxValue
					},
					CancellationToken.None)); ;
		}

		[Fact]
		public async Task GetUserListQueryHandler_Success()
		{
			var handler = new GetUserListQueryHandler(Context);

			var users = await handler.Handle(new GetUserListQuery(), CancellationToken.None);

			Assert.NotNull(users);
			Assert.Equal(3, users.Count);
		}
	}
}

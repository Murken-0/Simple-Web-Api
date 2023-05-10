using Microsoft.EntityFrameworkCore;
using Users.Application.Common.Exceptions;
using Users.Application.Users.Commands.DeleteUser;
using Users.Domain;
using Users.Tests.Common;
using Xunit;

namespace Users.Tests.Users.Commands
{
	public class DeleteUserCommandHandlerTests : TestCommandBase
	{
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
			Assert.Equal((int)UserState.Values.Blocked, user.StateId);
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
	}
}

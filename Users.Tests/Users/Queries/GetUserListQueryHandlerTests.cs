using Users.Application.Users.Queries.GetUserList;
using Users.Tests.Common;
using Xunit;

namespace Users.Tests.Users.Queries
{
	public class GetUserListQueryHandlerTests : TestCommandBase
	{
		[Fact]
		public async Task GetUserListQueryHandler_Success()
		{
			var handler = new GetUserListQueryHandler(Context);

			var users = await handler.Handle(new GetUserListQuery(), CancellationToken.None);

			Assert.NotNull(users);
			Assert.Equal(UsersContextFactory.Users.Count, users.Count);
		}
	}
}

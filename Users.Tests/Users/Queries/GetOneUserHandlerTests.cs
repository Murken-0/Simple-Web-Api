using Users.Application.Common.Exceptions;
using Users.Application.Users.Queries.GetOneUser;
using Users.Tests.Common;
using Xunit;

namespace Users.Tests.Users.Queries
{
	public class GetOneUserHandlerTests : TestCommandBase
	{
		[Fact]
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
	}
}

using Microsoft.AspNetCore.Mvc;
using Users.Application.Users.Queries.GetUserList;
using Users.Application.Users.Queries.GetOneUser;
using Users.Application.Users.Commands.CreateUser;
using Users.Application.Users.Commands.DeleteUser;
using Microsoft.AspNetCore.Authorization;

namespace Users.WebApi.Controllers
{
    [Route("/api/Users")]
	public class UserController : BaseController
	{
		[Route("~/api/all")]
		[Authorize]
		[HttpGet]
		public async Task<ActionResult<IList<UserDto>>> GetAll()
		{
			var query = new GetUserListQuery();
			var vm = await Mediator.Send(query);
			return Ok(vm);
		}

		[Route("~/api/one")]
		[Authorize]
		[HttpGet]
		public async Task<ActionResult<UserDto>> Get(int id)
		{
			var query = new GetOneUserQuery()
			{
				Id = id
			};
			var vm = await Mediator.Send(query);
			return Ok(vm);
		}

		[Route("~/api/delete")]
		[Authorize]
		[HttpGet]
		public async Task<ActionResult> Delete(int id)
		{
			var query = new DaleteUserCommand()
			{
				Id = id
			};
			await Mediator.Send(query);
			return NoContent();
		}

		[Route("~/api/create")]
		[HttpPost]
		public async Task<ActionResult<int>> Create(string login, string password, int groupId)
		{
			var command = new CreateUserCommand()
			{
				Login = login,
				Password = password,
				GroupId = groupId
			};
			var userId = await Mediator.Send(command);
			return Ok(userId);
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using Users.Domain;
using Users.Application.Users.Queries.GetUserList;
using Users.Application.Users.Queries.GetOneUser;

namespace Users.WebApi.Controllers
{
	public class UserController : BaseController
	{
		[HttpGet]
		public async Task<ActionResult<IList<User>>> GetAll()
		{
			var query = new GetUserListQuery();
			var vm = await Mediator.Send(query);
			return Ok(vm);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<User>> Get(Guid id)
		{
			var query = new GetOneUserQuery()
			{
				Id = id
			};
			var vm = await Mediator.Send(query);
			return Ok(vm);
		}

		[HttpPost]
		public async Task<ActionResult<Guid>> Create([FromBody])
	}
}

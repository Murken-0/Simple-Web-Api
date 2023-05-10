using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Persistense;

namespace Users.Tests.Common
{
	public class TestCommandBase
	{
		protected readonly UsersDbContext Context;

		public TestCommandBase()
		{
			Context = UsersContextFactory.Create();
		}
		public void Dispose()
		{
			UsersContextFactory.Destroy(Context);
		}
	}
}

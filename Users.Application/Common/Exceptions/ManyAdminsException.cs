using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Common.Exceptions
{
	public class ManyAdminsException : Exception
	{
		public ManyAdminsException() 
			: base("More then one admin in the system.") { }
	}
}

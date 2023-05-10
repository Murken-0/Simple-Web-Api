namespace Users.Application.Common.Exceptions
{
	public class LoginAlreadyExistExeption : Exception
	{
		public LoginAlreadyExistExeption(string login) 
			: base($"User with login {login} already exist.") { }
	}
}

using System.Net;
using System.Text.Json;
using Users.Application.Common.Exceptions;

namespace Users.WebApi.Middleware
{
	public class CustomExceptionsHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public CustomExceptionsHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception e)
			{
				await HandleExceptionAsync(context, e);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception e)
		{
			var code = HttpStatusCode.InternalServerError;
			var result = string.Empty;
			switch (e)
			{
				case NotFoundException:
					code = HttpStatusCode.NotFound;
					break;
				case ManyAdminsException:
					code = HttpStatusCode.Conflict;
					break;
				case LoginAlreadyExistExeption:
					code = HttpStatusCode.Conflict;
					break;
			}
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)code;

			if (result == string.Empty)
				result = JsonSerializer.Serialize(new { err = e.Message });

			return context.Response.WriteAsync(result);
		}
	}
}

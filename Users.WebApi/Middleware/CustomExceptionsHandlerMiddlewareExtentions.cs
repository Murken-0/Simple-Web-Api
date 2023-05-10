using System.Runtime.CompilerServices;

namespace Users.WebApi.Middleware
{
	public static class CustomExceptionsHandlerMiddlewareExtentions
	{
		public static IApplicationBuilder UseCostomExceptionHandler(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<CustomExceptionsHandlerMiddleware>();
		}
	}
}

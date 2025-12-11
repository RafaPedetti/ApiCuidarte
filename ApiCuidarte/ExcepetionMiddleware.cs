using LogicaNegocio.Excepciones;

namespace ApiCuidarte
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (DomainException ex)
			{
				httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
				httpContext.Response.ContentType = "application/json";

				var result = new { message = ex.Message };
				await httpContext.Response.WriteAsync(
					System.Text.Json.JsonSerializer.Serialize(result)
				);
			}
			catch (Exception ex)
			{
				httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
				httpContext.Response.ContentType = "application/json";

				var result = new { message = "Error inesperado", detail = ex.Message };
				await httpContext.Response.WriteAsync(
					System.Text.Json.JsonSerializer.Serialize(result)
				);
			}
		}
	}
}

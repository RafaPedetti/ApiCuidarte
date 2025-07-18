namespace ApiCuidarte
{
	public class ApiKeyMiddleware
	{
		private readonly RequestDelegate _next;
		private const string APIKEYNAME = "X-API-KEY";

		public ApiKeyMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context, IConfiguration config)
		{
			var path = context.Request.Path.Value;

			// Ignorar Swagger y archivos estáticos
			if (path.StartsWith("/swagger") || path.StartsWith("/favicon") || path.Contains("swagger.json"))
			{
				await _next(context);
				return;
			}


			if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
			{
				context.Response.StatusCode = 401;
				await context.Response.WriteAsync("API Key no proporcionada");
				return;
			}

			var apiKey = config.GetValue<string>("ApiKey");

			if (!apiKey.Equals(extractedApiKey))
			{
				context.Response.StatusCode = 403;
				await context.Response.WriteAsync("API Key inválida");
				return;
			}

			await _next(context);
		}
	}
}

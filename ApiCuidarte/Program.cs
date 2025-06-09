using Infraestructura.LogicaAccesoDatos.EF;
using LogicaAplicacion.Dtos.Usuarios;
using LogicaAplicacion.TiposPlan;
using LogicaAplicacion.TiposPlanes;
using LogicaAplicacion.TiposServicio;
using LogicaAplicacion.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfacesServicios;
using LogicaNegocio.InterfazServicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
namespace ApiCuidarte
{
	public class Program
	{
		public static void Main(string[] args)
		{

			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();

			// En caso que de problemas al consumir el api con las
			// referencias ciclicas
			builder.Services.AddControllers().AddJsonOptions(
				option =>
				option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
				);

			// personalizar configuracion de swagger
			builder.Services.AddSwaggerGen(opt =>
			{
				opt.SwaggerDoc(
					"v1", new OpenApiInfo
					{
						Title = "ApiCuidarte",
						Version = "v1",
						Description = "RestApi",
					});

				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				if (File.Exists(xmlPath))
				{
					opt.IncludeXmlComments(xmlPath);
				}

				opt.IncludeXmlComments(xmlPath);

				opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Coloque el token JWT",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "bearer"
				});

				opt.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type=ReferenceType.SecurityScheme,
								Id="Bearer"
							}
						},
						new string[]{}
					}
				});
			});

			// configuracion de la autorizacion y el JWT
			// 
			var claveSecreta = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=";
			builder.Services.AddAuthentication(aut =>
			{
				aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(aut =>
			{
				aut.RequireHttpsMetadata = false;
				aut.SaveToken = true;
				aut.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(claveSecreta)),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});

			//Repositorios

			builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
			builder.Services.AddScoped<IRepositorioTipoServicio, RepositorioTipoServicio>();
			builder.Services.AddScoped<IRepositorioTipoPlan, RepositorioTipoPlan>();

			// caso de uso -- Usuario --
			builder.Services.AddScoped<IObtenerTodos<UsuarioDto>, GetAllUsuario>();
			builder.Services.AddScoped<IAlta<CrearUsuarioDto>, AltaUsuario>();
			builder.Services.AddScoped<IObtener<UsuarioDto>, GetByIdUsuario>();
			builder.Services.AddScoped<IEditar<EditarUsuarioDto>, EditarUsuario>();
			builder.Services.AddScoped<IEliminar<UsuarioDto>, EliminarUsuario>();
			builder.Services.AddScoped<ILogin<UsuarioDto>, LoginUsuario>();
			builder.Services.AddScoped<IObtenerPorTexto<UsuarioDto>, ObtenerPorTextoUsuario>();

			// caso de uso -- TipoServicio --
			builder.Services.AddScoped<IObtenerTodos<TipoServicio>, GetAllTipoServicio>();
			builder.Services.AddScoped<IAlta<TipoServicio>, AltaTipoServicio>();
			builder.Services.AddScoped<IObtener<TipoServicio>, GetByIdTipoServicio>();
			builder.Services.AddScoped<IEditar<TipoServicio>, EditarTipoServicio>();
			builder.Services.AddScoped<IEliminar<TipoServicio>, EliminarTipoServicio>();

			// caso de uso -- TipoPlan --
			builder.Services.AddScoped<IObtenerTodos<TipoPlan>, GetAllTipoPlan>();
			builder.Services.AddScoped<IAlta<TipoPlan>, AltaTipoPlan>();
			builder.Services.AddScoped<IObtener<TipoPlan>, GetByIdTipoPlan>();
			builder.Services.AddScoped<IEditar<TipoPlan>, EditarTipoPlan>();
			builder.Services.AddScoped<IEliminar<TipoPlan>, EliminarTipoPlan>();

			var config = new ConfigurationBuilder()
			.AddJsonFile("parametos.json", optional: true, reloadOnChange: true)
			.Build();
			Parametros.MaxItemsPaginado = config.GetValue<int>("MaxItemsPaginado");
			Parametros.TopeUnidades = config.GetValue<int>("TopeUnidades");

			var connectionString = builder.Configuration.GetConnectionString("cuidarte");
			Console.WriteLine($"Cadena de conexión: {connectionString}");

			builder.Services.AddDbContext<CuidarteContext>(
				options => options.UseNpgsql
				(builder.Configuration.GetConnectionString("cuidarte")));
			builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();

		}
	}
}
using Infraestructura.LogicaAccesoDatos.EF;
using LogicaAplicacion.Clientes;
using LogicaAplicacion.Dtos;
using LogicaAplicacion.Dtos.Clientes;
using LogicaAplicacion.Dtos.Empresas;
using LogicaAplicacion.Dtos.Suscripciones;
using LogicaAplicacion.Dtos.Tareas;
using LogicaAplicacion.Dtos.TipoPlanes;
using LogicaAplicacion.Dtos.Usuarios;
using LogicaAplicacion.Empresas;
using LogicaAplicacion.EmpresaS;
using LogicaAplicacion.Mensualidades;
using LogicaAplicacion.Tareas;
using LogicaAplicacion.TiposPlan;
using LogicaAplicacion.TiposPlanes;
using LogicaAplicacion.TiposServicio;
using LogicaAplicacion.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfacesServicios;
using LogicaNegocio.InterfacesServicios.Mensualidades;
using LogicaNegocio.InterfazServicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("PermitirFrontend", policy =>
				{
					policy.WithOrigins("http://localhost:3000")
						  .AllowAnyHeader()
						  .AllowAnyMethod();
				});
			});

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();

			builder.Services.AddControllers().AddJsonOptions(
				option =>
				option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
				);
			builder.Services.AddSwaggerGen(opt =>
			{
				opt.SwaggerDoc(
					"v1", new OpenApiInfo
					{
						Title = "ApiCuidarte",
						Version = "v1",
						Description = "RestApi",
					});

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
			builder.Services.AddScoped<IRepositorioCliente, RepositorioCliente>();
			builder.Services.AddScoped<IRepositorioTarea, RepositorioTarea>();
			builder.Services.AddScoped<IRepositorioEmpresa, RepositorioEmpresa>();
			builder.Services.AddScoped<IRepositorioSuscripcion, RepositorioSuscripcion>();
			builder.Services.AddScoped<IRepositorioMensualidad, RepositorioMensualidad>();
			// caso de uso -- Usuario --
			builder.Services.AddScoped<IObtenerTodos<UsuarioDto>, GetAllUsuario>();
			builder.Services.AddScoped<IAlta<UsuarioDto>, AltaUsuario>();
			builder.Services.AddScoped<IObtener<UsuarioDto>, GetByIdUsuario>();
			builder.Services.AddScoped<IEditar<UsuarioDto>, EditarUsuario>();
			builder.Services.AddScoped<IEliminar<UsuarioDto>, EliminarUsuario>();
			builder.Services.AddScoped<ILogin<UsuarioDto>, LoginUsuario>();
			builder.Services.AddScoped<IObtenerPorTexto<UsuarioDto>, ObtenerPorTextoUsuario>();
			builder.Services.AddScoped<IObtenerHorasDelMes, ObtenerHorasDelMes>();
			// caso de uso -- TipoServicio --
			builder.Services.AddScoped<IObtenerTodos<TipoServicio>, GetAllTipoServicio>();
			builder.Services.AddScoped<IAlta<TipoServicio>, AltaTipoServicio>();
			builder.Services.AddScoped<IObtener<TipoServicio>, GetByIdTipoServicio>();
			builder.Services.AddScoped<IEditar<TipoServicio>, EditarTipoServicio>();
			builder.Services.AddScoped<IEliminar<TipoServicio>, EliminarTipoServicio>();

			// caso de uso -- TipoPlan --
			builder.Services.AddScoped<IObtenerTodos<TipoPlanDto>, GetAllTipoPlan>();
			builder.Services.AddScoped<IAlta<TipoPlanDto>, AltaTipoPlan>();
			builder.Services.AddScoped<IObtener<TipoPlanDto>, GetByIdTipoPlan>();
			builder.Services.AddScoped<IEditar<TipoPlanDto>, EditarTipoPlan>();
			builder.Services.AddScoped<IEliminar<TipoPlanDto>, EliminarTipoPlan>();

			// caso de uso -- Clientes --
			builder.Services.AddScoped<IObtenerPaginado<PaginadoResultado<ClienteDto>>, GetAllCliente>();
			builder.Services.AddScoped<IAlta<ClienteDto>, AltaCliente>();
			builder.Services.AddScoped<IObtener<ClienteDto>, GetByIdCliente>();
			builder.Services.AddScoped<IEditar<ClienteDto>, EditarCliente>();
			builder.Services.AddScoped<IEliminar<ClienteDto>, EliminarCliente>();
			builder.Services.AddScoped<IObtenerPorTexto<ClienteDto>, GetByTextoCliente>();

			// caso de uso -- Tareas --
			builder.Services.AddScoped<IObtenerPaginado<PaginadoResultado<TareaDto>>, GetAllTarea>();
			builder.Services.AddScoped<IAlta<TareaDto>, AltaTarea>();
			builder.Services.AddScoped<IObtener<TareaDto>, GetByIdTarea>();
			builder.Services.AddScoped<IEditar<TareaDto>, EditarTarea>();
			builder.Services.AddScoped<IEliminar<TareaDto>, EliminarTarea>();
			builder.Services.AddScoped<IObtenerPorTexto<TareaDto>, GetByTextoTarea>();

			// caso de uso -- Empresa --
			builder.Services.AddScoped<IObtenerTodos<EmpresaDto>, GetAllEmpresa>();
			builder.Services.AddScoped<IAlta<EmpresaDto>, AltaEmpresa>();
			builder.Services.AddScoped<IEditar<EmpresaDto>, EditarEmpresa>();
			builder.Services.AddScoped<IEliminar<EmpresaDto>, EliminarEmpresa>();

			// caso de uso -- Mensualidad  --
			builder.Services.AddScoped<IObtenerPorCliente<MensualidadDto>, GetAllMensualidad>();
			builder.Services.AddScoped<IPagarMensualidades<SuscripcionDto>, PagarMensualidad>();

			var config = new ConfigurationBuilder()
			.AddJsonFile("parametos.json", optional: true, reloadOnChange: true)
			.Build();
			Parametros.MaxItemsPaginado = config.GetValue<int>("MaxItemsPaginado");
			Parametros.TopeUnidades = config.GetValue<int>("TopeUnidades");

			var connectionString = builder.Configuration.GetConnectionString("cuidarte");
			Console.WriteLine($"Cadena de conexi�n: {connectionString}");

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

			app.UseCors("PermitirFrontend");

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();

		}
	}
}
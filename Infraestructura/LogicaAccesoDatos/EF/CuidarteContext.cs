
namespace Infraestructura.LogicaAccesoDatos.EF
{
	using Infraestructura.LogicaAccesoDatos.EF.Config;
	using LogicaNegocio.Entidades;
	using Microsoft.EntityFrameworkCore;
	public class CuidarteContext : DbContext
	{

		public DbSet<Usuario> Usuarios { get; set; }

		public DbSet<Administrador> Administradores { get; set; }

		public DbSet<Funcionario> Funcionarios { get; set; }

		public DbSet<Cliente> Clientes { get; set; }

		public DbSet<TipoPlan> TiposPlanes { get; set; }

		public DbSet<TipoServicio> TipoServicios { get; set; }
		
		public DbSet<Servicio> Servicios { get; set; }

		public DbSet<Tarea> Tareas { get; set; }

		public DbSet<Empresa> Empresas { get; set; }
		public DbSet<Mensualidad> Mensualidades { get; set; }
		public DbSet<Suscripcion> Suscripciones { get; set; }


		public CuidarteContext(DbContextOptions<CuidarteContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new UsuarioConfiguration());

			modelBuilder.ApplyConfiguration(new ClienteConfiguration());

			modelBuilder.ApplyConfiguration(new SuscripcionConfiguration());

			modelBuilder.ApplyConfiguration(new MensualidadConfiguration());
			modelBuilder.ApplyConfiguration(new EmpresaConfiguration());
		}
	}
}

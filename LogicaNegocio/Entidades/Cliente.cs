using LogicaNegocio.Excepciones.Cliente;
using LogicaNegocio.IntefacesDominio;
using LogicaNegocio.ValueObject;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace LogicaNegocio.Entidades
{
	public class Cliente : IEntity
	{
		public int Id { get; set; }

		[Required]
		public NombreCompleto NombreCompleto { get; set; }

		public string CI { get; set; } 
		[Required]
		public DateOnly FechaNacimiento { get; set; }
		[Required]
		public string Direccion { get; set; }
		[Required]
		public Telefono Telefono { get; set; }

		[Required]
		public Email Email { get; set; }

		public int TipoPlanId { get; set; }

		public TipoPlan Plan { get; set; }

		public List<Servicio> ServiciosDisponibles { get; set; } = new();
		public List<Servicio> ServiciosExtras { get; set; } = new();

		public Suscripcion? Suscripcion { get; set; }
		public int? SuscripcionId { get; set; }
		public bool Eliminado { get; set; }

		public Cliente(int id, NombreCompleto nombreCompleto, string ci,DateOnly fechaNacimiento, string direccion, Telefono telefono, int plan,Email email,Suscripcion suscripcion)
		{
			Id = id;
			NombreCompleto = nombreCompleto;
			FechaNacimiento = fechaNacimiento;
			Direccion = direccion;
			Telefono = telefono;
			TipoPlanId = plan;
			CI = ci;
			Email = email;
			Suscripcion = suscripcion;
			//ValidarCedula(CI);
		}

		public Cliente(int id, NombreCompleto nombreCompleto, string ci, DateOnly fechaNacimiento, string direccion, Telefono telefono, int plan, Email email)
		{
			Id = id;
			NombreCompleto = nombreCompleto;
			FechaNacimiento = fechaNacimiento;
			Direccion = direccion;
			Telefono = telefono;
			TipoPlanId = plan;
			CI = ci;
			Email = email;
			ValidarCedula(CI);
		}


		public Cliente(int id, NombreCompleto nombreCompleto, string ci, DateOnly fechaNacimiento, string direccion, Telefono telefono,Email email)
		{
			Id = id;
			NombreCompleto = nombreCompleto;
			FechaNacimiento = fechaNacimiento;
			Direccion = direccion;
			Telefono = telefono;
			CI = ci;
			Email= email;
			ValidarCedula(CI);
		}
		public Cliente() { }

		public void Update(Cliente c)
		{
			if( c.NombreCompleto != null) this.NombreCompleto =c.NombreCompleto;
			if (c.FechaNacimiento != null) this.FechaNacimiento = (DateOnly)c.FechaNacimiento;
			if (c.Direccion != null) this.Direccion = c.Direccion;
			if(c.Telefono != null) this.Telefono = c.Telefono;
		}

		public void CambiarPlan(TipoPlan tipoPlan)
		{
			this.Plan = tipoPlan;
			foreach (var servicio in tipoPlan.Servicios)
			{
				this.ServiciosDisponibles.Add(new Servicio
				{
					tipoServicio = servicio.tipoServicio,
					cantServicios = servicio.cantServicios,
				});
			}

		}

		public static void ValidarCedula(string cedula)
		{
			// Eliminar puntos y guiones
			cedula = cedula.Replace(".", "").Replace("-", "").Trim();

			// Asegurar que tenga 8 dígitos
			if (!Regex.IsMatch(cedula, @"^\d{7,8}$"))
				throw new ClienteException("cedula invalida");

			// Si tiene 7 dígitos, agregar un 0 al inicio
			if (cedula.Length == 7)
				cedula = "0" + cedula;

			int[] coeficientes = { 2, 9, 8, 7, 6, 3, 4 };
			int suma = 0;

			for (int i = 0; i < 7; i++)
			{
				suma += int.Parse(cedula[i].ToString()) * coeficientes[i];
			}

			int digitoVerificadorCalculado = (10 - (suma % 10)) % 10;
			int digitoVerificadorReal = int.Parse(cedula[7].ToString());

			if(digitoVerificadorCalculado != digitoVerificadorReal) throw new ClienteException("cedula invalida");
		}

		public void ResetearServicios()
		{
			this.ServiciosDisponibles = this.Plan.Servicios;
		}
	}
}

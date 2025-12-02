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
		public Telefono Celular { get; set; }

		[Required]
		public Email Email { get; set; }

		[Required]
		public string ResponsablePago { get; set; }

		[Required]
		public string FormaPago { get; set; }

		[Required]
		public string Observaciones { get; set; }
		public int TipoPlanId { get; set; }

		public TipoPlan Plan { get; set; }

		public List<Servicio> ServiciosDisponibles { get; set; } = new();
		public List<Servicio> ServiciosExtras { get; set; } = new();

		public Suscripcion? Suscripcion { get; set; }
		public int? SuscripcionId { get; set; }
		public bool Eliminado { get; set; }

		public Cliente(int id, NombreCompleto nombreCompleto, string ci,DateOnly fechaNacimiento, string direccion, Telefono telefono,Telefono celular, int plan,Email email,string ResponsablePago,string FormaPago,string Observaciones ,Suscripcion suscripcion)
		{
			Id = id;
			NombreCompleto = nombreCompleto;
			FechaNacimiento = fechaNacimiento;
			Direccion = direccion;
			Telefono = telefono;
			Celular = celular;
			TipoPlanId = plan;
			CI = ci;
			Email = email;
			Suscripcion = suscripcion;
			this.ResponsablePago= ResponsablePago;
			this.FormaPago= FormaPago;
			this.Observaciones= Observaciones;
			ValidarCedula(CI);
		}

		public Cliente(int id, NombreCompleto nombreCompleto, string ci, DateOnly fechaNacimiento, string direccion, Telefono telefono,Telefono celular, int plan, Email email, string ResponsablePago, string FormaPago, string Observaciones)
		{
			Id = id;
			NombreCompleto = nombreCompleto;
			FechaNacimiento = fechaNacimiento;
			Direccion = direccion;
			Telefono = telefono;
			Celular = celular;
			TipoPlanId = plan;
			CI = ci;
			Email = email;
			this.ResponsablePago = ResponsablePago;
			this.FormaPago = FormaPago;
			this.Observaciones = Observaciones;
			ValidarCedula(CI);
		}


		public Cliente(int id, NombreCompleto nombreCompleto, string ci, DateOnly fechaNacimiento, string direccion, Telefono telefono, Telefono celular,Email email, string ResponsablePago, string FormaPago, string Observaciones)
		{
			Id = id;
			NombreCompleto = nombreCompleto;
			FechaNacimiento = fechaNacimiento;
			Direccion = direccion;
			Telefono = telefono;
			Celular = celular;
			CI = ci;
			Email= email;
			this.ResponsablePago = ResponsablePago;
			this.FormaPago = FormaPago;
			this.Observaciones = Observaciones;
			ValidarCedula(CI);
		}
		public Cliente() { }

		public void Update(Cliente c)
		{
			if(c.CI != null)
			{
				ValidarCedula(c.CI);
				this.CI = c.CI;
			}
			if( c.NombreCompleto != null) this.NombreCompleto =c.NombreCompleto;
			if (c.FechaNacimiento != null) this.FechaNacimiento = (DateOnly)c.FechaNacimiento;
			if (c.Direccion != null) this.Direccion = c.Direccion;
			if(c.Telefono != null) this.Telefono = c.Telefono;
			if(c.Celular != null) this.Celular = c.Celular;
			if ( c.Email != null) this.Email = c.Email;
			if (c.ResponsablePago != null) this.ResponsablePago = c.ResponsablePago;
			if (c.FormaPago != null) this.FormaPago = c.FormaPago;
			if (c.Observaciones != null) this.Observaciones = c.Observaciones;
		}

		public void CambiarPlan(TipoPlan tipoPlan)
		{
			this.Plan = tipoPlan;
			this.ServiciosDisponibles.Clear();
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

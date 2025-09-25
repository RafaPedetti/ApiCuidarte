using LogicaNegocio.IntefacesDominio;
using LogicaNegocio.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
	public class Empresa : IEntity
	{
		public int Id { get; set; }
		public string Nombre { get; set; }

		public Telefono TelefonoContacto { get; set; }
		public int TipoPlanId { get; set; }
		public TipoPlan? Plan { get; set; }
		public int? SuscripcionId { get; set; }
		public Suscripcion? Suscripcion { get; set; }
		public bool Eliminado { get; set; }

		public Empresa() { }

		public Empresa(int id, string nombre, TipoPlan plan, string telefonoContacto)
		{
			Id = id;
			Nombre = nombre;
			Plan = plan;
			this.Eliminado = false;
			this.TelefonoContacto = new Telefono(telefonoContacto);
		}
		public Empresa(int id, string nombre, int planid, string telefonoContacto)
		{
			Id = id;
			Nombre = nombre;
			TipoPlanId = planid;
			this.Eliminado = false;
			this.TelefonoContacto = new Telefono(telefonoContacto);
		}

		public Empresa(int id, string nombre, int planid, string telefonoContacto, Suscripcion suscripcion)
		{
			Id = id;
			Nombre = nombre;
			TipoPlanId = planid;
			this.Eliminado = false;
			this.TelefonoContacto = new Telefono(telefonoContacto);
			this.Suscripcion = suscripcion;
		}

		public void Update(Empresa empresa)
		{
			if (empresa == null) throw new ArgumentNullException(nameof(empresa), "El objeto no puede ser nulo.");
			if (string.IsNullOrWhiteSpace(empresa.Nombre)) throw new ArgumentException("El nombre de la empresa no puede estar vacío.", nameof(empresa.Nombre));
			if (empresa.Plan == null) throw new ArgumentException("La empresa debe tener al menos un plan asociado.", nameof(empresa.Plan));
			this.Nombre = empresa.Nombre;
			this.Plan = empresa.Plan;
			this.TelefonoContacto = empresa.TelefonoContacto;
		}
	}

}

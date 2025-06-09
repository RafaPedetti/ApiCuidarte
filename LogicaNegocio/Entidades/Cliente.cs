using LogicaNegocio.IntefacesDominio;
using LogicaNegocio.ValueObject;
using System.ComponentModel.DataAnnotations;
namespace LogicaNegocio.Entidades
{
	public class Cliente : IEntity
	{
		public int Id { get; set; }

		[Required]
		public NombreCompleto NombreCompleto { get; set; }
		[Required]
		public DateOnly FechaNacimiento { get; set; }
		[Required]
		public string Direccion { get; set; }
		[Required]
		public Telefono Telefono { get; set; }

		public TipoPlan Plan { get; set; }

		public Cliente(int id, NombreCompleto nombreCompleto, DateOnly fechaNacimiento, string direccion, Telefono telefono, TipoPlan plan)
		{
			Id = id;
			NombreCompleto = nombreCompleto;
			FechaNacimiento = fechaNacimiento;
			Direccion = direccion;
			Telefono = telefono;
			Plan = plan;
		}

		public void Update(string? nombre,string? apellido,DateOnly? fecha,string? direccion,Telefono? telefono)
		{
			this.NombreCompleto.Update(nombre,apellido);
			if (fecha != null) this.FechaNacimiento = (DateOnly)fecha;
			if (direccion != null) this.Direccion = direccion;
			if(telefono != null) this.Telefono = telefono;
		}

		public void CambiarPlan(TipoPlan tipoPlan)
		{
			this.Plan = tipoPlan;
		}

		public Cliente() { }
	}
}

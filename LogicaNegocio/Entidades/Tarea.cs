using LogicaNegocio.IntefacesDominio;
using LogicaNegocio.ValueObject.Tarea;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
	public class Tarea : IEntity, IValidable
	{
		public int Id {  get; set; }

		[Required]
		public Cliente Cliente { get; set; }

		[Required]
		public Usuario EmpleadoResponsable { get; set; }

		[Required]
		public DateTime fecha { get; set; }

		[Required]
		public EstadoTarea Estado {  get; set; }

		public Tarea(Cliente c, Usuario e, DateTime f) 
		{
			this.Cliente = c;
			this.EmpleadoResponsable = e;
			this.fecha = f;
			this.Estado = EstadoTarea.Pendiente;
		}	

		public Tarea() { }
		public void Validar()
		{
			throw new NotImplementedException();
		}
	}
}

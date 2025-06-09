using LogicaNegocio.IntefacesDominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
	public class TipoPlan : IEntity
	{
		public int Id { get; set; }

		[Required]
		public string Nombre { get; set; }

		[Required]
		public List<Servicio> Servicios{ get; set;}
	
		public bool Eliminado { get; set; }

		public void Update(TipoPlan obj)
		{
			this.Nombre = obj.Nombre;
			this.Servicios = obj.Servicios ?? new List<Servicio>();
		}
	}
}

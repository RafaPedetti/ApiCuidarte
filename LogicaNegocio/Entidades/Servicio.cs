using LogicaNegocio.IntefacesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
	public class Servicio : IEntity
	{
		public int Id { get; set; }
		public TipoServicio tipoServicio { get; set; }

		public int cantServicios { get; set; }


		public Servicio() { }

		public Servicio(int id, TipoServicio tipoServicio, int cantServicios)
		{
			this.Id = id;
			this.tipoServicio = tipoServicio;
			this.cantServicios = cantServicios;
		}
	}
}

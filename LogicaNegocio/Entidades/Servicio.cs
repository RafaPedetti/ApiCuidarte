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
		public TipoServicio TipoServicio { get; set; }

		public int cantServicios { get; set; }
	}
}

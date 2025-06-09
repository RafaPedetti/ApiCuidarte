using LogicaNegocio.IntefacesDominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
	public class TipoServicio : IEntity
	{
		public int Id {  get; set; }

		[Required]
		public string Nombre {  get; set; }
		[Required]
		public float PrecioHora { get; set; }

		public bool Eliminado { get; set; } = false;

		public TipoServicio(int id, string nombre, float precioHora)
		{
			Id = id;
			Nombre = nombre;
			PrecioHora = precioHora;
		}

		public void Update(TipoServicio obj)
		{
			Nombre = obj.Nombre;
			PrecioHora = obj.PrecioHora;

		}
	}
}

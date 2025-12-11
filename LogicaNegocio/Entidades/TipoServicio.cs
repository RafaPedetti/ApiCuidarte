using LogicaNegocio.Excepciones;
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
		public decimal PrecioHora { get; set; }

		public bool Eliminado { get; set; } = false;

		public TipoServicio(int id, string nombre, decimal precioHora)
		{
			Id = id;
			Nombre = nombre;
			PrecioHora = precioHora;
		}

		public void Update(TipoServicio obj)
		{
			if (PrecioHora <= 0) throw new DomainException("El precio debe ser mayor a 0");
			Nombre = obj.Nombre;
			PrecioHora = obj.PrecioHora;

		}

		public override bool Equals(object? obj)
		{
			return obj is TipoServicio servicio &&
				   Nombre == servicio.Nombre;
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}
	}
}

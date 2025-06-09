using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.TiposServicio
{
	public class AltaTipoServicio : IAlta<TipoServicio>
	{
		private readonly IRepositorioTipoServicio _context;
		public AltaTipoServicio(IRepositorioTipoServicio context)
		{
			_context = context;
		}

		public void Ejecutar(TipoServicio obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("El tipo de objeto esta vacio");
			}
			_context.Add(obj);
		}
	}
}

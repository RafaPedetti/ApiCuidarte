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
	public class GetByIdTipoServicio : IObtener<TipoServicio>
	{
		IRepositorioTipoServicio _context;

		public GetByIdTipoServicio(IRepositorioTipoServicio context)
		{
			_context = context;
		}

		public TipoServicio Ejecutar(int id)
		{
			TipoServicio tipoServicio = _context.GetById(id);
			if (tipoServicio == null)
			{
				throw new Exception("Tipo de servicio no encontrado.");
			}

			return tipoServicio;
		}
	}
}

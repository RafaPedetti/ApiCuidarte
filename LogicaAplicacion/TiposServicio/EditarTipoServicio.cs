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
	public class EditarTipoServicio : IEditar<TipoServicio>
	{
		private IRepositorioTipoServicio _context;

		public EditarTipoServicio(IRepositorioTipoServicio context)
		{
			_context = context;
		}

		public TipoServicio Ejecutar(TipoServicio obj)
		{
			return _context.Update(obj);
		}
	}
}

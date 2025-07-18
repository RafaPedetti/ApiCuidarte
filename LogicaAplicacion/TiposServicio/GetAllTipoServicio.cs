using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.TiposPlanes
{
	public class GetAllTipoServicio : IObtenerTodos<TipoServicio>
	{
		private IRepositorioTipoServicio _context;

		public GetAllTipoServicio(IRepositorioTipoServicio context)
		{
			_context = context;
		}

		public IEnumerable<TipoServicio> Ejecutar(int pagina)
		{
			return _context.GetAll(pagina);
		}
	}
}

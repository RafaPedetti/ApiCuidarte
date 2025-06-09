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

		public void Ejecutar(int id, TipoServicio obj)
		{
			TipoServicio tipoServicio = _context.GetById(id);
			if (tipoServicio == null)
			{
				throw new Exception("Tipo de servicio no encontrado.");
			}
			_context.Update(id, obj);
		}
	}
}

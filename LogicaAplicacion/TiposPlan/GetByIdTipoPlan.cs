using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.TiposPlan
{
	public class GetByIdTipoPlan : IObtener<TipoPlan>
	{
		private readonly IRepositorioTipoPlan _context;
		public GetByIdTipoPlan(IRepositorioTipoPlan repositorioTipoPlan)
		{
			_context = repositorioTipoPlan;
		}

		public TipoPlan Ejecutar(int id)
		{
			return _context.GetById(id);
		}

	}
}

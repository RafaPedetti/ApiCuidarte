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
	public class GetAllTipoPlan : IObtenerTodos<TipoPlan>
	{
		private readonly IRepositorioTipoPlan _context;
		public GetAllTipoPlan(IRepositorioTipoPlan context)
		{
			_context = context;
		}
		public IEnumerable<TipoPlan> Ejecutar()
		{
			return _context.GetAll();
		}

	}
}

using LogicaAplicacion.Dtos.MapeosDto;
using LogicaAplicacion.Dtos.TipoPlanes;
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
	public class GetByIdTipoPlan : IObtener<TipoPlanDto>
	{
		private readonly IRepositorioTipoPlan _context;
		public GetByIdTipoPlan(IRepositorioTipoPlan repositorioTipoPlan)
		{
			_context = repositorioTipoPlan;
		}

		public TipoPlanDto Ejecutar(int id)
		{
			TipoPlanDto tpDto = TipoPlanMapper.ToDto(_context.GetById(id));
			return tpDto;
		}

	}
}

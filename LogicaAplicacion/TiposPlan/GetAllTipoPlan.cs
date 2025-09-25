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
	public class GetAllTipoPlan : IObtenerTodos<TipoPlanDto>
	{
		private readonly IRepositorioTipoPlan _context;
		public GetAllTipoPlan(IRepositorioTipoPlan context)
		{
			_context = context;
		}
		public IEnumerable<TipoPlanDto> Ejecutar()
		{
			IEnumerable<TipoPlanDto> tipoPlanDtos = TipoPlanMapper.ToListaDto(_context.GetAll());
			return tipoPlanDtos;
		}

	}
}

using LogicaAplicacion.Dtos.MapeosDto;
using LogicaAplicacion.Dtos.Tareas;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfacesServicios;
using LogicaNegocio.InterfazServicios;
using System.Collections.Generic;

namespace LogicaAplicacion.Tareas
{
	public class GetByTextoTarea : IObtenerPorTexto<TareaDto>
	{
		private readonly IRepositorioTarea _context;
		public GetByTextoTarea(IRepositorioTarea context)
		{
			_context = context;
		}

		public IEnumerable<TareaDto> Ejecutar(string texto)
		{
			IEnumerable<TareaDto> tDto = TareaMapper.ToListaDto(_context.GetByTexto(texto));
			return tDto;
		}

	}
}

using LogicaAplicacion.Dtos;
using LogicaAplicacion.Dtos.MapeosDto;
using LogicaAplicacion.Dtos.Tareas;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfacesServicios;

namespace LogicaAplicacion.Tareas
{
	public class GetAllTarea : IObtenerPaginado<PaginadoResultado<TareaDto>>
	{
		private readonly IRepositorioTarea _context;
		public GetAllTarea(IRepositorioTarea context)
		{
			_context = context;
		}
		public PaginadoResultado<TareaDto> Ejecutar(int pagina, string? usuario)
		{
			var totalItems = _context.TotalItemsAsync();
			var tareas =  _context.GetAll(pagina,usuario);
			return new PaginadoResultado<TareaDto>(TareaMapper.ToListaDto(tareas), totalItems);

		}

	}
}

using LogicaAplicacion.Dtos;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfacesServicios;

namespace LogicaAplicacion.Tareas
{
	public class GetAllTarea : IObtenerPaginado<PaginadoResultado<Tarea>>
	{
		private readonly IRepositorioTarea _context;
		public GetAllTarea(IRepositorioTarea context)
		{
			_context = context;
		}
		public PaginadoResultado<Tarea> Ejecutar(int pagina)
		{

			var totalItems = _context.TotalItemsAsync();

			var tareas =  _context.GetAll(pagina);

			return new PaginadoResultado<Tarea>(tareas, totalItems);

		}

	}
}

using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfacesServicios;
using LogicaNegocio.InterfazServicios;

namespace LogicaAplicacion.Tareas
{
	public class GetByTextoTarea : IObtenerPorTexto<Tarea>
	{
		private readonly IRepositorioTarea _context;
		public GetByTextoTarea(IRepositorioTarea context)
		{
			_context = context;
		}

		public IEnumerable<Tarea> Ejecutar(string texto)
		{
			return _context.GetByTexto(texto);
		}

	}
}

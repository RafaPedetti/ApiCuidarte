using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;

namespace LogicaAplicacion.Tareas
{
	public class GetByIdTarea : IObtener<Tarea>
	{
		private readonly IRepositorioTarea _context;
		public GetByIdTarea(IRepositorioTarea context)
		{
			_context = context;
		}

		public Tarea Ejecutar(int id)
		{
			return _context.GetById(id);
		}

	}
}

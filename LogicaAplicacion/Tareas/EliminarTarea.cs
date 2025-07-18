using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;


namespace LogicaAplicacion.Tareas
{
	public class EliminarTarea : IEliminar<Tarea>
	{
		public readonly IRepositorioCliente _context;

		public EliminarTarea(IRepositorioCliente context)
		{
			_context = context;
		}

		public void Ejecutar(int id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("El tipo de objeto esta vacio");
			}
			_context.Delete(id);
		}
	}
}

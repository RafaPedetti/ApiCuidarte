using LogicaAplicacion.Dtos.Clientes;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
namespace LogicaAplicacion.Clientes
{
	public class EliminarCliente : IEliminar<ClienteDto>
	{
		public readonly IRepositorioCliente _context;

		public EliminarCliente(IRepositorioCliente context)
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

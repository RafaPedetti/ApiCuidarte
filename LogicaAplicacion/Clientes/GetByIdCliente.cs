using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;

namespace LogicaAplicacion.Clientes
{
	public class GetByIdCliente : IObtener<Cliente>
	{
		private readonly IRepositorioCliente _context;
		public GetByIdCliente(IRepositorioCliente repositorioCliente)
		{
			_context = repositorioCliente;
		}

		public Cliente Ejecutar(int id)
		{
			return _context.GetById(id);
		}

	}
}

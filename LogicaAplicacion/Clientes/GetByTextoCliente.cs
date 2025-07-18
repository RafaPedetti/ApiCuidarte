using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfacesServicios;
using LogicaNegocio.InterfazServicios;

namespace LogicaAplicacion.Clientes
{
	public class GetByTextoCliente : IObtenerPorTexto<Cliente>
	{
		private readonly IRepositorioCliente _context;
		public GetByTextoCliente(IRepositorioCliente repositorioCliente)
		{
			_context = repositorioCliente;
		}

		public IEnumerable<Cliente> Ejecutar(string texto)
		{
			return _context.GetByTexto(texto);
		}

	}
}

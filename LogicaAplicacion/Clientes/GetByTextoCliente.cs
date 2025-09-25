using LogicaAplicacion.Dtos.Clientes;
using LogicaAplicacion.Dtos.MapeosDto;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfacesServicios;

namespace LogicaAplicacion.Clientes
{
	public class GetByTextoCliente : IObtenerPorTexto<ClienteDto>
	{
		private readonly IRepositorioCliente _context;
		public GetByTextoCliente(IRepositorioCliente repositorioCliente)
		{
			_context = repositorioCliente;
		}

		public IEnumerable<ClienteDto> Ejecutar(string texto)
		{
			IEnumerable<ClienteDto> cDto = ClienteMapper.ToListaDto(_context.GetByTexto(texto));
			return cDto;
		}

	}
}

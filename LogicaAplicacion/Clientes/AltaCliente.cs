using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
using LogicaNegocio.Entidades;
using LogicaAplicacion.Dtos.MapeosDto;
using LogicaAplicacion.Dtos.Clientes;
namespace LogicaAplicacion.Clientes
{
	public class AltaCliente : IAlta<ClienteDto,Cliente>
	{
		private readonly IRepositorioCliente _context;
		public AltaCliente(IRepositorioCliente context)
		{
			_context = context;
		}
		public Cliente Ejecutar(ClienteDto obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("El tipo de objeto esta vacio");
			}

			Cliente c = ClienteMapper.FromDto(obj);
			return _context.Add(c);
		}
	}
}

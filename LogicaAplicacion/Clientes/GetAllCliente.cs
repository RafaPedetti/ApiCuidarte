using LogicaAplicacion.Dtos;
using LogicaAplicacion.Dtos.Clientes;
using LogicaAplicacion.Dtos.MapeosDto;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfacesServicios;
using LogicaNegocio.InterfazServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Clientes
{
	public class GetAllCliente : IObtenerPaginado<PaginadoResultado<ClienteDto>>
	{
		private readonly IRepositorioCliente _context;
		public GetAllCliente(IRepositorioCliente context)
		{
			_context = context;
		}
		public PaginadoResultado<ClienteDto> Ejecutar(int pagina)
		{
			var clientes = _context.GetAll(pagina);
			var totalClientes = _context.TotalItems();
			return new PaginadoResultado<ClienteDto>(ClienteMapper.ToListaDto(clientes), totalClientes);
		}

	}
}

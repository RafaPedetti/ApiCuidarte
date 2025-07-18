using LogicaAplicacion.Dtos;
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
	public class GetAllCliente : IObtenerPaginado<PaginadoResultado<Cliente>>
	{
		private readonly IRepositorioCliente _context;
		public GetAllCliente(IRepositorioCliente context)
		{
			_context = context;
		}
		public PaginadoResultado<Cliente> Ejecutar(int pagina)
		{
			var clientes = _context.GetAll(pagina);
			var totalClientes = _context.TotalItems();
			return new PaginadoResultado<Cliente>(clientes, totalClientes);
		}

	}
}

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

namespace LogicaAplicacion.Empresas
{
	public class GetAllEmpresa : IObtenerTodos<Empresa>
	{
		private readonly IRepositorioEmpresa _context;
		public GetAllEmpresa(IRepositorioEmpresa context)
		{
			_context = context;
		}
		public IEnumerable<Empresa> Ejecutar()
		{
			var empresas = _context.GetAll();
			return empresas;

		}

	}
}

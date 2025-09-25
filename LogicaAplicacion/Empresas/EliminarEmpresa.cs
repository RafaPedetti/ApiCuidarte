using LogicaAplicacion.Dtos.Empresas;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Empresas
{
	public class EliminarEmpresa : IEliminar<EmpresaDto>
	{
		public readonly IRepositorioEmpresa _context;

		public EliminarEmpresa(IRepositorioEmpresa context)
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

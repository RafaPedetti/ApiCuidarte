using LogicaAplicacion.Dtos.Empresas;
using LogicaAplicacion.Dtos.MapeosDto;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;

namespace LogicaAplicacion.Empresas
{
	public class GetAllEmpresa : IObtenerTodos<EmpresaDto>
	{
		private readonly IRepositorioEmpresa _context;
		public GetAllEmpresa(IRepositorioEmpresa context)
		{
			_context = context;
		}
		public IEnumerable<EmpresaDto> Ejecutar()
		{
			var empresas = EmpresaMapper.ToListaDto(_context.GetAll());
			return empresas;

		}

	}
}

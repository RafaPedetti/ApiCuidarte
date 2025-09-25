using LogicaAplicacion.Dtos.Empresas;
using LogicaAplicacion.Dtos.MapeosDto;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;

namespace LogicaAplicacion.Empresas
{
	public class EditarEmpresa : IEditar<EmpresaDto>
	{
		public readonly IRepositorioEmpresa _context;

		public EditarEmpresa(IRepositorioEmpresa context)
		{
			_context = context;
		}
		public EmpresaDto Ejecutar(EmpresaDto obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("El tipo de objeto esta vacio");
			}
			if (obj.id <= 0)
			{
				throw new ArgumentException("El Id del cliente debe ser mayor a 0");
			}
			Empresa empresa = EmpresaMapper.FromDto(obj);
			EmpresaDto empresaDto = EmpresaMapper.ToDto(_context.Update(empresa));
			return empresaDto;

		}
	}
}

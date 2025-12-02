using LogicaAplicacion.Dtos.Empresas;
using LogicaAplicacion.Dtos.MapeosDto;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
using LogicaNegocio.ValueObject.TipoPlan;

namespace LogicaAplicacion.Empresas
{
	public class EditarEmpresa : IEditar<EmpresaDto>
	{
		public readonly IRepositorioEmpresa _context;
		public readonly IRepositorioTipoPlan _contextTipoPlan;

		public EditarEmpresa(IRepositorioEmpresa context, IRepositorioTipoPlan contextTipoPlan)
		{
			_context = context;
			_contextTipoPlan = contextTipoPlan;
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
			TipoPlan tipoPlan = _contextTipoPlan.GetById(obj.TipoPlanId);
			if (tipoPlan == null)
			{
				throw new KeyNotFoundException("El Tipo de plan no existe");
			}
			if(tipoPlan.Destino != PlanDestino.Empresa)
			{
				throw new ArgumentException("El Tipo de plan no es valido para una empresa");
			}
			empresa.Plan = tipoPlan;
			EmpresaDto empresaDto = EmpresaMapper.ToDto(_context.Update(empresa));
			return empresaDto;

		}
	}
}

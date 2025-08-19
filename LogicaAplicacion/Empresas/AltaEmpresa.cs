using LogicaAplicacion.Dtos.Empresas;
using LogicaAplicacion.Dtos.MapeosDto;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;


namespace LogicaAplicacion.EmpresaS
{
	public class AltaEmpresa : IAlta<EmpresaDto, Empresa>
	{
		private readonly IRepositorioEmpresa _context;
		private readonly IRepositorioTipoPlan _contextTipoPlan;
		private readonly IRepositorioSuscripcion _contextSuscripcion;
		public AltaEmpresa(IRepositorioEmpresa context,IRepositorioTipoPlan contextTipoPlan,IRepositorioSuscripcion contextSuscripcion)
		{
			_context = context;
			_contextTipoPlan = contextTipoPlan;
			_contextSuscripcion = contextSuscripcion;
		}
		public Empresa Ejecutar(EmpresaDto obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("El tipo de objeto esta vacio");
			}
			Empresa empresa = EmpresaMapper.FromDto(obj);
			TipoPlan plan = _contextTipoPlan.GetById(obj.TipoPlanId);
			empresa.Plan = plan;
			Empresa eCreada = _context.Add(empresa);
			var suscripcion = new Suscripcion(eCreada, eCreada.Plan);
			_contextSuscripcion.Add(suscripcion);
			eCreada.SuscripcionId = suscripcion.Id;
			_context.Update(eCreada);
			return eCreada;
		}
	}
}

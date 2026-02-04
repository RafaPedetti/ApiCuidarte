using LogicaAplicacion.Dtos.Clientes;
using LogicaAplicacion.Dtos.MapeosDto;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
using LogicaNegocio.ValueObject.TipoPlan;
namespace LogicaAplicacion.Clientes
{
	public class AltaCliente : IAlta<ClienteDto>
	{
		private readonly IRepositorioCliente _context;

		private readonly IRepositorioTipoPlan _contextTipoPlan;

		private readonly IRepositorioSuscripcion _contextSuscripcion;
		public AltaCliente(IRepositorioCliente context, IRepositorioSuscripcion contextSuscripcion,IRepositorioTipoPlan contextTipoPlan)
		{
			_context = context;
			_contextSuscripcion = contextSuscripcion;
			_contextTipoPlan = contextTipoPlan;
		}
		public ClienteDto Ejecutar(ClienteDto obj)
		{
			if (obj == null)
				throw new ArgumentNullException("El tipo de objeto está vacío");
			Cliente c = ClienteMapper.FromDto(obj);
			TipoPlan plan = _contextTipoPlan.GetById(obj.TipoPlanId);
			Cliente cCreado = _context.Add(c);
			_context.CambiarPlan(cCreado.Id, plan);
			var suscripcion = new Suscripcion(null,cCreado, cCreado.Plan);
			_contextSuscripcion.Add(suscripcion);
			cCreado.SuscripcionId = suscripcion.Id;
			ClienteDto cDto = ClienteMapper.ToDto(_context.Update(cCreado));

			return cDto;
		}

	}
}

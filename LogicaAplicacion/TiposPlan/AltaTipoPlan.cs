using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
using LogicaNegocio.Entidades;
using LogicaAplicacion.Dtos.TipoPlanes;
using LogicaAplicacion.Dtos.MapeosDto;
namespace LogicaAplicacion.TiposPlan
{
	public class AltaTipoPlan : IAlta<TipoPlanDto>
	{
		private readonly IRepositorioTipoPlan _context;
		private readonly IRepositorioTipoServicio _contextTS;
		public AltaTipoPlan(IRepositorioTipoPlan context,IRepositorioTipoServicio contextTs)
		{
			_context = context;
			_contextTS = contextTs;
		}
		public TipoPlanDto Ejecutar(TipoPlanDto obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("El tipo de objeto esta vacio");
			}
			TipoPlan tp = TipoPlanMapper.FromDto(obj);
			foreach (ServicioDto s in obj.servicios)
			{
				TipoServicio ts = _contextTS.GetById(s.tipoServicio.id);
				Servicio servicio = new Servicio(0,ts,s.cantServicios);
				tp.AddServicio(servicio);
			}
			TipoPlanDto tDto = TipoPlanMapper.ToDto(_context.Add(tp));
			return tDto;
		}
	}
}

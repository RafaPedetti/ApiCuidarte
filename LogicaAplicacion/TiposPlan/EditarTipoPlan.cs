using LogicaAplicacion.Dtos.MapeosDto;
using LogicaAplicacion.Dtos.TipoPlanes;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.TiposPlan
{
	public class EditarTipoPlan : IEditar<TipoPlanDto,TipoPlan>
	{
		public readonly IRepositorioTipoPlan _context;
		public readonly IRepositorioTipoServicio _contextTS;

		public EditarTipoPlan(IRepositorioTipoPlan context, IRepositorioTipoServicio contextTS)
		{
			_context = context;
			_contextTS = contextTS;
		}
		public TipoPlan Ejecutar(TipoPlanDto obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("El tipo de objeto esta vacio");
			}
			if (obj.id <= 0)
			{
				throw new ArgumentException("El Id del tipo de plan debe ser mayor a 0");
			}
			TipoPlan tp = TipoPlanMapper.FromDto(obj);
			foreach(ServicioDto s in obj.servicios)
			{
				TipoServicio ts = _contextTS.GetById(s.tipoServicio.id);
				tp.AddServicio(new Servicio(s.id, ts, s.cantServicios));
			}
			return _context.Update(tp);

		}
	}
}

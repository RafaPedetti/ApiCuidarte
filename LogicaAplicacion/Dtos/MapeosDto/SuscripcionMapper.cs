using LogicaAplicacion.Dtos.Suscripciones;
using LogicaAplicacion.Dtos.TipoPlanes;
using LogicaNegocio.Entidades;
namespace LogicaAplicacion.Dtos.MapeosDto
{
	public class SuscripcionMapper
	{
		public static Suscripcion FromDto(SuscripcionDto sDto)
		{
			Suscripcion suscripcion = new Suscripcion(sDto.id, sDto.clienteId, sDto.responsableId, sDto.tipoPlanId, sDto.fechaInicio, (DateOnly)sDto.fechaFin);
			return suscripcion;
		}

		public static SuscripcionDto ToDto(Suscripcion s)
		{

			SuscripcionDto sDto	= new SuscripcionDto(s.Id,s.ClienteId ?? 0, s.EmpresaId ?? 0,
				s.PlanId,
				s.FechaInicio,
				s.ProximoCobro
			);
			return sDto;
		}

		public static IEnumerable<SuscripcionDto> ToListaDto(IEnumerable<Suscripcion> suscripciones)
		{
			List<SuscripcionDto> aux = new List<SuscripcionDto>();
			foreach (var suscripcion in suscripciones)
			{
				SuscripcionDto suscripcionDto = SuscripcionMapper.ToDto(suscripcion);
				aux.Add(suscripcionDto);
			}
			return aux;
		}
	}
}

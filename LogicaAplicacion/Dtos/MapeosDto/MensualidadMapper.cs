using LogicaAplicacion.Dtos.Suscripciones;
using LogicaNegocio.Entidades;
namespace LogicaAplicacion.Dtos.MapeosDto
{
	public class MensualidadMapper
	{
		public static Mensualidad FromDto(MensualidadDto mDto)
		{
			Mensualidad mensualidad;
			if (mDto.suscripcion != null) mensualidad = new Mensualidad(mDto.id, mDto.suscripcionId, SuscripcionMapper.FromDto(mDto.suscripcion), mDto.periodoDesde, mDto.periodoHasta,mDto.estado, mDto.precio, mDto.precioProximaMensualidad);
			else mensualidad = new Mensualidad(mDto.id, mDto.suscripcionId, null, mDto.periodoDesde, mDto.periodoHasta, mDto.estado, mDto.precio,mDto.precioProximaMensualidad);
			return mensualidad;
		}


		public static MensualidadDto ToDto(Mensualidad mensualidad)
		{
			MensualidadDto mensualidadDto = new MensualidadDto(mensualidad.Id, mensualidad.SubscriptionId, SuscripcionMapper.ToDto(mensualidad.Subscription) ?? null, mensualidad.PeriodoDesde, mensualidad.PeriodoHasta,mensualidad.Estado,mensualidad.Precio,null, mensualidad.PrecioProximaMensualidad);
			return mensualidadDto;
		}

		public static IEnumerable<MensualidadDto> ToListaDto(IEnumerable<Mensualidad> mensualidades)
		{
			List<MensualidadDto> aux = new List<MensualidadDto>();
			foreach (var mensualidad in mensualidades)
			{
				MensualidadDto mensualidadDto = MensualidadMapper.ToDto(mensualidad);
				aux.Add(mensualidadDto);
			}
			return aux;
		}
	}
}

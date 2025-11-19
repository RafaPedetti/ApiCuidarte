using LogicaAplicacion.Dtos.Empresas;
using LogicaAplicacion.Dtos.Tareas;
using LogicaNegocio.Entidades;
using LogicaNegocio.ValueObject.Tarea;

namespace LogicaAplicacion.Dtos.MapeosDto
{
	public class CalificacionMapper
	{
		public static Calificacion FromDto(CalificacionDto cDto)
		{
			Calificacion calificacion = new Calificacion(cDto.nota,cDto.comentario,cDto.idTarea);
			return calificacion;
		}

		public static CalificacionDto ToDto(Calificacion calificacion)
		{
			var calificacionDto = new CalificacionDto(
				calificacion.Nota,
				calificacion.Comentario,
				calificacion.TareaId ?? 0
			);
			return calificacionDto;
		}

		public static IEnumerable<CalificacionDto> ToListaDto(IEnumerable<Calificacion> calificaciones)
		{
			List<CalificacionDto> aux = new List<CalificacionDto>();
			foreach (var calificacion in calificaciones)
			{
				CalificacionDto calificacionDto = CalificacionMapper.ToDto(calificacion);
				aux.Add(calificacionDto);
			}
			return aux;
		}
	}
}

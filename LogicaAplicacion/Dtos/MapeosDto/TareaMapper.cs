using LogicaAplicacion.Dtos.Tareas;
using LogicaNegocio.Entidades;


namespace LogicaAplicacion.Dtos.MapeosDto
{
	public class TareaMapper
	{
		public static Tarea FromDto(TareaDto tDto)
		{
			var tarea = new Tarea(tDto.id,tDto.fecha,tDto.descripcion,tDto.estado);
			return tarea;
		}

	}
}

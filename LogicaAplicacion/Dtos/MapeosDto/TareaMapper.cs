using LogicaAplicacion.Dtos.Clientes;
using LogicaAplicacion.Dtos.Tareas;
using LogicaAplicacion.Dtos.TipoPlanes;
using LogicaAplicacion.Dtos.Usuarios;
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
		public static TareaDto ToDto(Tarea tarea)
		{
			
			UsuarioDto responsable = UsuarioMapper.ToDto(tarea.EmpleadoResponsable);
			ClienteDto cliente = ClienteMapper.ToDto(tarea.Cliente);
			var tareaDto = new TareaDto(tarea.Id, tarea.Cliente.Id,cliente, tarea.EmpleadoResponsable.Id, responsable, tarea.fecha, tarea.Estado, tarea.Descripcion, ServicioMapper.ToListaDto(tarea.serviciosUsados).ToList(), ServicioMapper.ToListaDto(tarea.ServiciosExtras).ToList(),tarea.CostoTotal);
			return tareaDto;
		}

		public static IEnumerable<TareaDto> ToListaDto(IEnumerable<Tarea> tareas)
		{
			List<TareaDto> aux = new List<TareaDto>();
			foreach (var tarea in tareas)
			{
				TareaDto tareaDto = TareaMapper.ToDto(tarea);
				aux.Add(tareaDto);
			}
			return aux;
		}

	}
}

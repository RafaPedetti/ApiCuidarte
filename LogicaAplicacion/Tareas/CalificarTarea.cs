using LogicaAplicacion.Dtos.MapeosDto;
using LogicaAplicacion.Dtos.Tareas;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfacesServicios;
using LogicaNegocio.ValueObject.Tarea;

namespace LogicaAplicacion.Tareas
{
	public class CalificarTarea : ICalificar<CalificacionDto>
	{
		private readonly IRepositorioTarea _repositorioTareas;

		public CalificarTarea(IRepositorioTarea repositorioTareas)
		{
			_repositorioTareas = repositorioTareas;
		}
		public CalificacionDto Ejecutar(CalificacionDto calificacionDto)
		{
			Calificacion calificacion = CalificacionMapper.FromDto(calificacionDto);
			Tarea tarea = _repositorioTareas.GetById(calificacionDto.idTarea);
			tarea.Calificar(calificacion);
			Tarea uTarea = _repositorioTareas.Update(tarea);
			return calificacionDto;
		}
	}
}

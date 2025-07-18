using LogicaAplicacion.Dtos.TipoPlanes;
using LogicaNegocio.ValueObject.Tarea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.Tareas
{
	public record TareaDto(int id,int clienteId,int responsableId,DateTime fecha,EstadoTarea estado,string descripcion,List<ServicioDto>? servicios)
	{
	}
}

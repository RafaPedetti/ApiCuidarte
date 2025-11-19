using LogicaAplicacion.Dtos.Clientes;
using LogicaAplicacion.Dtos.TipoPlanes;
using LogicaAplicacion.Dtos.Usuarios;
using LogicaNegocio.ValueObject.Tarea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.Tareas
{
	public record TareaDto(int id,int clienteId, ClienteDto? cliente,int responsableId, UsuarioDto? responsable, DateTime fecha,EstadoTarea estado,string descripcion,List<ServicioDto>? servicios,List<ServicioDto>? serviciosExtra,decimal costo,CalificacionDto? calificacion)
	{
	}
}

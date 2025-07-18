using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LogicaNegocio.ValueObject.Tarea
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum EstadoTarea
	{
		Pendiente,Activo,Finalizado

	}
}

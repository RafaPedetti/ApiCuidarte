using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.Suscripciones
{
	public record SuscripcionDto(int id, int? clienteId, int? responsableId,int tipoPlanId, DateOnly fechaInicio, DateOnly? fechaFin,decimal monto)
	{
	}
}

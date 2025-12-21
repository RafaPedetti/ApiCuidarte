using LogicaNegocio.Entidades;
using LogicaNegocio.ValueObject.Suscripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.Suscripciones
{
	public record MensualidadDto(int id, int suscripcionId,SuscripcionDto? suscripcion,DateOnly periodoDesde, DateOnly periodoHasta,MensualidadEstado estado,decimal precio,int? clienteId)
	{
	}
}
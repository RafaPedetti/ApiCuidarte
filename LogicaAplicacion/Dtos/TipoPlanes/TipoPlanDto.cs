using LogicaNegocio.Entidades;
using LogicaNegocio.ValueObject.TipoPlan;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.TipoPlanes
{
	public record TipoPlanDto(int id,string nombre,List<ServicioDto> servicios,int? empresaId,decimal precio,PlanDestino destino)
	{
	}
}

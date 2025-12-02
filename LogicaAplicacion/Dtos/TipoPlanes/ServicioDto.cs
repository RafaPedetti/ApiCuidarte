using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.TipoPlanes
{
	public record ServicioDto(int id, TiposServicioDto tipoServicio,int cantServicios)
	{
	}

	public record TiposServicioDto (int id, string nombre, decimal precio) { }
}

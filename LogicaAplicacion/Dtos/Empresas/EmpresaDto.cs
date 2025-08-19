using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.Empresas
{
	public record EmpresaDto(int id, string nombre, string telefonoContacto, int TipoPlanId)
	{
	}
}

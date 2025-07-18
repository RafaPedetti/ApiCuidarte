using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.Usuarios
{
	public record EditarUsuarioDto(int id,string email, string nombre, string apellido, string password)
	{
	}
}

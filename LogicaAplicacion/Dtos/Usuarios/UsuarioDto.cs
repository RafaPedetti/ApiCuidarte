using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.Usuarios
{
	public record UsuarioDto(int Id, string Email, string Nombre, string Apellido, string password, string Discriminador,bool Eliminado)
	{
	}
}

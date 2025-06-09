using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.DtosAuxs.Usuarios
{
	public record UsuarioLoginDto(string Email, string Password)
	{
	}
}

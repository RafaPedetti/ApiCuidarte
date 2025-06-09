using LogicaAplicacion.Dtos.MapeosDto;
using LogicaAplicacion.Dtos.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones.Usuario;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Usuarios
{
	public class LoginUsuario : ILogin<UsuarioDto>
	{
		IRepositorioUsuario _repoUsuario;

		public LoginUsuario(IRepositorioUsuario repoUsuario)
		{
			_repoUsuario = repoUsuario;
		}
		public UsuarioDto Ejecutar(string Email, string Password)
		{
			Usuario user = _repoUsuario.GetByLogin(Email, Password);
			if (user == null)
			{
				throw new UsuarioException("usuario no encontrado");
			}
			UsuarioDto usuarioDto = UsuarioMapper.ToDto(user);
			return usuarioDto;
		}
	}
}

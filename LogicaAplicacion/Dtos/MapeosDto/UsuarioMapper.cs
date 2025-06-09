using LogicaAplicacion.Dtos.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.ValueObject;
using LogicaNegocio.ValueObject.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.MapeosDto
{
    public class UsuarioMapper
    {
        public static Usuario FromDto(UsuarioDto usuarioDto)
        {
            if (usuarioDto.Discriminador.Equals(Administrador.RolValor))
            {
                return new Administrador(usuarioDto.Email, usuarioDto.Nombre, usuarioDto.Apellido, usuarioDto.password);
            }
            else
            {
                return new Funcionario(usuarioDto.Email, usuarioDto.Nombre, usuarioDto.Apellido, usuarioDto.password);
            }

        }
		public static Usuario FromDto(CrearUsuarioDto usuarioDto)
		{
			if (usuarioDto.Discriminador.Equals(Administrador.RolValor))
			{
				return new Administrador(usuarioDto.Email, usuarioDto.Nombre, usuarioDto.Apellido, usuarioDto.password);
			}
			else
			{
				return new Funcionario(usuarioDto.Email, usuarioDto.Nombre, usuarioDto.Apellido, usuarioDto.password);
			}

		}

		public static UsuarioDto ToDto(Usuario usuario)
        {
            string discriminador = "";
            if(usuario is Administrador)
            {
                discriminador = "Administrador";
            }
            if (usuario is Funcionario)
            {
                discriminador = "Funcionario";
            }
            return new UsuarioDto(usuario.Id,usuario.Email.Value,usuario.NombreCompleto.Nombre,usuario.NombreCompleto.Apellido,usuario.Password.Value,discriminador, usuario.Eliminado);
        }

        public static IEnumerable<UsuarioDto> ToListaDto(IEnumerable<Usuario> usuarios)
        {
            List<UsuarioDto> aux = new List<UsuarioDto>();
            foreach (var user in usuarios)
            {
                UsuarioDto userDto = UsuarioMapper.ToDto(user);
                aux.Add(userDto);
            }
            return aux;
        }
    }
}

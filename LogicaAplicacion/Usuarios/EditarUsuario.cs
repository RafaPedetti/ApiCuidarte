using LogicaAplicacion.Dtos.MapeosDto;
using LogicaAplicacion.Dtos.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Usuarios
{
	public class EditarUsuario : IEditar<UsuarioDto>
	{
		private readonly IRepositorioUsuario _repositorioUsuario;
		public EditarUsuario(IRepositorioUsuario repositorioUsuario)
		{
			_repositorioUsuario = repositorioUsuario;
		}

		public UsuarioDto Ejecutar(UsuarioDto obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(nameof(obj), "El usuario no puede ser nulo.");
			}
			Usuario usuarioExistente = _repositorioUsuario.GetById(obj.Id);
			if (usuarioExistente == null)
			{
				throw new KeyNotFoundException($"Usuario con ID {obj.Id} no encontrado.");
			}
			usuarioExistente.Update(obj.Email,obj.Nombre,obj.Apellido,obj.password);
			UsuarioDto uDto = UsuarioMapper.ToDto(_repositorioUsuario.Update(usuarioExistente));
			return uDto;
		}
	}
}

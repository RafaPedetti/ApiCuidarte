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
	public class EditarUsuario : IEditar<EditarUsuarioDto>
	{
		private readonly IRepositorioUsuario _repositorioUsuario;
		public EditarUsuario(IRepositorioUsuario repositorioUsuario)
		{
			_repositorioUsuario = repositorioUsuario;
		}

		public void Ejecutar(int id, EditarUsuarioDto obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(nameof(obj), "El usuario no puede ser nulo.");
			}
			Usuario usuarioExistente = _repositorioUsuario.GetById(id);
			if (usuarioExistente == null)
			{
				throw new KeyNotFoundException($"Usuario con ID {id} no encontrado.");
			}
			usuarioExistente.Update(obj.email,obj.nombre,obj.apellido,obj.pass);
			_repositorioUsuario.Update(id, usuarioExistente);
		}
	}
}

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
	public class AltaUsuario : IAlta<CrearUsuarioDto,Usuario>
	{
		IRepositorioUsuario _repositorioUsuario;

		public AltaUsuario(IRepositorioUsuario repositorioUsuario)
		{
			_repositorioUsuario = repositorioUsuario;
		}

		public Usuario Ejecutar(CrearUsuarioDto obj)
		{
			Usuario usuario = UsuarioMapper.FromDto(obj);
			return _repositorioUsuario.Add(usuario);
		}
	}
}

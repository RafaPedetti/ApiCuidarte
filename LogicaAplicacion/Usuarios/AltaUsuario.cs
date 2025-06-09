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
	public class AltaUsuario : IAlta<CrearUsuarioDto>
	{
		IRepositorioUsuario _repositorioUsuario;

		public AltaUsuario(IRepositorioUsuario repositorioUsuario)
		{
			_repositorioUsuario = repositorioUsuario;
		}

		public void Ejecutar(CrearUsuarioDto obj)
		{
			Usuario usuario = UsuarioMapper.FromDto(obj);
			_repositorioUsuario.Add(usuario);
		}
	}
}

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
	public class AltaUsuario : IAlta<UsuarioDto>
	{
		IRepositorioUsuario _repositorioUsuario;

		public AltaUsuario(IRepositorioUsuario repositorioUsuario)
		{
			_repositorioUsuario = repositorioUsuario;
		}

		public UsuarioDto Ejecutar(UsuarioDto obj)
		{
			Usuario usuario = UsuarioMapper.FromDto(obj);
			UsuarioDto uDto= UsuarioMapper.ToDto(_repositorioUsuario.Add(usuario));
			return uDto;
		}
	}
}

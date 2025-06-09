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
	public class GetByIdUsuario : IObtener<UsuarioDto>
	{
		private readonly IRepositorioUsuario _repositorioUsuario;
		public GetByIdUsuario(IRepositorioUsuario repositorioUsuario)
		{
			_repositorioUsuario = repositorioUsuario;
		}

		public UsuarioDto Ejecutar(int id)
		{
			Usuario usuario =  _repositorioUsuario.GetById(id);
			if (usuario == null)
			{
				throw new KeyNotFoundException($"Usuario con ID {id} no encontrado.");
			}
			return UsuarioMapper.ToDto(usuario);
		}

	}
}

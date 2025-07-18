using LogicaAplicacion.Dtos.MapeosDto;
using LogicaAplicacion.Dtos.Usuarios;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Usuarios
{
	public class GetAllUsuario : IObtenerTodos<UsuarioDto>
	{
		IRepositorioUsuario _repositorioUsuario;

		public GetAllUsuario(IRepositorioUsuario repositorioUsuario)
		{
			_repositorioUsuario = repositorioUsuario;
		}

		public IEnumerable<UsuarioDto> Ejecutar(int pagina)
		{
			IEnumerable<UsuarioDto> usuarios = UsuarioMapper.ToListaDto(_repositorioUsuario.GetAll(pagina));
			return usuarios;
		}
	}
}

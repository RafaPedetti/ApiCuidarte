using LogicaAplicacion.Dtos.MapeosDto;
using LogicaAplicacion.Dtos.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfacesServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Usuarios
{
	public class ObtenerPorTextoUsuario : IObtenerPorTexto<UsuarioDto>
	{
		IRepositorioUsuario _context;

		public ObtenerPorTextoUsuario(IRepositorioUsuario context)
		{
			_context = context;
		}

		public IEnumerable<UsuarioDto> Ejecutar(string texto, string? usuario)
		{
			IEnumerable<Usuario> usuarios = _context.GetByText(texto,usuario);
			if(usuarios.Count() == 0)
			{
				throw new KeyNotFoundException($"No se encontro ningun usuario");
			}
			return UsuarioMapper.ToListaDto(usuarios);
		}
	}
}

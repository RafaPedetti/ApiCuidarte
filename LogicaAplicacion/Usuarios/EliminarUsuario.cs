using LogicaAplicacion.Dtos.Usuarios;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;

namespace LogicaAplicacion.Usuarios
{
	public class EliminarUsuario : IEliminar<UsuarioDto>
	{
		private readonly IRepositorioUsuario _repositorioUsuario;
		public EliminarUsuario(IRepositorioUsuario repositorioUsuario)
		{
			_repositorioUsuario = repositorioUsuario;
		}
		public void Ejecutar(int id)
		{
			if (id <= 0)
			{
				throw new ArgumentException("El ID del usuario debe ser mayor que cero.", nameof(id));
			}
			_repositorioUsuario.Delete(id);
		}
	}
}

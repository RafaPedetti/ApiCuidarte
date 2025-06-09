using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
namespace LogicaAplicacion.TiposServicio
{
	public class EliminarTipoServicio : IEliminar<TipoServicio>
	{
		private IRepositorioTipoServicio _context;
		public EliminarTipoServicio(IRepositorioTipoServicio context)
		{
			_context = context;
		}
		public void Ejecutar(int id)
		{	if(id == null)
			{
				throw new ArgumentException("");
			}
			_context.Delete(id); 
		}
	}
}

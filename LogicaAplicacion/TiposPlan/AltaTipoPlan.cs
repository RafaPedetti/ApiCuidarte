using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
using LogicaNegocio.Entidades;
namespace LogicaAplicacion.TiposPlan
{
	public class AltaTipoPlan : IAlta<TipoPlan>
	{
		private readonly IRepositorioTipoPlan _context;
		public AltaTipoPlan(IRepositorioTipoPlan context)
		{
			_context = context;
		}
		public void Ejecutar(TipoPlan obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("El tipo de objeto esta vacio");
			}
			_context.Add(obj);
		}
	}
}

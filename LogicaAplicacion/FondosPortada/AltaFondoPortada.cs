using LogicaAplicacion.Dtos;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
namespace LogicaAplicacion.FondosPortada
{
	public class AltaFondoPortada :IAlta<FondoPortadaDto>
	{
		private readonly IRepositorioFondoPortada _context;
		public AltaFondoPortada(IRepositorioFondoPortada context)
		{
			_context = context;
		}
		public FondoPortadaDto Ejecutar(FondoPortadaDto obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("El tipo de objeto esta vacio");
			}

			FondoPortada fp = _context.Add(new FondoPortada(0, obj.url));
			return new FondoPortadaDto(fp.Url);
		}
	}
}

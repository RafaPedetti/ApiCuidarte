using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.TiposPlan
{
	public class EditarTipoPlan : IEditar<TipoPlan>
	{
		public readonly IRepositorioTipoPlan _context;

		public EditarTipoPlan(IRepositorioTipoPlan context)
		{
			_context = context;
		}
		public void Ejecutar(int id,TipoPlan obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("El tipo de objeto esta vacio");
			}
			if (id <= 0)
			{
				throw new ArgumentException("El Id del tipo de plan debe ser mayor a 0");
			}
			_context.Update(id,obj);
		}
	}
}

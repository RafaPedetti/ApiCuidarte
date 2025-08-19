using LogicaAplicacion.Dtos.Clientes;
using LogicaAplicacion.Dtos.MapeosDto;
using LogicaAplicacion.Dtos.TipoPlanes;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Clientes
{
	public class EditarCliente : IEditar<ClienteDto,Cliente>
	{
		public readonly IRepositorioCliente _context;
		public readonly IRepositorioTipoPlan _contextTP;
		public readonly IRepositorioSuscripcion _contextSuscripcion;

		public EditarCliente(IRepositorioCliente context, IRepositorioTipoPlan contextTP)
		{
			_context = context;
			_contextTP = contextTP;
		}
		public Cliente Ejecutar(ClienteDto obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("El tipo de objeto esta vacio");
			}
			if (obj.id <= 0)
			{
				throw new ArgumentException("El Id del cliente debe ser mayor a 0");
			}
			Cliente c = ClienteMapper.FromDto(obj);
			Suscripcion suscripcion = _contextSuscripcion.GetByIdCliente(c.Id);
			TipoPlan tp= _contextTP.GetById(obj.TipoPlanId);
			c.Plan = tp;
			suscripcion.Plan = tp;
			_contextSuscripcion.Update(suscripcion);
			return _context.Update(c);

		}
	}
}

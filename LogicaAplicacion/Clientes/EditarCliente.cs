using LogicaAplicacion.Dtos.Clientes;
using LogicaAplicacion.Dtos.MapeosDto;
using LogicaAplicacion.Dtos.TipoPlanes;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfazServicios;
using LogicaNegocio.ValueObject.TipoPlan;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Clientes
{
	public class EditarCliente : IEditar<ClienteDto>
	{
		public readonly IRepositorioCliente _context;
		public readonly IRepositorioTipoPlan _contextTP;
		public readonly IRepositorioSuscripcion _contextSuscripcion;

		public EditarCliente(IRepositorioCliente context, IRepositorioTipoPlan contextTP,IRepositorioSuscripcion contextS)
		{
			_context = context;
			_contextTP = contextTP;
			_contextSuscripcion = contextS;
		}
		public ClienteDto Ejecutar(ClienteDto obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("El tipo de objeto esta vacio");
			}
			if (obj.id <= 0)
			{
				throw new DomainException("El Id del cliente debe ser mayor a 0");
			}
			Cliente c = ClienteMapper.FromDto(obj);
			Suscripcion suscripcion = _contextSuscripcion.GetByIdCliente(c.Id);
			TipoPlan tp= _contextTP.GetById(obj.TipoPlanId);
			if (tp.Destino != PlanDestino.Cliente)
			{
				throw new ArgumentException("El Tipo de plan no es valido para un cliente");
			}
			c.Plan = tp;
			suscripcion.Plan = tp;
			_contextSuscripcion.Update(suscripcion);
			ClienteDto cDto = ClienteMapper.ToDto(_context.Update(c));
			return cDto;

		}
	}
}

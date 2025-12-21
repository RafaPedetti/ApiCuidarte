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
		public readonly IRepositorioEmpresa _contextEmpresa;

		public EditarCliente(IRepositorioCliente context, IRepositorioTipoPlan contextTP,IRepositorioSuscripcion contextS, IRepositorioEmpresa contextEmpresa)
		{
			_context = context;
			_contextTP = contextTP;
			_contextSuscripcion = contextS;
			_contextEmpresa = contextEmpresa;
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
			TipoPlan tp= _contextTP.GetById(obj.TipoPlanId);
			c.Plan = tp;
			Suscripcion suscripcion;
			if (tp.Destino.Equals(PlanDestino.Empresa))
			{
				Empresa e = _contextEmpresa.GetById((int)tp.EmpresaId);
				suscripcion = e.Suscripcion;
				suscripcion.Clientes.Add(c);

			}
			else
			{

				 suscripcion = _contextSuscripcion.GetByIdCliente(c.Id);
			}
			suscripcion.Plan = tp;
			_contextSuscripcion.Update(suscripcion);
			ClienteDto cDto = ClienteMapper.ToDto(_context.Update(c));
			return cDto;

		}
	}
}

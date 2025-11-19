using LogicaAplicacion.Dtos.Clientes;
using LogicaAplicacion.Dtos.TipoPlanes;
using LogicaAplicacion.Dtos.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.MapeosDto
{
	public class ClienteMapper
	{
		public static Cliente FromDto(ClienteDto cDto)
		{
			Cliente cliente;
			if (cDto.suscripcion != null)
			{
				cliente = new Cliente(cDto.id, new NombreCompleto(cDto.nombre, cDto.apellido), cDto.ci, DateOnly.FromDateTime(cDto.fechaNacimiento), cDto.direccion, new Telefono(cDto.telefono), new Telefono(cDto.celular),cDto.TipoPlanId, new Email(cDto.Email),cDto.responsablePago,cDto.formaPago,cDto.observaciones, SuscripcionMapper.FromDto(cDto.suscripcion));
			}
			else 
			{ 
				cliente = new Cliente(cDto.id, new NombreCompleto(cDto.nombre, cDto.apellido), cDto.ci, DateOnly.FromDateTime(cDto.fechaNacimiento), cDto.direccion, new Telefono(cDto.telefono), new Telefono(cDto.celular), cDto.TipoPlanId, new Email(cDto.Email), cDto.responsablePago, cDto.formaPago, cDto.observaciones);
			}

				return cliente;
		}

		public static ClienteDto ToDto(Cliente cliente)
		{
			ClienteDto clienteDto;
			if (cliente.Suscripcion != null)
			{
			 clienteDto = new ClienteDto(cliente.Id,cliente.NombreCompleto.Nombre, cliente.NombreCompleto.Apellido,cliente.CI, cliente.Email.Value, cliente.FechaNacimiento.ToDateTime(TimeOnly.MinValue), cliente.Direccion, cliente.Telefono.Value,cliente.Celular.Value ,cliente.ResponsablePago, cliente.FormaPago, cliente.Observaciones, cliente.TipoPlanId,SuscripcionMapper.ToDto(cliente.Suscripcion));
			}
			else
			{
				clienteDto = new ClienteDto(cliente.Id, cliente.NombreCompleto.Nombre, cliente.NombreCompleto.Apellido, cliente.CI, cliente.Email.Value, cliente.FechaNacimiento.ToDateTime(TimeOnly.MinValue), cliente.Direccion, cliente.Telefono.Value, cliente.Celular.Value, cliente.ResponsablePago, cliente.FormaPago, cliente.Observaciones, cliente.TipoPlanId,null);
			}
				return clienteDto;
		}

		public static IEnumerable<ClienteDto> ToListaDto(IEnumerable<Cliente> clientes)
		{
			List<ClienteDto> aux = new List<ClienteDto>();
			foreach (var cliente in clientes)
			{
				ClienteDto clienteDto = ClienteMapper.ToDto(cliente);
				aux.Add(clienteDto);
			}
			return aux;
		}
	}
}


using LogicaAplicacion.Dtos.Suscripciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.Clientes
{
	public record ClienteFormularioDto(int id, string nombre, string apellido, string ci, string email, DateTime fechaNacimiento, string direccion, string telefono, string celular,string responsablePago, string formaPago, string observaciones, ServicioFormularioDto[] servicios)
	{
	}
}

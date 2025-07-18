using LogicaNegocio.Entidades;
using LogicaNegocio.ValueObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.Clientes
{
	public record ClienteDto (int id,string nombre, string apellido,string ci,string Email, DateTime fechaNacimiento, string direccion, string telefono, int TipoPlanId)
	{

	}
}

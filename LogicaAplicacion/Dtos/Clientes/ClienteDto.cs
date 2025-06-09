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
	public record ClienteDto (string nombre, string apellido, DateOnly fechaNacimiento, string direccion, string telefono, int plan)
	{

	}
}

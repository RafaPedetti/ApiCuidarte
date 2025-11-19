using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LogicaNegocio.ValueObject.TipoPlan
{
	[Flags]
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum PlanDestino
	{
		Cliente = 0,
		Empresa = 1,
	}
}


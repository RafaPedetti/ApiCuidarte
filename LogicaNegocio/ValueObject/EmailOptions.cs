using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.ValueObject
{
	public class EmailOptions
	{
		public string FromName { get; set; } = string.Empty;
		public string FromAddress { get; set; } = string.Empty;
		public string ToAddress { get; set; } = string.Empty;
		public string Key { get; set; } = string.Empty;
	}

}

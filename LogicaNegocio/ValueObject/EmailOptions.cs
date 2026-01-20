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
		public string SmtpHost { get; set; } = string.Empty;
		public int SmtpPort { get; set; }
		public string Password { get; set; } = string.Empty;
	}

}

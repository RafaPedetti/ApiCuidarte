using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
	public class Administrador : Usuario
	{
		public static new string RolValor = "Administrador";

		public Administrador() { }

		public Administrador(string emai, string nom, string ape, string pass) : base(emai, nom, ape, pass)
		{
			
		}
	}
}

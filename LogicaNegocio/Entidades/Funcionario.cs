using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
	public class Funcionario : Usuario
	{
		public static new string RolValor = "Funcionario";
		public Funcionario() { }
		public Funcionario(string emai, string nom, string ape, string pass) : base(emai, nom, ape, pass)
		{
			RolValor = "Funcionario";
		}
	}
}

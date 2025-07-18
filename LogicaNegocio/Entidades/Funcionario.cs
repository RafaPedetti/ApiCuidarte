using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
	public class Funcionario : Usuario
	{

		public override string Discriminador { get; set; } = "Funcionario";

		public static readonly string DiscriminadorStatic = "Funcionario";


		public Funcionario() { }
		public Funcionario(string emai, string nom, string ape, string pass) : base(emai, nom, ape, pass)
		{
		}
	}
}

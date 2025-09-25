using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using LogicaNegocio.InterfacesServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Usuarios
{
	public class ObtenerHorasDelMes : IObtenerHorasDelMes
	{
		public IRepositorioTarea _context;

		public ObtenerHorasDelMes(IRepositorioTarea context)
		{
			_context = context;
		}
		public IEnumerable<int> Ejecutar(int idFuncionario, int mes, int anio)
		{
			if (idFuncionario <= 0)
				throw new ArgumentException("El id del funcionario es inválido");
			if (mes < 1 || mes > 12)
				throw new ArgumentException("El mes es inválido");
			if (anio < 1900 || anio > DateTime.Now.Year)
				throw new ArgumentException("El año es inválido");

			var ret = new List<int>();

			for (int i = 0; i < 3; i++)
			{
				int mesActual = mes - i;
				int anioActual = anio;

				if (mesActual <= 0)
				{
					mesActual += 12;
					anioActual -= 1;
				}

				try
				{
					int horasTareas = _context.GetHorasFuncionario(idFuncionario, mesActual, anioActual);
					ret.Add(horasTareas);
				}
				catch (ArgumentOutOfRangeException)
				{
					break;
				}
			}

			return ret;
		}
	}
}

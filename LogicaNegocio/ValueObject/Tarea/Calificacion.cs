using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.ValueObject.Tarea
{
	public class Calificacion
	{
		public int Nota;
		
		public string  Comentario;

		public int? TareaId;
		public Calificacion() { }

		public Calificacion(int nota, string comentario, int? tareaId)
		{
			this.Nota = nota;
			this.Comentario = comentario;
			if(tareaId != null ) TareaId = tareaId;

		}
	}
}

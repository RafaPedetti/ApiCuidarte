using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infraestructura.LogicaAccesoDatos.EF
{
	public class RepositorioTarea : IRepositorioTarea
	{
		private CuidarteContext _context;
		public RepositorioTarea(CuidarteContext context)
		{
			_context = context;
		}

		public Tarea Add(Tarea obj)
		{
			if (obj == null) throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo.");

			obj.AplicarConsumoServicios(obj.serviciosUsados); 

			_context.Tareas.Add(obj);
			_context.Update(obj.Cliente);
			_context.SaveChanges();

			return obj;
		}


		public void Delete(int id)
		{
			Tarea tarea = GetById(id);
			if (tarea == null)
			{
				throw new KeyNotFoundException($"Tipo de plan con ID {id} no encontrado.");
			}
			tarea.Eliminado = true;
			Update(tarea);
		}

		public IEnumerable<Tarea> GetAll(int pagina, string? usuario)
		{
			if(usuario == null) 
			{
				return _context.Tareas
		.Include(t => t.EmpleadoResponsable)
		.Include(t => t.Cliente)
		.Include(t => t.Calificación)
		.Include(t => t.serviciosUsados).ThenInclude(s => s.tipoServicio)
		.Include(t => t.ServiciosExtras).ThenInclude(s => s.tipoServicio)
		.Where(ts => !ts.Eliminado)
		.OrderByDescending(t => t.fecha)
		.Skip(pagina * Parametros.MaxItemsPaginado)
		.Take(Parametros.MaxItemsPaginado)
		.ToList();
			}
			else
			{
				return _context.Tareas
			.Include(t => t.EmpleadoResponsable)
			.Include(t => t.Cliente)
			.Include(t => t.Calificación)
			.Include(t => t.serviciosUsados).ThenInclude(s => s.tipoServicio)
			.Include(t => t.ServiciosExtras).ThenInclude(s => s.tipoServicio)
			.Where(t => !t.Eliminado && t.EmpleadoResponsable.Email.Value.Equals(usuario))
			.OrderByDescending(t => t.fecha)
			.Skip(pagina * Parametros.MaxItemsPaginado)
			.Take(Parametros.MaxItemsPaginado)
			.ToList();
			}

		}

		public int TotalItemsAsync()
		{
			return _context.Tareas.Count(t => !t.Eliminado);
		}

		public Tarea GetById(int id)
		{
			if (id == null)
			{
				throw new ArgumentNullException(nameof(id), "El ID no puede ser nulo.");
			}
			Tarea tarea = _context.Tareas
				.Include(t => t.EmpleadoResponsable)
				.Include(t => t.Cliente)
				.Include(t => t.Calificación)
				.Include(t => t.serviciosUsados).ThenInclude(s => s.tipoServicio)
				.Include(t => t.ServiciosExtras).ThenInclude(s => s.tipoServicio)
				.FirstOrDefault(t => t.Id == id);

			if (tarea == null)
			{
				throw new KeyNotFoundException($"Tipo de plan con ID {id} no encontrado.");
			}
			return tarea;
		}


		public IEnumerable<Tarea> GetByTexto(string texto,string? usuario)
		{
			texto = texto?.ToLower()?.Trim();
			IEnumerable<Tarea> query;
			if (usuario == null)
			{
				 query = _context.Tareas
				.Include(t => t.EmpleadoResponsable)
				.Include(t => t.Cliente)
				.Include(t => t.Calificación)
				.Include(t => t.ServiciosExtras).ThenInclude(s => s.tipoServicio)
				.Include(t => t.serviciosUsados).ThenInclude(s => s.tipoServicio)
				.Where(t => !t.Eliminado)
				.AsEnumerable()
				.Where(t =>
					(!string.IsNullOrWhiteSpace(t.Descripcion) && t.Descripcion.ToLower().Contains(texto)) ||
					(!string.IsNullOrWhiteSpace(t.Cliente.NombreCompleto.Nombre) && t.Cliente.NombreCompleto.Apellido.ToLower().Contains(texto)) ||
							(!string.IsNullOrWhiteSpace(t.EmpleadoResponsable.NombreCompleto.Nombre) && t.EmpleadoResponsable.NombreCompleto.Apellido.ToLower().Contains(texto)) ||
					t.serviciosUsados.Any(s => s.tipoServicio?.Nombre.ToLower().Contains(texto) == true) ||
					t.ServiciosExtras.Any(s => s.tipoServicio?.Nombre.ToLower().Contains(texto) == true) ||
					(!string.IsNullOrWhiteSpace(t.Estado.ToString()) && t.Estado.ToString().ToLower().Contains(texto))
				);
			}
			else
			{
				query = _context.Tareas
		.Include(t => t.EmpleadoResponsable)
		.Include(t => t.Cliente)
		.Include(t => t.Calificación)
		.Include(t => t.ServiciosExtras).ThenInclude(s => s.tipoServicio)
		.Include(t => t.serviciosUsados).ThenInclude(s => s.tipoServicio)
		.Where(t => !t.Eliminado &&
			(
				(!string.IsNullOrWhiteSpace(t.Descripcion) && t.Descripcion.ToLower().Contains(texto)) ||
				(!string.IsNullOrWhiteSpace(t.Cliente.NombreCompleto.Nombre) && t.Cliente.NombreCompleto.Apellido.ToLower().Contains(texto)) ||
				(!string.IsNullOrWhiteSpace(t.EmpleadoResponsable.NombreCompleto.Nombre) && t.EmpleadoResponsable.NombreCompleto.Apellido.ToLower().Contains(texto)) ||
				t.serviciosUsados.Any(s => s.tipoServicio!.Nombre.ToLower().Contains(texto)) ||
				t.ServiciosExtras.Any(s => s.tipoServicio!.Nombre.ToLower().Contains(texto)) ||
				(!string.IsNullOrWhiteSpace(t.Estado.ToString()) && t.Estado.ToString().ToLower().Contains(texto))
			)
			&& t.EmpleadoResponsable != null
			&& t.EmpleadoResponsable.Email.Value.ToLower() == usuario.ToLower()
		);

			}


			return query.ToList();
		}



		public Tarea Update(Tarea obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo.");
			}
			Tarea tarea = GetById(obj.Id);
			tarea.Update(obj.Cliente, obj.EmpleadoResponsable, obj.fecha, obj.Descripcion, obj.Estado, obj.Calificación ?? null,obj.serviciosUsados,obj.ServiciosExtras);
			_context.Tareas.Update(tarea);
			_context.SaveChanges();
			return tarea;
		}

		public IEnumerable<Tarea> GetAll()
		{
			throw new NotImplementedException();
		}

		public int GetHorasFuncionario(int idFuncionario, int mes, int anio)
		{
			var inicioMes = new DateTime(anio, mes, 1, 0, 0, 0, DateTimeKind.Utc);
			var finMes = inicioMes.AddMonths(1);


			var tareasDelMes = _context.Tareas
					.Include(t => t.EmpleadoResponsable)
					.Include(t => t.Cliente)
					.Include(t => t.serviciosUsados).ThenInclude(s => s.tipoServicio)
					.Include(t => t.ServiciosExtras).ThenInclude(s => s.tipoServicio)
					.Where(ts => !ts.Eliminado)
				.Where(t => t.EmpleadoResponsable.Id == idFuncionario &&
							t.fecha >= inicioMes &&
							t.fecha < finMes)
				.ToList();

			var totalHoras = tareasDelMes.Sum(t =>
				t.ServiciosExtras.Sum(s => s.cantServicios) +
				t.serviciosUsados.Sum(s => s.cantServicios)
			);

			return totalHoras;
		}

		public IEnumerable<Tarea> GetTareasByCliente(int idCliente)
		{
			if (idCliente == null)
			{
				throw new ArgumentNullException(nameof(idCliente), "El ID no puede ser nulo.");
			}
		return _context.Tareas
				.Include(t => t.EmpleadoResponsable)
				.Include(t => t.Cliente)
				.Include(t => t.Calificación)
				.Include(t => t.serviciosUsados).ThenInclude(s => s.tipoServicio)
				.Include(t => t.ServiciosExtras).ThenInclude(s => s.tipoServicio)
				.Where(t => t.Cliente.Id == idCliente)
				.ToList();
		}
	}
}
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepocitorio;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.LogicaAccesoDatos.EF
{
	public class RepositorioCliente : IRepositorioCliente
	{
		private CuidarteContext _context;
		public RepositorioCliente(CuidarteContext context)
		{
			_context = context;
		}
		public Cliente Add(Cliente obj)
		{
			if (obj == null) throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo.");
			bool existe = _context.Clientes
			.Where(c => !c.Eliminado && c.CI == obj.CI)
			.Any();

			if (existe)
				throw new DomainException("Ya existe un cliente con esa cédula.");

			_context.Clientes.Add(obj);
			_context.SaveChanges();
			return obj;
		}

		public void CambiarPlan(int idCliente, TipoPlan tipoPlan)
		{
			Cliente cliente = GetById(idCliente);
			if (cliente == null)
			{
				throw new DomainException($"Cliente con ID {idCliente} no encontrado.");
			}
			cliente.TipoPlanId = tipoPlan.Id;
			cliente.Plan = tipoPlan;
			Update(cliente);
		}

		public void Delete(int id)
		{
			Cliente cliente = GetById(id);
			if (cliente == null)
			{
				throw new DomainException($"Cliente con ID {id} no encontrado.");
			}
			cliente.Eliminado = true;
			Update(cliente);
		}

		public IEnumerable<Cliente> GetAll(int pagina, string? usuario)
		{
			return _context.Clientes.Include(c => c.Plan)
			.Include(c => c.ServiciosDisponibles).ThenInclude(s => s.tipoServicio)
			.Include(c => c.ServiciosExtras).ThenInclude(s => s.tipoServicio)
			.Include(c => c.Suscripcion)
			.Where(c => !c.Eliminado).Skip(pagina * Parametros.MaxItemsPaginado)
		.Take(Parametros.MaxItemsPaginado).ToList();
		}

		public IEnumerable<Cliente> GetAll()
		{
			return _context.Clientes.Include(c => c.Plan)
			.Include(c => c.ServiciosDisponibles).ThenInclude(s => s.tipoServicio)
			.Where(c => !c.Eliminado).ToList();
		}

		public Cliente GetById(int id)
		{
			Cliente cliente = _context.Clientes
				.Include(c => c.Plan)
				.ThenInclude(tp => tp.Servicios)
				.ThenInclude(s => s.tipoServicio)
				.Include(c => c.ServiciosDisponibles)
				.Include(c => c.Suscripcion)
				.FirstOrDefault(c => c.Id == id);

			if (cliente == null)
			{
				throw new DomainException($"El cliente con ID {id} no encontrado.");
			}
			return cliente;
		}

		public IEnumerable<Cliente> GetByTexto(string texto, string? usuario)
		{
			if (string.IsNullOrWhiteSpace(texto))
				return Enumerable.Empty<Cliente>();

			texto = texto.Trim().ToLower();

			var query = _context.Clientes
				.Include(c => c.ServiciosDisponibles)
					.ThenInclude(s => s.tipoServicio)
				.Include(c => c.ServiciosExtras)
				.Where(c => !c.Eliminado)
				.Where(c =>
					(!string.IsNullOrEmpty(c.NombreCompleto.Nombre) && c.NombreCompleto.Nombre.ToLower().Contains(texto)) ||
					(!string.IsNullOrEmpty(c.NombreCompleto.Apellido) && c.NombreCompleto.Apellido.ToLower().Contains(texto)) ||
					(!string.IsNullOrEmpty(c.Email.Value) && c.Email.Value.ToLower().Contains(texto)) ||
					(!string.IsNullOrEmpty(c.CI) && c.CI.Contains(texto)) ||
					(!string.IsNullOrEmpty(c.Direccion) && c.Direccion.ToLower().Contains(texto)) ||
					(!string.IsNullOrEmpty(c.FormaPago) && c.FormaPago.ToLower().Contains(texto)) ||
					(!string.IsNullOrEmpty(c.Celular.Value) && c.Celular.Value.ToLower().Contains(texto)) ||
					(!string.IsNullOrEmpty(c.Telefono.Value) && c.Telefono.Value.ToLower().Contains(texto)) ||
					(!string.IsNullOrEmpty(c.ResponsablePago) && c.ResponsablePago.ToLower().Contains(texto))
				);

			return query.ToList();
		}

		public int TotalItems()
		{
			return _context.Clientes.Count(c => !c.Eliminado);
		}

		public Cliente Update(Cliente obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(nameof(obj), "El objeto no puede ser nulo.");
			}
			Cliente cliente = GetById(obj.Id);
			if (!cliente.CI.Equals(obj.CI))
			{
				bool existe = _context.Clientes.Any(c => c.CI == obj.CI);
				if (existe)
					throw new DomainException("Ya existe un cliente con esa cédula.");


			}
			cliente.Update(obj);
			cliente.CambiarPlan(obj.Plan);
			if (obj.Plan != null && obj.Plan.Id > 0)
			{
				_context.Entry(obj.Plan).State = EntityState.Modified;
			}

			_context.Clientes.Update(cliente);
			_context.SaveChanges();
			return cliente;
		}


	}
}

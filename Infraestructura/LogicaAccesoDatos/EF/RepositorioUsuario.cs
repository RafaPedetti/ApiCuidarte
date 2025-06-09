using Infraestructura.LogicaAccesoDatos.Excepciones;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepocitorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.LogicaAccesoDatos.EF
{
	public class RepositorioUsuario : IRepositorioUsuario
	{
		private CuidarteContext _context;

		public RepositorioUsuario(CuidarteContext context)
		{
			_context = context;
		}

		public void Add(Usuario obj)
		{
			if( obj == null)
			{
				throw new ArgumentNullException(nameof(obj), "El usuario no puede ser nulo.");
			}
			try
			{
			obj.Id = 0; 
			_context.Usuarios.Add(obj);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new Exception("Error al agregar el usuario: " + ex.Message, ex);
			}
		}

		public void Delete(int id)
		{
			Usuario user = GetById(id);
			if (user == null)
			{
				throw new NotFoundException();
			}
			Usuario userCopia = user;
			userCopia.Eliminado = true;
			Update(id, userCopia);
		}

		public IEnumerable<Usuario> GetAll()
		{
			IEnumerable<Usuario> usuarios = _context.Usuarios
			  .Where(user => !user.Eliminado);

			if(usuarios.Count() == 0) throw new Exception("No se encontraron usuarios.");
			return usuarios.ToList();
		}

		public Usuario GetById(int id)
		{
			Usuario u = _context.Usuarios.FirstOrDefault(user => user.Id == id);
			if( u == null)
			{
				throw new NotFoundException($"Usuario con ID {id} no encontrado.");
			}
			return u;
		}

		public Usuario GetByLogin(string email, string pass)
		{
			Usuario unUser = _context.Usuarios
	  .AsEnumerable()
	  .FirstOrDefault(user => user.Email.Value == email && user.Password.VerifyPassword(pass));
			return unUser;
		}

		public IEnumerable<Usuario> GetByText(string texto)
		{
			IEnumerable<Usuario> usuarios = _context.Usuarios
				.Where(user => !user.Eliminado && (user.NombreCompleto.Nombre.Contains(texto) || user.NombreCompleto.Apellido.Contains(texto) || user.Email.Value.Contains(texto)))
				.AsEnumerable();
			if (usuarios == null) throw new Exception("No se encontraron usuarios.");
			return usuarios;
		}

		public void Update(int id, Usuario obj)
		{
			try
			{
				Usuario user = GetById(id);
				if (user == null)
				{
					throw new NotFoundException();
				}
				if (!user.Email.Value.Equals(obj.Email.Value))
				{
					throw new ArgumentException("No se puede editar el Email");
				}
				_context.Usuarios.Update(user);
				_context.SaveChanges(true);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}

using LogicaAplicacion.Dtos.Usuarios;
using LogicaNegocio.DtosAuxs.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.Excepciones.Usuario;
using LogicaNegocio.InterfacesServicios;
using LogicaNegocio.InterfazServicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi;

namespace ApiCuidarte.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsuarioController : ControllerBase
	{
		IObtenerTodos<UsuarioDto> _getAll;
		IAlta<UsuarioDto> _alta;
		IObtener<UsuarioDto> _obtener;
		IEditar<UsuarioDto> _editar;
		IEliminar<UsuarioDto> _eliminar;
		ILogin<UsuarioDto> _obtenerLogin;
		IObtenerPorTexto<UsuarioDto> _obtenerPorTexto;
		IObtenerHorasDelMes _obtenerHorasDelMes;

		public UsuarioController(
			IObtenerTodos<UsuarioDto> getAll,
			IAlta<UsuarioDto> alta,
			IObtener<UsuarioDto> obtener,
			IObtenerPorTexto<UsuarioDto> obtenerPorTexto,
			IEditar<UsuarioDto> editar,
			IEliminar<UsuarioDto> eliminar,
			ILogin<UsuarioDto> login,
			IObtenerHorasDelMes obtenerHorasDelMes
		)
		{
			_getAll = getAll;
			_alta = alta;
			_obtener = obtener;
			_obtenerPorTexto = obtenerPorTexto;
			_editar = editar;
			_eliminar = eliminar;
			_obtenerLogin = login;
			_obtenerHorasDelMes = obtenerHorasDelMes;
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPost]
		[Route("Login")]
		public IActionResult Login(UsuarioLoginDto user)
		{
			try
			{
				if (string.IsNullOrEmpty(user.Email) && string.IsNullOrEmpty(user.Password))
				{
					throw new UsuarioException("Ni el correo ni la contraseña debe ser vacia");
				}
				UsuarioDto usuario = _obtenerLogin.Ejecutar(user.Email, user.Password);

				if (usuario == null || usuario.Eliminado)
				{
					throw new ArgumentException("No se encontro el usuario");
				}
				var token = ManejadorJwt.GenerarToken(usuario);
				var usuarioConToken = usuario with { token = token };

				return Ok(usuarioConToken);
			}
			catch (DomainException ex)
			{
				return StatusCode(400, ex.Message);
			}
			catch (ArgumentException ex)
			{
				return StatusCode(500, "Hupp" + ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Hupps hubo un error intente nuevamente mas tarde");
			}
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPost]
		[Route("Crear")]
		[Authorize]
		public IActionResult Crear(UsuarioDto user)
		{
			try
			{
				UsuarioDto uCreado = _alta.Ejecutar(user);
				return Ok(uCreado);
			}
			catch (DomainException ex)
			{
				return StatusCode(400, ex.Message);
			}
			catch (ArgumentException ex)
			{
				return StatusCode(500, "Hupp" + ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Hupps hubo un error intente nuevamente mas tarde");
			}
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPut]
		[Route("Editar")]
		[Authorize]
		public IActionResult Editar(int id, UsuarioDto user)
		{
			try
			{
				UsuarioDto uEditado = _editar.Ejecutar(user);
				return Ok(uEditado);
			}
			catch (DomainException ex)
			{
				return StatusCode(400, ex.Message);
			}
			catch (ArgumentException ex)
			{
				return StatusCode(500, "Hupp" + ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Hupps hubo un error intente nuevamente mas tarde");
			}
		}
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpDelete]
		[Route("Eliminar")]
		[Authorize]
		public IActionResult Eliminar([FromBody]int id)
		{
			try
			{
				_eliminar.Ejecutar(id);
				return Ok();
			}
			catch (DomainException ex)
			{
				return StatusCode(400, ex.Message);
			}
			catch (ArgumentException ex)
			{
				return StatusCode(500, "Hupp" + ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Hupps hubo un error intente nuevamente mas tarde");
			}
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Route("ObtenerPorId")]
		[HttpGet]
		[Authorize]
		public IActionResult ObtenerPorId(int id)
		{
			try
			{
				var usuario = _obtener.Ejecutar(id);
				if (usuario == null || usuario.Eliminado)
				{
					throw new UsuarioException("No se encontro el usuario");
				}
				return Ok(usuario);
			}
			catch (DomainException ex)
			{
				return StatusCode(400, ex.Message);
			}
			catch (ArgumentException ex)
			{
				return StatusCode(500, "Hupp" + ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Hupps hubo un error intente nuevamente mas tarde");
			}
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Route("ObtenerPorTexto")]
		[HttpGet]
		[Authorize]
		public IActionResult ObtenerPorTexto(string texto)
		{
			try
			{
				if (string.IsNullOrEmpty(texto))
				{
					throw new UsuarioException("El texto de busqueda no puede ser vacio");
				}
				IEnumerable<UsuarioDto> usuarios = _obtenerPorTexto.Ejecutar(texto);
				if (usuarios == null || !usuarios.Any())
				{
					throw new UsuarioException("No se encontraron usuarios con el texto proporcionado");
				}
				return Ok(usuarios);
			}
			catch (DomainException ex)
			{
				return StatusCode(400, ex.Message);
			}
			catch (ArgumentException ex)
			{
				return StatusCode(500, "Hupp" + ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Hupps hubo un error intente nuevamente mas tarde");
			}
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Route("ObtenerHorasDelMes")]
		[HttpGet]
		[Authorize]
		public IActionResult ObtenerHorasDelMes(int idFuncionario)
		{
			try
			{
				DateTime now = DateTime.Now;
				var horas = _obtenerHorasDelMes.Ejecutar(idFuncionario,now.Month,now.Year);
				if (horas == null)
				{
					throw new UsuarioException("No se encontro el usuario");
				}
				return Ok(horas);
			}
			catch (DomainException ex)
			{
				return StatusCode(400, ex.Message);
			}
			catch (ArgumentException ex)
			{
				return StatusCode(500, "Hupp" + ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Hupps hubo un error intente nuevamente mas tarde");
			}
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpGet]
		[Route("ObtenerTodos")]
		[Authorize]
		public IActionResult ObtenerTodos()
		{
			try
			{
				var rol = User.FindFirst(ClaimTypes.Role)?.Value;
				IEnumerable<UsuarioDto> usuarios;
				if (rol == Administrador.DiscriminadorStatic)
				{
					usuarios = _getAll.Ejecutar();
				}
				else
				{
					usuarios = _obtenerPorTexto.Ejecutar(User.FindFirst(ClaimTypes.Email).Value);
				}
				if (usuarios == null || !usuarios.Any())
				{
					throw new UsuarioException("No se encontraron usuarios");
				}
				return Ok(usuarios);
			}
			catch (DomainException ex)
			{
				return StatusCode(204, ex.Message);
			}
			catch (ArgumentException ex)
			{
				return StatusCode(500, "Hupp" + ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Hupps hubo un error intente nuevamente mas tarde");
			}
		}
	}
}

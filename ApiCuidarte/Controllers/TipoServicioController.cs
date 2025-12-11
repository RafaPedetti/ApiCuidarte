using Infraestructura.LogicaAccesoDatos.Excepciones;
using LogicaAplicacion.Dtos.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.Excepciones.Usuario;
using LogicaNegocio.InterfacesServicios;
using LogicaNegocio.InterfazServicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCuidarte.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class TipoServicioController : ControllerBase
	{
		IAlta<TipoServicio> _alta;
		IEditar<TipoServicio> _editar;
		IEliminar<TipoServicio> _eliminar;
		IObtenerTodos<TipoServicio> _getAll;
		IObtener<TipoServicio> _obtener;

		public TipoServicioController(
			IAlta<TipoServicio> alta,
			IEditar<TipoServicio> editar,
			IEliminar<TipoServicio> eliminar,
			IObtenerTodos<TipoServicio> getAll,
			IObtener<TipoServicio> obtener
		)
		{
			_alta = alta;
			_editar = editar;
			_eliminar = eliminar;
			_getAll = getAll;
			_obtener = obtener;
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPost]
		[Route("Crear")]
		public IActionResult Crear(TipoServicio ts)
		{
			try
			{
				TipoServicio tsCreado= _alta.Ejecutar(ts);
				Console.WriteLine(tsCreado);
				return Ok(tsCreado);
			}
			catch (DomainException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch (UnauthorizedAccessException)
			{
				return Unauthorized(new { message = "No autorizado" });
			}
			catch (NotFoundException ex)
			{
				return NotFound(new { message = ex.Message });
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					new { message = "Hupps hubo un error, intente nuevamente más tarde" });
			}
		}
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPut]
		[Route("Editar")]
		public IActionResult Editar(TipoServicio tp)
		 {
			try
			{
				_editar.Ejecutar(tp);
				return Ok(tp);
			}
			catch (DomainException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch (UnauthorizedAccessException)
			{
				return Unauthorized(new { message = "No autorizado" });
			}
			catch (NotFoundException ex)
			{
				return NotFound(new { message = ex.Message });
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					new { message = "Hupps hubo un error, intente nuevamente más tarde" });
			}
		}
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpDelete]
		[Route("Eliminar")]
		public IActionResult Eliminar([FromBody] int id)
		{
			try
			{
				_eliminar.Ejecutar(id);
				return Ok();
			}
			catch (DomainException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch (UnauthorizedAccessException)
			{
				return Unauthorized(new { message = "No autorizado" });
			}
			catch (NotFoundException ex)
			{
				return NotFound(new { message = ex.Message });
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					new { message = "Hupps hubo un error, intente nuevamente más tarde" });
			}
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Route("ObtenerPorId")]
		[HttpGet]
		public IActionResult ObtenerPorId(int id)
		{
			try
			{
				var usuario = _obtener.Ejecutar(id);
				if (usuario == null || usuario.Eliminado)
				{
					throw new DomainException("No se encontro el tipo servicio");
				}
				return Ok(usuario);
			}
			catch (DomainException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch (UnauthorizedAccessException)
			{
				return Unauthorized(new { message = "No autorizado" });
			}
			catch (NotFoundException ex)
			{
				return NotFound(new { message = ex.Message });
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					new { message = "Hupps hubo un error, intente nuevamente más tarde" });
			}
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpGet]
		[Route("ObtenerTodos")]
		public IActionResult ObtenerTodos()
		{
			try
			{
				IEnumerable<TipoServicio> tp = _getAll.Ejecutar();
				if (tp == null || !tp.Any())
				{
					throw new DomainException("No se encontraro el tipo servicio");
				}
				return Ok(tp);
			}
			catch (DomainException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch (UnauthorizedAccessException)
			{
				return Unauthorized(new { message = "No autorizado" });
			}
			catch (NotFoundException ex)
			{
				return NotFound(new { message = ex.Message });
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					new { message = "Hupps hubo un error, intente nuevamente más tarde" });
			}
		}
	}

}

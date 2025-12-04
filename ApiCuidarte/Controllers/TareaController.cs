using LogicaAplicacion.Dtos;
using LogicaAplicacion.Dtos.Tareas;
using LogicaAplicacion.Dtos.Usuarios;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.Excepciones.Tarea;
using LogicaNegocio.InterfacesServicios;
using LogicaNegocio.InterfazServicios;
using LogicaNegocio.ValueObject.Tarea;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiCuidarte.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class TareaController : ControllerBase
	{
		IAlta<TareaDto> _alta;
		IEditar<TareaDto> _editar;
		IEliminar<TareaDto> _eliminar;
		IObtenerPaginado<PaginadoResultado<TareaDto>> _getAll;
		IObtener<TareaDto> _obtener;
		IObtenerPorTexto<TareaDto> _obtenerPorTexto;
		ICalificar<CalificacionDto> _calificar;


		public TareaController(
			IAlta<TareaDto> alta,
			IEditar<TareaDto> editar,
			IEliminar<TareaDto> eliminar,
			IObtenerPaginado<PaginadoResultado<TareaDto>> getAll,
			IObtener<TareaDto> obtener,
			IObtenerPorTexto<TareaDto> obtenerPorTexto,
			ICalificar<CalificacionDto> calificar
		)
		{
			_alta = alta;
			_editar = editar;
			_eliminar = eliminar;
			_getAll = getAll;
			_obtener = obtener;
			_obtenerPorTexto = obtenerPorTexto;
			_calificar = calificar;
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPost]
		[Route("Crear")]
		public IActionResult Crear(TareaDto t)
		{
			try
			{
				TareaDto tCreado = _alta.Ejecutar(t);
				return Ok(tCreado);
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
		public IActionResult Editar(TareaDto t)
		{
			try
			{
				TareaDto tCreado = _editar.Ejecutar(t);
				return Ok(tCreado);
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
		public IActionResult Eliminar([FromBody] int id)
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
		public IActionResult ObtenerPorId(int id)
		{
			try
			{
				TareaDto c = _obtener.Ejecutar(id);
				if (c == null)
				{
					throw new TareaException("No se encontro la tarea");
				}
				return Ok(c);
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
		public IActionResult ObtenerPorTexto([FromQuery]string texto)
		{
			try
			{
				var rol = User.FindFirst(ClaimTypes.Role)?.Value;
				IEnumerable<TareaDto> c;
				if (rol == Administrador.DiscriminadorStatic)
				{
				c = _obtenerPorTexto.Ejecutar(texto,null);
				}
				else
				{
					c = _obtenerPorTexto.Ejecutar(texto, User.FindFirst(ClaimTypes.Email).Value);

				}
				if (c == null)
				{
					throw new TareaException("No se encontro la tarea");
				}
				return Ok(c);
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
		public IActionResult ObtenerTodos(int pagina)
		{
			try
			{
				var rol = User.FindFirst(ClaimTypes.Role)?.Value;
				PaginadoResultado<TareaDto> c;
				if (rol == Administrador.DiscriminadorStatic)
				{
					c = _getAll.Ejecutar(pagina, null);
				}
				else
				{
					c = _getAll.Ejecutar(pagina, User.FindFirst(ClaimTypes.Email).Value);
				}
				if (c == null)
				{
					throw new TareaException("No se encontraron tareas");
				}
				return Ok(c);
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

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPost]
		[Route("Calificar")]
		public IActionResult Calificar([FromBody] CalificacionDto calificacion)
		{
			try
			{
				CalificacionDto c = _calificar.Ejecutar(calificacion);
				if (c == null)
				{
					throw new TareaException("No se encontraro la  tarea");
				}
				return Ok(c);
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

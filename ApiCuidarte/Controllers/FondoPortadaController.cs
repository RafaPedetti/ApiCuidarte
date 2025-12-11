using Infraestructura.LogicaAccesoDatos.Excepciones;
using LogicaAplicacion.Dtos;
using LogicaAplicacion.Dtos.Usuarios;
using LogicaNegocio.DtosAuxs.Usuarios;
using LogicaNegocio.Excepciones;
using LogicaNegocio.Excepciones.Usuario;
using LogicaNegocio.InterfacesServicios;
using LogicaNegocio.InterfazServicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi;

namespace ApiCuidarte.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FondoPortadaController : ControllerBase
	{
		IObtener<FondoPortadaDto> _getAll;
		IAlta<FondoPortadaDto> _alta;

		public FondoPortadaController(
			IObtener<FondoPortadaDto> getAll,
			IAlta<FondoPortadaDto> alta
		)
		{
			_getAll = getAll;
			_alta = alta;
		}
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Route("Obtener")]
		[HttpGet]
		public IActionResult Obtener()
		{
			try
			{
				var fondo = _getAll.Ejecutar(0);
				return Ok(fondo);
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
		[Route("Crear")]
		[Authorize]
		public IActionResult Crear(FondoPortadaDto fondo)
		{
			try
			{
				FondoPortadaDto fCreado = _alta.Ejecutar(fondo);
				return Ok(fCreado);
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

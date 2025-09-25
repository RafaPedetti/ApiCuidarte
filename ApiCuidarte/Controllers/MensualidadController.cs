using LogicaAplicacion.Dtos.Suscripciones;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesServicios;
using LogicaNegocio.InterfacesServicios.Mensualidades;
using LogicaNegocio.InterfazServicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCuidarte.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class MensualidadController : ControllerBase
	{
		IObtenerPorCliente<MensualidadDto> _getAll;
		IPagarMensualidades<SuscripcionDto> _pagarMensualidades;
		public MensualidadController
			(
			IObtenerPorCliente<MensualidadDto> getAll,
			IPagarMensualidades<SuscripcionDto> pagarMensualidades
			)
		{
			this._getAll = getAll;

			this._pagarMensualidades = pagarMensualidades;
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPost]
		[Route("Pagar")]
		public IActionResult Pagar([FromBody]int idSuscripcion)
		{
			try
			{
				Mensualidad mCreado = _pagarMensualidades.Ejecutar(idSuscripcion);
				return Ok(mCreado);
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
		[HttpGet]
		[Route("ObtenerTodos")]
		public IActionResult GetAll(int idSuscripcion)
		{
			try
			{
			    IEnumerable<MensualidadDto> mCreado = _getAll.Ejecutar(idSuscripcion);
				return Ok(mCreado);
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
	}
}

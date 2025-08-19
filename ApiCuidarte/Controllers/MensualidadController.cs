using LogicaAplicacion.Dtos;
using LogicaAplicacion.Dtos.Clientes;
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
		IAlta<Suscripcion, Suscripcion> _alta;
		IObtenerPorCliente<Mensualidad> _getAll;
		IObtenerPorEmpresa<Mensualidad> _obtenerPorEmpresa;
		IPagarMensualidades<Suscripcion> _pagarMensualidades;
		ICancelarConvenio<Suscripcion> _cancelarConvenio;
		public MensualidadController
			(
			IAlta<Suscripcion, Suscripcion> alta,
			IObtenerPorCliente<Mensualidad> getAll,
			IObtenerPorEmpresa<Mensualidad> obtenerPorEmpresa,
			IPagarMensualidades<Suscripcion> pagarMensualidades,
			ICancelarConvenio<Suscripcion> cancelarConvenio
			)
		{
			this._alta = alta;
			this._getAll = getAll;
			this._obtenerPorEmpresa = obtenerPorEmpresa;
			this._pagarMensualidades = pagarMensualidades;
			this._cancelarConvenio = cancelarConvenio;

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

	}
}

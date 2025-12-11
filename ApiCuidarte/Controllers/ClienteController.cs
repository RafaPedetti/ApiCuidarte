using Infraestructura.LogicaAccesoDatos.Excepciones;
using LogicaAplicacion.Dtos;
using LogicaAplicacion.Dtos.Clientes;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.Excepciones.Cliente;
using LogicaNegocio.Excepciones.Usuario;
using LogicaNegocio.InterfacesServicios;
using LogicaNegocio.InterfazServicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiCuidarte.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ClienteController : ControllerBase
	{
		IAlta<ClienteDto> _alta;
		IEditar<ClienteDto> _editar;
		IEliminar<ClienteDto> _eliminar;
		IObtenerPaginado<PaginadoResultado<ClienteDto>> _getAll;
		IObtener<ClienteDto> _obtener;
		IObtenerPorTexto<ClienteDto> _obtenerPorTexto;
		IFormularioNuevoCliente<ClienteFormularioDto> _formularioNuevoCliente;

		public ClienteController(
			IAlta<ClienteDto> alta,
			IEditar<ClienteDto> editar,
			IEliminar<ClienteDto> eliminar,
			IObtenerPaginado<PaginadoResultado<ClienteDto>> getAll,
			IObtener<ClienteDto> obtener,
			IObtenerPorTexto<ClienteDto> obtenerPorTexto,
			IFormularioNuevoCliente<ClienteFormularioDto> formularioNuevoCliente
		)
		{
			_alta = alta;
			_editar = editar;
			_eliminar = eliminar;
			_getAll = getAll;
			_obtener = obtener;
			_obtenerPorTexto = obtenerPorTexto;
			_formularioNuevoCliente = formularioNuevoCliente;
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPost]
		[Route("Crear")]
		public IActionResult Crear(ClienteDto cliente)
		{
			try
			{
				 _alta.Ejecutar(cliente);
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
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPut]
		[Route("Editar")]
		public IActionResult Editar(int id, ClienteDto c)
		{
			try
			{
				ClienteDto cCreado = _editar.Ejecutar(c);
				return Ok(cCreado);
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
				ClienteDto c = _obtener.Ejecutar(id);
				if (c == null)
				{
					throw new ClienteException("No se encontro el cliente");
				}
				return Ok(c);
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
		[Route("ObtenerPorTexto")]
		[HttpGet]
		public IActionResult ObtenerPorTexto(string texto)
		{
			try
			{
				IEnumerable<ClienteDto> c = _obtenerPorTexto.Ejecutar(texto,null);
				if (c == null)
				{
					throw new ClienteException("No se encontro el cliente");
				}
				return Ok(c);
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
		public IActionResult ObtenerTodos(int pagina)
		{
			try
			{
				PaginadoResultado<ClienteDto> c = _getAll.Ejecutar(pagina,null);
				if (c == null)
				{
					throw new ClienteException("No se encontraron cliente");
				}
				return Ok(c);
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
		[AllowAnonymous]
		[HttpPost]
		[Route("FormularioNuevoCliente")]
		public IActionResult FormularioNuevoCliente(ClienteFormularioDto cliente)
		{
			try
			{
				_formularioNuevoCliente.Ejecutar(cliente);
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
	}
}

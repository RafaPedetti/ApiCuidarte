using LogicaAplicacion.Dtos.Empresas;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.Excepciones.Cliente;
using LogicaNegocio.InterfazServicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiCuidarte.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class EmpresaController : ControllerBase
	{
		IAlta<EmpresaDto> _alta;
		IEditar<EmpresaDto> _editar;
		IEliminar<EmpresaDto> _eliminar;
		IObtenerTodos<EmpresaDto> _getAll;

		public EmpresaController(
			IAlta<EmpresaDto> alta,
			IEditar<EmpresaDto> editar,
			IEliminar<EmpresaDto> eliminar,
			IObtenerTodos<EmpresaDto> getAll
		)
		{
			_alta = alta;
			_editar = editar;
			_eliminar = eliminar;
			_getAll = getAll;
		}

		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPost]
		[Route("Crear")]
		public IActionResult Crear(EmpresaDto e)
		{
			try
			{
				EmpresaDto eCreado = _alta.Ejecutar(e);
				return Ok(eCreado);
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
		public IActionResult Editar(int id, EmpresaDto e)
		{
			try
			{
				EmpresaDto eCreado = _editar.Ejecutar(e);
				return Ok(eCreado);
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
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpGet]
		[Route("ObtenerTodos")]
		public IActionResult ObtenerTodos()
		{
			try
			{
				IEnumerable<EmpresaDto> e = _getAll.Ejecutar();
				if (e == null)
				{
					throw new ClienteException("No se encontraron cliente");
				}
				return Ok(e);
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

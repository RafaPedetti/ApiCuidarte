using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using LogicaNegocio.Excepciones.Usuario;
using LogicaNegocio.InterfazServicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCuidarte.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class TipoPlanController : ControllerBase
	{
		IAlta<TipoPlan> _alta;
		IEditar<TipoPlan> _editar;
		IEliminar<TipoPlan> _eliminar;
		IObtenerTodos<TipoPlan> _getAll;
		IObtener<TipoPlan> _obtener;

		public TipoPlanController(
			IAlta<TipoPlan> alta,
			IEditar<TipoPlan> editar,
			IEliminar<TipoPlan> eliminar,
			IObtenerTodos<TipoPlan> getAll,
			IObtener<TipoPlan> obtener
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
		public IActionResult Crear(TipoPlan tp)
		{
			try
			{
				_alta.Ejecutar(tp);
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
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPut]
		[Route("Editar")]
		public IActionResult Editar(int id, TipoPlan tp)
		{
			try
			{
				_editar.Ejecutar(id, tp);
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
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpDelete]
		[Route("Eliminar")]
		public IActionResult Eliminar(int id)
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
				TipoPlan tp = _obtener.Ejecutar(id);
				if (tp == null || tp.Eliminado)
				{
					throw new UsuarioException("No se encontro el usuario");
				}
				return Ok(tp);
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
				IEnumerable<TipoPlan> tp = _getAll.Ejecutar();
				if (tp == null || !tp.Any())
				{
					throw new UsuarioException("No se encontraron usuarios");
				}
				return Ok(tp);
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
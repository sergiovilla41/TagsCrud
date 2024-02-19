using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Simem.AppCom.Datos.Repo;

namespace Simem.AppCom.Datos.Servicios.Controllers
{
    /// <summary>
    /// Api para obtener información de novedades
    /// </summary>
    [Route("news")]
    public class NewsController : ControllerBase
    {
        /// <summary>
        /// Obtiene un conjunto de datos en base a su categoria, 
        /// </summary>
        /// <param name="categoria">id de la categoria</param>
        /// <param name="termino">Dato a filtrar</param>
        /// <param name="paginador">Objeto paginador de resultados</param>
        /// <response code="200">Lista de categorías devuelta con éxito</response>
        /// <response code="400">Error durante la consulta</response>
        [HttpGet]
        public async Task<IActionResult> Http_GetConjuntoDatos(string? categoria, string? termino, string paginador)
        {
            try
            {
                Guid? idCategoria = null;
                if (!string.IsNullOrEmpty(categoria))
                {
                    idCategoria = Guid.Parse(categoria);
                }

                Paginador _paginador = JsonConvert.DeserializeObject<Paginador>(paginador) ?? throw new Exception("Paginator is missing");
                DatoRepo conjuntoDatosRepo = new();
                var conjuntosDatos = await conjuntoDatosRepo.GetConjuntosDatos(_paginador, termino, idCategoria);
                int count = await conjuntoDatosRepo.GetConjuntosDatosCount(_paginador, termino, idCategoria);

                return Ok(new { rows = conjuntosDatos, count });

            }
            catch (Exception ex)
            {
                return  BadRequest( new { message = "error al obtener novedades: " + ex.Message });
            }
        }
            
        /// <summary>
        /// Obtiene los detalles de  una novedas
        /// </summary>
        /// <param name="id">identificador unico</param>
        /// <response code="200">Devuelve un registro consultado con éxito</response>
        /// <response code="204">No se encuentra información</response>
        /// <response code="400">Error durante el proceso de consulta</response>
        [Route("detail")]
        [HttpGet]
        public async Task<IActionResult> HttpGetNovedadDetail(string? id)
        {
            try
            {
                Guid Id;
                if (!string.IsNullOrEmpty(id))
                {
                    Id = Guid.Parse(id);
                }
                else
                {
                    return new BadRequestObjectResult(new { message = "No se ingresó el ID de la novedad" });
                }

                Core.Novedad novedadCore = new();
                var novedad = await novedadCore.GetNovedadDetail(Id);

                if (novedad.Novedad != null)
                {
                    return Ok(novedad);
                }
                else
                {
                    return NoContent();
                }

            }
            catch (Exception ex)
            {
                return  BadRequest( new { message = "error al obtener novedades: " + ex.Message });
            }
        }

        /// <summary>
        /// Obtiene una novedad en base a los parametros recibidos.
        /// </summary>
        /// <param name="categoria">id de la categoria</param>
        /// <param name="termino">Dato a filtrar</param>
        /// <param name="paginador">Objeto paginador de resultados</param>
        /// <param name="idGeneracionArchivo">Id conjunto de datos</param>
        /// <response code="200">Devuelve un registro consultado con éxito</response>
        /// <response code="400">Error durante el proceso de consulta</response>
        [Route("filtered")]
        [HttpGet]
        public async Task<IActionResult> HttpGetNovedades(string? categoria, string? termino, string? paginador, string? idGeneracionArchivo)
        {

            try
            {
                Guid? _idGeneracionArchivo = Guid.Empty;
                if (!string.IsNullOrEmpty(idGeneracionArchivo))
                {
                    _idGeneracionArchivo = Guid.Parse(idGeneracionArchivo);
                }

                Guid? _categoria = Guid.Empty;
                if (!string.IsNullOrEmpty(categoria))
                {
                    _categoria = Guid.Parse(categoria);
                }
                Paginador? _paginador = null;
                if (!string.IsNullOrEmpty(paginador))
                {
                    _paginador = JsonConvert.DeserializeObject<Paginador>(paginador);
                }

                if (_paginador == null)
                {
                    return StatusCode(500);
                }

                Core.Novedad novedadCore = new();

                var novedades = await novedadCore.GetNovedadesCategoriaNovedades(_paginador, termino, _categoria, _idGeneracionArchivo);
                int count = await novedadCore.GetNovedadesCount(_paginador, termino, _categoria, _idGeneracionArchivo);

                return Ok(new { rows = novedades, count });
            }
            catch (Exception ex)
            {
                return  BadRequest( new { message = "error al obtener novedades: " + ex.Message });
            }
        }

        /// <summary>
        /// Obtiene las novedades de una categoria
        /// </summary>
        /// <response code="200">Lista de novedades consultadas con éxito</response>
        /// <response code="204">No se encuentra información</response>
        /// <response code="400">Ocurrio un error</response>
        [Route("categories")]
        [HttpGet]
        public async Task<IActionResult> HttpGetNovedadesCategories()
        {
            try
            {
                Core.Novedad novedadCore = new();
                var categoriasNovedades = await novedadCore.GetNovedadesCategories();
                if (categoriasNovedades.Count > 0)
                {
                    return new OkObjectResult(categoriasNovedades);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Ocurrio un error: " + ex.Message });
            }            
        }

        /// <summary>
        /// Obtiene las novedades en base a conjuntos de datos especificos
        /// </summary>
        /// <response code="200">Lista de novedades pro conjunto de datos consultada con éxito</response>
        /// <response code="204">No se encuentra información</response>
        /// <response code="400">Ocurrio un error</response>
        [Route("data-set")]
        [HttpGet]
        public async Task<IActionResult> HttpGetConjuntoDeDatosConNovedades()
        {
            try
            {
                Core.Novedad novedadCore = new();
                var conjuntoDeDatosConNovedades = await novedadCore.GetConjuntoDeDatosConNovedades();
                if (conjuntoDeDatosConNovedades.Count > 0)
                {
                    return new OkObjectResult(conjuntoDeDatosConNovedades);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Ocurrio un error: " + ex.Message });
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Simem.AppCom.Datos.Servicios.Controllers
{
    /// <summary>
    /// Controlador para gestionar capactiones .
    /// </summary>
    [Route("capacitacion")]
    public class CapacitacionController : ControllerBase
    {
        /// <summary>
        /// Se utiliza para obtener todos los tutoriales de capacitación al usuario de SIMEM
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Tutoriales para capacitar al usuario</response>
        /// <response code="400">Ocurrió un error al obtener los tutoriales</response>
        [HttpGet]
        public async Task<IActionResult> GetCapacitacion()
        {
            try
            {
                Core.CapacitacionUsuario core = new Core.CapacitacionUsuario();
                return Ok( new { message = await core.GetCapacitaciones() });
            }
            catch (Exception ex)
            {
                return BadRequest( new { messageError = ex.Message });
            }
        }
    }
}

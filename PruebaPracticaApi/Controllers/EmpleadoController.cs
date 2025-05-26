using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaPracticaApi.Data;
using PruebaPracticaApi.Model;

namespace PruebaPracticaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmpleadoController : ControllerBase
    {
        private readonly EmpleadoRepository _repo;

        public EmpleadoController(EmpleadoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult>ObtenerEmpleados () => Ok(await _repo.ObtenerEmpleados());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpleadoById(int id)
        {
            var empleado = await _repo.ObtenerEmpleadoPorId(id);
            if (empleado == null)
                return NotFound();

            return Ok(empleado); // Esto devuelve solo un objeto
        }


        [HttpPost]
        public async Task<IActionResult> AgregarEmpleado([FromBody] Empleado empleado)
        {
            await _repo.AgregarEmpleado(empleado);
            return Ok();

        }

        [HttpPut]
        public async Task<IActionResult> ActualizarEmpleado([FromBody] Empleado empleado)
        {
            await _repo.ActualizarEmpleado(empleado);
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarEmpleado(int id)
        {
            await _repo.EliminarEmpleado(id);
            return Ok();
        }
    }
}

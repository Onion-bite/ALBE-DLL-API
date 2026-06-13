using BecasAPI.Modelos;
using BecasAPI.Servicios;
using Microsoft.AspNetCore.Mvc;
using static BecasAPI.DTOs.DTOs;

namespace BecasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BecasController : ControllerBase
    {
        // Instancia del servicio de becas
        private readonly BecaServicio _servicio;

        public BecasController()
        {
            _servicio = new BecaServicio();
        }

        [HttpGet]
        public IActionResult ListarBecas()
        {
            List<Beca> becas = _servicio.ListarBecas();
            return Ok(becas);
        }
        // GET: api/becas/{id}
        // USUARIO: Busca una beca por Id usando el ABB
        [HttpGet("{id}")]
        public IActionResult BuscarBeca(int id)
        {
            Beca? beca = _servicio.BuscarBeca(id);

            if (beca == null)
                return NotFound(new RespuestaDTO
                {
                    Exito = false,
                    Mensaje = $"No existe beca con Id {id}"
                });

            return Ok(beca);
        }

        [HttpGet("carrera/{carrera}")]
        public IActionResult FiltrarPorCarrera(string carrera)
        {
            List<Beca> becas = _servicio.FiltrarPorCarrera(carrera);
            return Ok(becas);
        }

        [HttpGet("proximas/{dias}")]
        public IActionResult ObtenerProximasAVencer(int dias)
        {
            List<Beca> becas = _servicio.ObtenerProximasAVencer(dias);
            return Ok(becas);
        }
        
        [HttpPost]
        public IActionResult InsertarBeca([FromBody] CrearBecaDTO dto)
        {
            // Construir el objeto Beca desde el DTO
            Beca beca = new Beca
            {
                Nombre = dto.Nombre,
                Carrera = dto.Carrera,
                FechaLimite = dto.FechaLimite,
                Requisitos = dto.Requisitos,
                Descripcion = dto.Descripcion
            };

            RespuestaOperacion resultado = _servicio.InsertarBeca(beca);

            if (!resultado.Exito)
                return BadRequest(new RespuestaDTO
                {
                    Exito = false,
                    Mensaje = resultado.Mensaje
                });

            return Ok(new RespuestaDTO
            {
                Exito = true,
                Mensaje = resultado.Mensaje
            });
        }
        
        [HttpPut]
        public IActionResult ModificarBeca([FromBody] ModificarBecaDTO dto) 
        {
            // Construir el objeto Beca desde el DTO
            Beca beca = new Beca
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Carrera = dto.Carrera,
                FechaLimite = dto.FechaLimite,
                Requisitos = dto.Requisitos,
                Descripcion = dto.Descripcion
            };
            RespuestaOperacion resultado = _servicio.ModificarBeca(beca);

            if (!resultado.Exito)
                return NotFound(new RespuestaDTO
                {
                    Exito = false,
                    Mensaje = resultado.Mensaje
                });

            return Ok(new RespuestaDTO
            {
                Exito = true,
                Mensaje = resultado.Mensaje
            });
        }
        
        [HttpDelete("{id}")]
        public IActionResult EliminarBeca(int id) 
        {
            RespuestaOperacion resultado = _servicio.EliminarBeca(id);

            if (!resultado.Exito)
                return NotFound(new RespuestaDTO
                {
                    Exito = false,
                    Mensaje = resultado.Mensaje
                });

            return Ok(new RespuestaDTO
            {
                Exito = true,
                Mensaje = resultado.Mensaje
            });
        }
    }
}

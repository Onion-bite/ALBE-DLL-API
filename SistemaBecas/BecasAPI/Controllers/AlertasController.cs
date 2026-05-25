using BecasAPI.DTOs;
using BecasAPI.Modelos;
using BecasAPI.Servicios;
using Microsoft.AspNetCore.Mvc;
using static BecasAPI.DTOs.DTOs;

namespace BecasAPI.Controllers
{
    [ApiController]
    [Route("api/alertas")]
    public class AlertasController : ControllerBase
    {
        // ── DEPENDENCIA ──────────────────────────────────────────────────────
        // El servicio maneja la CustomQueue<Alerta> internamente
        private readonly AlertaService _servicio;

        // ── CONSTRUCTOR ──────────────────────────────────────────────────────
        // ASP.NET inyecta el AlertaService automáticamente
        public AlertasController(AlertaService servicio)
        {
            _servicio = servicio;
        }

        // ── GET /api/alertas ─────────────────────────────────────────────────
        // Lista TODAS las alertas — uso del Admin
        // Internamente llama a ListarAlertas() de la CustomQueue
        [HttpGet]
        public IActionResult ObtenerTodas()
        {
            List<Alerta> alertas = _servicio.ListarTodas();
            return Ok(alertas);
        }

        // ── GET /api/alertas/usuario/{usuarioId} ─────────────────────────────
        // Lista las alertas NO leídas de un usuario específico
        // Usa VerAlertas() que filtra por usuarioId y Leida == false
        [HttpGet("usuario/{usuarioId}")]
        public IActionResult ObtenerPorUsuario(int usuarioId)
        {
            List<Alerta> alertas = _servicio.VerAlertas(usuarioId);

            // Si no tiene alertas pendientes, igual devolvemos 200
            // con lista vacía — no es un error, simplemente no hay
            return Ok(alertas);
        }

        // ── POST /api/alertas ────────────────────────────────────────────────
        // Genera y encola una nueva alerta (FIFO)
        // El body JSON debe tener: UsuarioId, BecaId, Mensaje
        [HttpPost]
        public IActionResult Generar([FromBody] CrearAlertaDTO dto)
        {
            // Llamamos al servicio con los 3 datos necesarios
            RespuestaOperacion resultado = _servicio.GenerarAlerta(
                dto.UsuarioId,
                dto.BecaId,
                dto.Mensaje
            );

            if (!resultado.Exito)
            {
                return BadRequest(new RespuestaDTO
                {
                    Exito = false,
                    Mensaje = resultado.Mensaje
                });
            }

            // HTTP 201: alerta creada y encolada
            return StatusCode(201, new RespuestaDTO
            {
                Exito = true,
                Mensaje = resultado.Mensaje
            });
        }

        // ── PATCH /api/alertas/{id}/leida ────────────────────────────────────
        // Marca una alerta específica como leída
        // Usamos PATCH (no PUT) porque solo modificamos UN campo: Leida
        [HttpPatch("{id}/leida")]
        public IActionResult MarcarComoLeida(int id)
        {
            RespuestaOperacion resultado = _servicio.MarcarLeida(id);

            if (!resultado.Exito)
            {
                // HTTP 404: no existe esa alerta
                return NotFound(new RespuestaDTO
                {
                    Exito = false,
                    Mensaje = resultado.Mensaje
                });
            }

            return Ok(new RespuestaDTO
            {
                Exito = true,
                Mensaje = resultado.Mensaje
            });
        }

    }
}

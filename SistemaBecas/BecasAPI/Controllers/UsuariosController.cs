using BecasAPI.DTOs;
using BecasAPI.Modelos;
using BecasAPI.Servicios;
using Microsoft.AspNetCore.Mvc;
using static BecasAPI.DTOs.DTOs;

namespace BecasAPI.Controllers
{
    [ApiController]
    [Route("api/usuarios")]

    public class UsuariosController : ControllerBase
    {
        // ── DEPENDENCIA ──────────────────────────────────────────────────────
        // Nombre correcto: UsuarioServicio (con 'io'), no UsuarioService
        private readonly UsuarioServicio _servicio;

        // ── CONSTRUCTOR ──────────────────────────────────────────────────────
        public UsuariosController(UsuarioServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            // ListarUsuarios() recorre la LstSimple y devuelve List<Usuario>
            List<Usuario> usuarios = _servicio.ListarUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            // BuscarUsuario devuelve Usuario? (nullable: puede ser null)
            Usuario? usuario = _servicio.BuscarUsuario(id);

            if (usuario == null)
            {
                // HTTP 404: no encontrado
                return NotFound(new RespuestaDTO
                {
                    Exito = false,
                    Mensaje = $"No se encontró el usuario con ID {id}."
                });
            }

            // HTTP 200 con el usuario encontrado
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Registrar([FromBody] CrearUsuarioDTO dto)
        {
            
            Usuario nuevoUsuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Carrera = dto.Carrera,
                Rol = dto.Rol
            };

            // Llamamos al servicio — él asigna el Id y persiste en JSON
            RespuestaOperacion resultado = _servicio.InsertarUsuario(nuevoUsuario);

            if (!resultado.Exito)
            {
                // HTTP 400: datos incorrectos o correo duplicado
                return BadRequest(new RespuestaDTO
                {
                    Exito = false,
                    Mensaje = resultado.Mensaje
                });
            }

            // HTTP 201: recurso creado exitosamente
            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = nuevoUsuario.Id },
                new RespuestaDTO
                {
                    Exito = true,
                    Mensaje = resultado.Mensaje
                }
            );
        }
        
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            RespuestaOperacion resultado = _servicio.EliminarUsuario(id);

            if (!resultado.Exito)
            {
                // HTTP 404: no se encontró el usuario a eliminar
                return NotFound(new RespuestaDTO
                {
                    Exito = false,
                    Mensaje = resultado.Mensaje
                });
            }

            // HTTP 200: eliminado correctamente
            return Ok(new RespuestaDTO
            {
                Exito = true,
                Mensaje = resultado.Mensaje
            });
        }

    }

}

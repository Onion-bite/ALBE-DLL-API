namespace BecasAPI.DTOs
{
    public class DTOs
    {
        // ── DTO para CREAR una nueva Beca ────────────────────────────────────────
        public class CrearBecaDTO
        {
            public string Nombre { get; set; } = string.Empty;
            public string Carrera { get; set; } = string.Empty;
            public DateTime FechaLimite { get; set; }
            public string Requisitos { get; set; } = string.Empty;
            public string Descripcion { get; set; } = string.Empty;
        }

        // ── DTO para MODIFICAR una Beca existente ────────────────────────────────
        public class ModificarBecaDTO
        {
            public int Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public string Carrera { get; set; } = string.Empty;
            public DateTime FechaLimite { get; set; }
            public string Requisitos { get; set; } = string.Empty;
            public string Descripcion { get; set; } = string.Empty;
        }

        // ── DTO para CREAR un nuevo Usuario ─────────────────────────────────────
        public class CrearUsuarioDTO
        {
            public string Nombre { get; set; } = string.Empty;
            public string Correo { get; set; } = string.Empty;
            public string Carrera { get; set; } = string.Empty;
            public int Edad { get; set; }
            public string Rol { get; set; } = "Usuario";
        }

        // ── DTO para GENERAR una nueva Alerta ────────────────────────────────────
        public class CrearAlertaDTO
        {
            public int UsuarioId { get; set; }
            public int BecaId { get; set; }
            public string Mensaje { get; set; } = string.Empty;
        }

        // ── DTO de RESPUESTA genérica ────────────────────────────────────────────
        public class RespuestaDTO
        {
            public bool Exito { get; set; }
            public string Mensaje { get; set; } = string.Empty;
        }


    }
}

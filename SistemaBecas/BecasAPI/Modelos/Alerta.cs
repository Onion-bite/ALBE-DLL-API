namespace BecasAPI.Modelos
{
    public class Alerta
    {
        // Identificador único de la alerta
        public int Id { get; set; }

        // ID del usuario que recibe la alerta
        public int UsuarioId { get; set; }

        // ID de la beca a la que hace referencia
        public int BecaId { get; set; }

        // Mensaje de la alerta
        public string Mensaje { get; set; } = string.Empty;

        // Fecha en que se generó la alerta
        public DateTime FechaGenerada { get; set; }

        // Indica si el usuario ya la vio
        public bool Leida { get; set; } = false;
    }
}

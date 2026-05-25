namespace BecasAPI.Modelos
{
    public class Usuario
    {
        // Identificador único del usuario
        public int Id { get; set; }

        // Nombre completo del estudiante
        public string Nombre { get; set; } = string.Empty;

        // Correo institucional UPN
        public string Correo { get; set; } = string.Empty;

        // Carrera que estudia
        public string Carrera { get; set; } = string.Empty;

        // Edad del estudiante
        public int Edad { get; set; }

        // Rol: "Admin" o "Usuario"
        public string Rol { get; set; } = "Usuario";
    }
}

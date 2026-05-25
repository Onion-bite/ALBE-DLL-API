namespace BecasAPI.Modelos
{
    public class Beca
    {
        // Identificador 
        public int Id { get; set; }

        // Nombre de la beca
        public string Nombre { get; set; } = string.Empty;

        // Carrera a la que va dirigida
        public string Carrera { get; set; } = string.Empty;

        // Fecha límite de postulación
        public DateTime FechaLimite { get; set; }

        // Requisitos para postular
        public string Requisitos { get; set; } = string.Empty;

        // Descripción general de la beca
        public string Descripcion { get; set; } = string.Empty;
    }
}

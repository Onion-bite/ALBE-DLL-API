using System.Text.Json;

namespace BecasAPI.Helpers
{
    public class JsonHelper
    {
        
        private static readonly JsonSerializerOptions _opciones = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

     
        public static T[] Leer<T>(string rutaArchivo)
        {
            if (!File.Exists(rutaArchivo))
                return new T[0];

            string contenido = File.ReadAllText(rutaArchivo);

            if (string.IsNullOrWhiteSpace(contenido))
                return new T[0];

            return JsonSerializer.Deserialize<T[]>(contenido, _opciones)
                   ?? new T[0];
        }

       
        public static void Escribir<T>(string rutaArchivo, T[] datos)
        {
            string contenido = JsonSerializer.Serialize(datos, _opciones);
            File.WriteAllText(rutaArchivo, contenido);
        }
    }
}

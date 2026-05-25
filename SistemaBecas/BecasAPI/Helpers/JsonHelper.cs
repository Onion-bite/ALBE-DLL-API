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

     
        public static List<T> Leer<T>(string rutaArchivo)
        {
          
            if (!File.Exists(rutaArchivo))
                return new List<T>();

            
            string contenido = File.ReadAllText(rutaArchivo);

         
            if (string.IsNullOrWhiteSpace(contenido))
                return new List<T>();

            return JsonSerializer.Deserialize<List<T>>(contenido, _opciones)
                   ?? new List<T>();
        }

       
        public static void Escribir<T>(string rutaArchivo, List<T> datos)
        {
            
            string contenido = JsonSerializer.Serialize(datos, _opciones);

            
            File.WriteAllText(rutaArchivo, contenido);
        }
    }
}

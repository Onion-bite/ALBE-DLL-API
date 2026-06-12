using BecasAPI.Helpers;
using BecasAPI.Modelos;
using DLL__ESTRUCTURAS_VERSION_2;

namespace BecasAPI.Servicios
{
    public class AlertaService
    {
        // Ruta del archivo donde se guardan las alertas
        private readonly string _ruta = "alertas.json";

        // Estructura principal: Cola FIFO para gestión de alertas
        private CustomQueue<Alerta> _cola;

        public AlertaService()
        {
            // Inicializar la cola
            _cola = new CustomQueue<Alerta>();

            // Cargar datos existentes del archivo .json
            CargarDatos();
        }

        // Carga los datos del archivo .json hacia la cola
        private void CargarDatos()
        {
            // Limpiar antes de cargar
            _cola.Limpiar();

            // Leer alertas del archivo
            List<Alerta> alertas = JsonHelper.Leer<Alerta>(_ruta);

            // Encolar cada alerta en orden
            foreach (Alerta alerta in alertas)
                _cola.Encolar(alerta);
        }
        // Guarda el estado actual de la cola en el archivo .json
        private void GuardarDatos()
        {
            List<Alerta> alertas = _cola.ListarAlertas();
            JsonHelper.Escribir<Alerta>(_ruta, alertas);
        }

        // SISTEMA: Genera una nueva alerta para un usuario
        public RespuestaOperacion GenerarAlerta(int usuarioId, int becaId, string mensaje)
        {
            // Leer alertas actuales para generar Id
            List<Alerta> actuales = _cola.ListarAlertas();

            // Crear la nueva alerta
            Alerta alerta = new Alerta
            {
                Id = actuales.Count > 0
                    ? actuales.Max(a => a.Id) + 1
                    : 1,
                UsuarioId = usuarioId,
                BecaId = becaId,
                Mensaje = mensaje,
                FechaGenerada = DateTime.Now,
                Leida = false
            };                  

            // Encolar la alerta (FIFO)
            _cola.Encolar(alerta);

            // Persistir cambios
            GuardarDatos();

            return new RespuestaOperacion(true, $"Alerta generada para usuario {usuarioId}");
        }
        // USUARIO: Ve sus alertas pendientes sin desencolar
        public List<Alerta> VerAlertas(int usuarioId)
        {
            // Listar todas y filtrar por usuario
            return _cola.ListarAlertas()
                .Where(a => a.UsuarioId == usuarioId && !a.Leida)
                .ToList();
        }

        // USUARIO: Marca una alerta como leída
        public RespuestaOperacion MarcarLeida(int alertaId)
        {
            // Obtener todas las alertas
            List<Alerta> todas = _cola.ListarAlertas();

            // Buscar la alerta específica
            Alerta? encontrada = todas.FirstOrDefault(a => a.Id == alertaId);

            if (encontrada == null)
                return new RespuestaOperacion(false, $"No existe alerta con Id {alertaId}");

            // Marcar como leída
            encontrada.Leida = true;

            // Reconstruir la cola con el cambio
            _cola.Limpiar();
            foreach (Alerta a in todas)
                _cola.Encolar(a);

            // Persistir cambios
            GuardarDatos();

            return new RespuestaOperacion(true, $"Alerta {alertaId} marcada como leída");
        }
        // SISTEMA: Genera alertas automáticas para becas próximas a vencer
        public void GenerarAlertasAutomaticas(List<Alerta> alertasNuevas)
        {
            foreach (Alerta alerta in alertasNuevas)
            {
                // Solo encolar si no existe ya una alerta igual
                List<Alerta> actuales = _cola.ListarAlertas();
                bool yaExiste = actuales.Any(
                    a => a.UsuarioId == alerta.UsuarioId && a.BecaId == alerta.BecaId && !a.Leida
                );

                if (!yaExiste)
                {
                    alerta.Id = actuales.Count > 0
                        ? actuales.Max(a => a.Id) + 1
                        : 1;
                    _cola.Encolar(alerta);
                }
            }

            // Persistir todos los cambios
            GuardarDatos();
        }

        // Lista todas las alertas (para el admin)
        public List<Alerta> ListarTodas()
        {
            return _cola.ListarAlertas();
        }
    }
}

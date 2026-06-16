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
            Alerta[] alertas = JsonHelper.Leer<Alerta>(_ruta);

            // Encolar cada alerta en orden
            for (int i = 0; i < alertas.Length; i++)
                _cola.Encolar(alertas[i]);
        }
        // Guarda el estado actual de la cola en el archivo .json
        private void GuardarDatos()
        {
            Alerta[] alertas = _cola.ListarAlertas();
            JsonHelper.Escribir<Alerta>(_ruta, alertas);
        }

        // SISTEMA: Genera una nueva alerta para un usuario
        public RespuestaOperacion GenerarAlerta(int usuarioId, int becaId, string mensaje)
        {
            // Leer alertas actuales para generar Id
            Alerta[] actuales = _cola.ListarAlertas();

            // Crear la nueva alerta
            Alerta alerta = new Alerta
            {
                Id = (actuales.Length > 0) ? (GetMaxId(actuales) + 1) : 1,
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
        public Alerta[] VerAlertas(int usuarioId)
        {
            // Listar todas y filtrar por usuario
            Alerta[] todas = _cola.ListarAlertas();

            int count = 0;
            for (int i = 0; i < todas.Length; i++)
            {
                Alerta a = todas[i];
                if (a != null && a.UsuarioId == usuarioId && !a.Leida)
                    count++;
            }

            Alerta[] resultado = new Alerta[count];
            int idx = 0;
            for (int i = 0; i < todas.Length; i++)
            {
                Alerta a = todas[i];
                if (a != null && a.UsuarioId == usuarioId && !a.Leida)
                    resultado[idx++] = a;
            }

            return resultado;
        }

        // USUARIO: Marca una alerta como leída
        public RespuestaOperacion MarcarLeida(int alertaId)
        {
            // Obtener todas las alertas
            Alerta[] todas = _cola.ListarAlertas();

            // Buscar la alerta específica
            Alerta? encontrada = null;
            for (int i = 0; i < todas.Length; i++)
            {
                if (todas[i] != null && todas[i].Id == alertaId)
                {
                    encontrada = todas[i];
                    break;
                }
            }

            if (encontrada == null)
                return new RespuestaOperacion(false, $"No existe alerta con Id {alertaId}");

            // Marcar como leída
            encontrada.Leida = true;

            // Reconstruir la cola con el cambio
            _cola.Limpiar();
            for (int i = 0; i < todas.Length; i++)
                _cola.Encolar(todas[i]);

            // Persistir cambios
            GuardarDatos();

            return new RespuestaOperacion(true, $"Alerta {alertaId} marcada como leída");
        }
        // SISTEMA: Genera alertas automáticas para becas próximas a vencer
        public void GenerarAlertasAutomaticas(Alerta[] alertasNuevas)
        {
            for (int i = 0; i < alertasNuevas.Length; i++)
            {
                Alerta alerta = alertasNuevas[i];
                // Solo encolar si no existe ya una alerta igual
                Alerta[] actuales = _cola.ListarAlertas();
                bool yaExiste = false;
                for (int j = 0; j < actuales.Length; j++)
                {
                    Alerta a = actuales[j];
                    if (a != null && a.UsuarioId == alerta.UsuarioId && a.BecaId == alerta.BecaId && !a.Leida)
                    {
                        yaExiste = true;
                        break;
                    }
                }

                if (!yaExiste)
                {
                    alerta.Id = (actuales.Length > 0) ? (GetMaxId(actuales) + 1) : 1;
                    _cola.Encolar(alerta);
                }
            }

            // Persistir todos los cambios
            GuardarDatos();
        }

        // Lista todas las alertas (para el admin)
        public Alerta[] ListarTodas()
        {
            return _cola.ListarAlertas();
        }

        private int GetMaxId(Alerta[] arr)
        {
            int max = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != null && arr[i].Id > max)
                    max = arr[i].Id;
            }
            return max;
        }
    }
}

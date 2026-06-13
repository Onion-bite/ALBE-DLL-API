using BecasAPI.Helpers;
using BecasAPI.Modelos;
using DLL__ESTRUCTURAS_VERSION_2;

namespace BecasAPI.Servicios
{
    public class BecaServicio
    {
        // Ruta del archivo becas
        private readonly string _ruta = "becas.json";

        // Estructura principal: Lista Doble para CRUD completo
        private LstDoble<Beca> _listaBecas;

        // Estructura de índice: ABB para búsqueda optimizada
        private ABB<Beca> _arbol;

        public BecaServicio()
        {
            // Inicializar la lista doble
            _listaBecas = new LstDoble<Beca>();

            // Inicializar el ABB con comparador por Id
            _arbol = new ABB<Beca>((a, b) => a.Id.CompareTo(b.Id));

            // Cargar datos existentes del archivo .json
            CargarDatos();
        }
        // Carga los datos del archivo .json hacia las estructuras
        private void CargarDatos()
        {
            // Limpiar estructuras antes de cargar
            _listaBecas.Limpiar();
            _arbol.Limpiar();

            // Leer becas del archivo
            List<Beca> becas = JsonHelper.Leer<Beca>(_ruta);

            // Insertar cada beca en ambas estructuras
            foreach (Beca beca in becas)
            {
                _listaBecas.InsertarAlFinal(beca);
                _arbol.Insertar(beca);
            }
        }
        // Guarda el estado actual de la lista doble en el archivo .json
        private void GuardarDatos()
        {
            List<Beca> becas = _listaBecas.ListarTodas();
            JsonHelper.Escribir<Beca>(_ruta, becas);
        }

        // ADMIN: Inserta una nueva beca
        public RespuestaOperacion InsertarBeca(Beca beca)
        {
            // Generar Id automático
            List<Beca> actuales = _listaBecas.ListarTodas();
            beca.Id = actuales.Count > 0
                ? actuales.Max(b => b.Id) + 1
                : 1;

            // Insertar en ambas estructuras
            _listaBecas.InsertarAlFinal(beca);
            _arbol.Insertar(beca);

            // Persistir cambios
            GuardarDatos();

            return new RespuestaOperacion(true, $"Beca '{beca.Nombre}' insertada con Id {beca.Id}");
        }
        // ADMIN: Elimina una beca por Id
        public RespuestaOperacion EliminarBeca(int id)
        {
            // Buscar la beca en la lista doble usando lambda
            Beca? encontrada = _listaBecas.Buscar(b => b.Id == id);

            if (encontrada == null)
                return new RespuestaOperacion(false, $"No existe beca con Id {id}");

            // Eliminar de la lista doble usando lambda
            _listaBecas.Eliminar(b => b.Id == id);

            // Eliminar del ABB usando el objeto directamente
            _arbol.Eliminar(encontrada);

            // Persistir cambios
            GuardarDatos();

            return new RespuestaOperacion(true, $"Beca '{encontrada.Nombre}' eliminada correctamente");
        }
        // ADMIN: Modifica una beca existente
        public RespuestaOperacion ModificarBeca(Beca becaModificada)
        {
            // Verificar que existe usando lambda
            Beca? encontrada = _listaBecas.Buscar(b => b.Id == becaModificada.Id);

            if (encontrada == null)
                return new RespuestaOperacion(false, $"No existe beca con Id {becaModificada.Id}");

            // Modificar en la lista doble usando lambda
            _listaBecas.Modificar(b => b.Id == becaModificada.Id, becaModificada);

            // Reconstruir el ABB con los datos actualizados
            _arbol.Limpiar();
            foreach (Beca b in _listaBecas.ListarTodas())
                _arbol.Insertar(b);

            // Persistir cambios
            GuardarDatos();

            return new RespuestaOperacion(true, $"Beca '{becaModificada.Nombre}' modificada correctamente");
        }

        // ADMIN: Lista todas las becas
        public List<Beca> ListarBecas()
        {
            return _listaBecas.ListarTodas();
        }

        // USUARIO: Busca una beca por Id usando el ABB
        public Beca? BuscarBeca(int id)
        {
            // Buscar en el ABB usando lambda
            return _arbol.Buscar(b => b.Id == id);
        }

        // USUARIO: Filtra becas por carrera
        public List<Beca> FiltrarPorCarrera(string carrera)
        {
            List<Beca> todas = _listaBecas.ListarTodas();

            // Usamos .Contains() para buscar el texto dentro de la cadena larga de carreras
            return todas
                .Where(b => b.Carrera.ToLower().Contains(carrera.ToLower().Trim()))
                .ToList();
        }

        // SISTEMA: Obtiene becas próximas a vencer
        public List<Beca> ObtenerProximasAVencer(int dias)
        {
            // El ABB recibe los días y una función que extrae la fecha de cada beca
            return _arbol.ObtenerProximasAVencer(dias, b => b.FechaLimite);
        }

    }
    public class RespuestaOperacion
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }

        public RespuestaOperacion(bool exito, string mensaje)
        {
            Exito = exito;
            Mensaje = mensaje;
        }
    }
}

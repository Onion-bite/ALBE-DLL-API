using BecasAPI.Helpers;
using BecasAPI.Modelos;
using DLL__ESTRUCTURAS_VERSION_2;

namespace BecasAPI.Servicios
{
    public class UsuarioServicio
    {
        // Ruta del archivo donde se persisten los usuarios
        private readonly string _ruta = "usuarios.json";

        // Estructura principal: Lista Simple para registro de usuarios
        private LstSimple<Usuario> _listaUsuarios;

        public UsuarioServicio()
        {
            // Inicializar la lista simple
            _listaUsuarios = new LstSimple<Usuario>();

            // Cargar datos existentes del archivo .json
            CargarDatos();
        }
        // Carga los datos del archivo .json hacia la estructura
        private void CargarDatos()
        {
            // Limpiar antes de cargar
            _listaUsuarios.Limpiar();

            // Leer usuarios del archivo
            Usuario[] usuarios = JsonHelper.Leer<Usuario>(_ruta);

            // Insertar cada usuario en la lista
            for (int i = 0; i < usuarios.Length; i++)
                _listaUsuarios.InsertarAlFinal(usuarios[i]);
        }
        // Guarda el estado actual de la lista en el archivo .json
        private void GuardarDatos()
        {
            Usuario[] usuarios = _listaUsuarios.ListarTodos();
            JsonHelper.Escribir<Usuario>(_ruta, usuarios);
        }

        // Registra un nuevo usuario
        public RespuestaOperacion InsertarUsuario(Usuario usuario)
        {
            // Verificar que el correo no esté registrado
            Usuario? existente = _listaUsuarios.Buscar(u => u.Correo == usuario.Correo);

            if (existente != null)
                return new RespuestaOperacion(false, $"Ya existe un usuario con el correo {usuario.Correo}");

            // Generar Id automático
            Usuario[] actuales = _listaUsuarios.ListarTodos();
            if (actuales.Length > 0)
            {
                int max = 0;
                for (int i = 0; i < actuales.Length; i++)
                {
                    if (actuales[i] != null && actuales[i].Id > max)
                        max = actuales[i].Id;
                }

                usuario.Id = max + 1;
            }
            else
            {
                usuario.Id = 1;
            }

            // Insertar en la lista
            _listaUsuarios.InsertarAlFinal(usuario);

            // Persistir cambios
            GuardarDatos();

            return new RespuestaOperacion(true, $"Usuario '{usuario.Nombre}' registrado con Id {usuario.Id}");
        }
        // Elimina un usuario por Id
        public RespuestaOperacion EliminarUsuario(int id)
        {
            // Verificar que existe
            Usuario? encontrado = _listaUsuarios.Buscar(u => u.Id == id);

            if (encontrado == null)
                return new RespuestaOperacion(false, $"No existe usuario con Id {id}");

            // Eliminar de la lista
            _listaUsuarios.Eliminar(u => u.Id == id);

            // Persistir cambios
            GuardarDatos();

            return new RespuestaOperacion(true, $"Usuario '{encontrado.Nombre}' eliminado correctamente");
        }

        // Lista todos los usuarios
        public Usuario[] ListarUsuarios()
        {
            return _listaUsuarios.ListarTodos();
        }

        // Busca un usuario por Id
        public Usuario? BuscarUsuario(int id)
        {
            return _listaUsuarios.Buscar(u => u.Id == id);
        }

        // Busca un usuario por correo
        public Usuario? BuscarPorCorreo(string correo)
        {
            return _listaUsuarios.Buscar(u => u.Correo == correo);
        }

    }
}

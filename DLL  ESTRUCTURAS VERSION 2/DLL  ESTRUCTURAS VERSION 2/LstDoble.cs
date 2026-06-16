using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL__ESTRUCTURAS_VERSION_2
{
    public class LstDoble<T>
    {
        public class Nodo
        {
            public T Dato { get; set; }

            public Nodo Atras { get; set; }

            public Nodo Adelante { get; set; }

            public Nodo(T dato)
            {
                Dato = dato;

                Atras = null;

                Adelante = null;
            }
        }
        private Nodo cabeza;

        private Nodo cola;

        private int cantidad;

        public int Cantidad
        {
            get
            {
                return cantidad;
            }
        }
        public bool EstaVacia
        {
            get
            {
                return cabeza == null;
            }
        }
        public T Ultimo
        {
            get
            {
                if (cola != null)
                {
                    return cola.Dato;
                }

                return default;
            }
        }
        public void InsertarAlFinal(T dato)
        {
            Nodo nuevo = new Nodo(dato);

            if (EstaVacia)
            {
                cabeza = nuevo;

                cola = nuevo;
            }
            else
            {
                nuevo.Atras = cola;

                cola.Adelante = nuevo;

                cola = nuevo;
            }

            cantidad++;
        }
        public T Buscar(Func<T, bool> condicion)
        {
            Nodo actual = cabeza;

            while (actual != null)
            {
                if (condicion(actual.Dato))
                {
                    return actual.Dato;
                }

                actual = actual.Adelante;
            }

            return default;
        }
        public bool Eliminar(Func<T, bool> condicion)
        {
            Nodo actual = cabeza;

            while (actual != null)
            {
                if (condicion(actual.Dato))
                {
                    // SI ES EL PRIMERO
                    if (actual == cabeza)
                    {
                        cabeza = cabeza.Adelante;

                        if (cabeza != null)
                        {
                            cabeza.Atras = null;
                        }
                    }

                    // SI ES EL ULTIMO
                    else if (actual == cola)
                    {
                        cola = cola.Atras;

                        if (cola != null)
                        {
                            cola.Adelante = null;
                        }
                    }

                    // SI ESTA EN MEDIO
                    else
                    {
                        actual.Atras.Adelante = actual.Adelante;

                        actual.Adelante.Atras = actual.Atras;
                    }

                    cantidad--;

                    return true;
                }

                actual = actual.Adelante;
            }

            return false;
        }
        public bool Modificar(Func<T, bool> condicion,T nuevoDato)
        {
            Nodo actual = cabeza;

            while (actual != null)
            {
                if (condicion(actual.Dato))
                {
                    actual.Dato = nuevoDato;

                    return true;
                }

                actual = actual.Adelante;
            }

            return false;
        }
        public T[] ListarTodas()
        {
            T[] arreglo = new T[cantidad];

            Nodo actual = cabeza;

            int idx = 0;

            while (actual != null)
            {
                arreglo[idx++] = actual.Dato;

                actual = actual.Adelante;
            }

            return arreglo;
        }
        public T[] ListarInverso()
        {
            T[] arreglo = new T[cantidad];

            Nodo actual = cola;

            int idx = 0;

            while (actual != null)
            {
                arreglo[idx++] = actual.Dato;

                actual = actual.Atras;
            }

            return arreglo;
        }
        public T EliminarPrimero()
        {
            if (EstaVacia)
            {
                return default;
            }

            T dato = cabeza.Dato;

            if (cabeza == cola)
            {
                cabeza = null;

                cola = null;
            }
            else
            {
                cabeza = cabeza.Adelante;

                cabeza.Atras = null;
            }

            cantidad--;

            return dato;
        }
        public void Limpiar()
        {
            cabeza = null;

            cola = null;

            cantidad = 0;
        }
    }
}

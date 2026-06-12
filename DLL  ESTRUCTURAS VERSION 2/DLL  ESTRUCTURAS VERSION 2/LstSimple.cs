using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL__ESTRUCTURAS_VERSION_2
{
    public class Nodo<T>
    {
        public T Dato { get; set; }

        public Nodo<T> Siguiente { get; set; }

        public Nodo(T dato)
        {
            Dato = dato;

            Siguiente = null;
        }
    }
    public class LstSimple<T>
    {
        private Nodo<T> primero;

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
                return primero == null;
            }

        }
        public void InsertarAlFinal(T dato)
        {
            Nodo<T> nuevo =
                new Nodo<T>(dato);

            if (EstaVacia)
            {
                primero = nuevo;
            }
            else
            {
                Nodo<T> actual = primero;

                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }

                actual.Siguiente = nuevo;
            }

            cantidad++;
        }
        public T Buscar(Func<T, bool> condicion)
        {
            Nodo<T> actual = primero;

            while (actual != null)
            {
                if (condicion(actual.Dato))
                {
                    return actual.Dato;
                }

                actual = actual.Siguiente;
            }

            return default;
        }
        public bool Eliminar(Func<T, bool> condicion)
        {
            if (EstaVacia)
            {
                return false;
            }

            if (condicion(primero.Dato))
            {
                primero = primero.Siguiente;

                cantidad--;

                return true;
            }

            Nodo<T> actual = primero;

            while (actual.Siguiente != null)
            {
                if (condicion(actual.Siguiente.Dato))
                {
                    actual.Siguiente =actual.Siguiente.Siguiente;

                    cantidad--;

                    return true;
                }

                actual = actual.Siguiente;
            }

            return false;
        }
        public List<T> ListarTodos()
        {
            List<T> lista =
                new List<T>();

            Nodo<T> actual = primero;

            while (actual != null)
            {
                lista.Add(actual.Dato);

                actual = actual.Siguiente;
            }

            return lista;
        }

        public void Limpiar()
        {
            primero = null;

            cantidad = 0;
        }
    }
}

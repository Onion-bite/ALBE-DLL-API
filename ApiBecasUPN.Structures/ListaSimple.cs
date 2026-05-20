using System;
using System.Collections.Generic;
using System.Text;

namespace ApiBecasUPN.Structures
{
    public class Nodo<T>
    {
        public T Dato { get; set; }
        public Nodo<T> Siguiente { get; set; }

        public Nodo(T valor)
        {
            Dato = valor;
            Siguiente = null;
        }
    }
    internal class ListaSimple<T> : IEnumerable<T>
    {
        private Nodo<T> primero;
        private int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
        }

        public bool EstaVacia
        {
            get { return primero == null; }
        }

        public void InsertarInicio(T dato)
        {
            Nodo<T> nuevo = new Nodo<T>(dato);

            nuevo.Siguiente = primero;
            primero = nuevo;
            cantidad++;
        }

        public void InsertarFinal(T dato)
        {
            Nodo<T> nuevo = new Nodo<T>(dato);

            if (primero == null)
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

            return default(T);
        }

        public IEnumerable<T> BuscarTodos(Func<T, bool> condicion)
        {
            List<T> resultados = new List<T>();

            Nodo<T> actual = primero;

            while (actual != null)
            {
                if (condicion(actual.Dato))
                {
                    resultados.Add(actual.Dato);
                }

                actual = actual.Siguiente;
            }

            return resultados;
        }

        public bool Eliminar(Func<T, bool> condicion)
        {
            if (primero == null)
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
                    actual.Siguiente = actual.Siguiente.Siguiente;
                    cantidad--;
                    return true;
                }

                actual = actual.Siguiente;
            }

            return false;
        }

        public bool Actualizar(Func<T, bool> condicion, T nuevoDato)
        {
            Nodo<T> actual = primero;

            while (actual != null)
            {
                if (condicion(actual.Dato))
                {
                    actual.Dato = nuevoDato;
                    return true;
                }

                actual = actual.Siguiente;
            }

            return false;
        }

        public void Limpiar()
        {
            primero = null;
            cantidad = 0;
        }

        public void CargarDesdeColeccion(IEnumerable<T> coleccion)
        {
            Limpiar();

            foreach (T item in coleccion)
            {
                InsertarFinal(item);
            }
        }

        public T[] ConvertirArreglo()
        {
            T[] arreglo = new T[cantidad];

            Nodo<T> actual = primero;

            int i = 0;

            while (actual != null)
            {
                arreglo[i] = actual.Dato;
                i++;
                actual = actual.Siguiente;
            }

            return arreglo;
        }

        public IEnumerator<T> GetEnumerator()
        {
            List<T> lista = new List<T>();

            Nodo<T> actual = primero;

            while (actual != null)
            {
                lista.Add(actual.Dato);
                actual = actual.Siguiente;
            }

            return lista.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

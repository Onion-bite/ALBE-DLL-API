using System;
using System.Collections.Generic;
using System.Text;

namespace ApiBecasUPN.Structures
{
    public class DLLNodo<T>
    {
        public T Dato { get; set; }

        public DLLNodo<T>? Anterior { get; set; }

        public DLLNodo<T>? Siguiente { get; set; }

        public DLLNodo(T dato)
        {
            Dato = dato;

            Anterior = null;

            Siguiente = null;
        }
    }
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        private DLLNodo<T>? _primero;

        private DLLNodo<T>? _ultimo;

        private int _cantidad;

        public int Cantidad => _cantidad;

        public bool EstaVacia => _primero == null;

        public T? Primero =>
            _primero != null
            ? _primero.Dato
            : default;

        public T? Ultimo =>
            _ultimo != null
            ? _ultimo.Dato
            : default;

        // ─────────────────────────────────────────────────────────────────────────
        // INSERTAR
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Inserta al inicio. O(1).
        /// </summary>
        public void InsertarInicio(T dato)
        {
            var nuevo =
                new DLLNodo<T>(dato);

            // Lista vacía
            if (_primero == null)
            {
                _primero = _ultimo = nuevo;
            }
            else
            {
                nuevo.Siguiente = _primero;

                _primero.Anterior = nuevo;

                _primero = nuevo;
            }

            _cantidad++;
        }

        /// <summary>
        /// Inserta al final. O(1).
        /// </summary>
        public void InsertarFinal(T dato)
        {
            var nuevo =
                new DLLNodo<T>(dato);

            // Lista vacía
            if (_ultimo == null)
            {
                _primero = _ultimo = nuevo;
            }
            else
            {
                nuevo.Anterior = _ultimo;

                _ultimo.Siguiente = nuevo;

                _ultimo = nuevo;
            }

            _cantidad++;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // ELIMINAR
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Elimina el primer nodo. O(1).
        /// </summary>
        public T? EliminarPrimero()
        {
            if (_primero == null)
            {
                return default;
            }

            var dato = _primero.Dato;

            // Solo un nodo
            if (_primero == _ultimo)
            {
                _primero = _ultimo = null;
            }
            else
            {
                _primero = _primero.Siguiente;

                _primero!.Anterior = null;
            }

            _cantidad--;

            return dato;
        }

        /// <summary>
        /// Elimina el último nodo. O(1).
        /// </summary>
        public T? EliminarUltimo()
        {
            if (_ultimo == null)
            {
                return default;
            }

            var dato = _ultimo.Dato;

            // Solo un nodo
            if (_primero == _ultimo)
            {
                _primero = _ultimo = null;
            }
            else
            {
                _ultimo = _ultimo.Anterior;

                _ultimo!.Siguiente = null;
            }

            _cantidad--;

            return dato;
        }

        /// <summary>
        /// Elimina el primer nodo que cumpla la condición. O(n).
        /// </summary>
        public bool Eliminar(Func<T, bool> condicion)
        {
            var actual = _primero;

            while (actual != null)
            {
                if (condicion(actual.Dato))
                {
                    // Reconectar nodos
                    if (actual.Anterior != null)
                    {
                        actual.Anterior.Siguiente =
                            actual.Siguiente;
                    }
                    else
                    {
                        _primero = actual.Siguiente;
                    }

                    if (actual.Siguiente != null)
                    {
                        actual.Siguiente.Anterior =
                            actual.Anterior;
                    }
                    else
                    {
                        _ultimo = actual.Anterior;
                    }

                    _cantidad--;

                    return true;
                }

                actual = actual.Siguiente;
            }

            return false;
        }

        // ─────────────────────────────────────────────────────────────────────────
        // BUSCAR
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Busca el primer elemento que cumpla la condición. O(n).
        /// </summary>
        public T? Buscar(Func<T, bool> condicion)
        {
            var actual = _primero;

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

        /// <summary>
        /// Recorre desde el final hasta el inicio.
        /// </summary>
        public IEnumerable<T> RecorrerReversa()
        {
            var actual = _ultimo;

            while (actual != null)
            {
                yield return actual.Dato;

                actual = actual.Anterior;
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // UTILIDADES
        // ─────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Limpia toda la lista.
        /// </summary>
        public void Limpiar()
        {
            _primero = null;

            _ultimo = null;

            _cantidad = 0;
        }

        /// <summary>
        /// Carga datos desde una colección.
        /// </summary>
        public void CargarDesdeColeccion(
            IEnumerable<T> coleccion)
        {
            Limpiar();

            foreach (var item in coleccion)
            {
                InsertarFinal(item);
            }
        }

        // ─────────────────────────────────────────────────────────────────────────
        // IENUMERABLE
        // ─────────────────────────────────────────────────────────────────────────

        public IEnumerator<T> GetEnumerator()
        {
            var actual = _primero;

            while (actual != null)
            {
                yield return actual.Dato;

                actual = actual.Siguiente;
            }
        }

        System.Collections.IEnumerator
            System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }



    }
}

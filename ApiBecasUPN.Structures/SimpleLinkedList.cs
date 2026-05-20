using System;
using System.Collections.Generic;
using System.Text;

namespace ApiBecasUPN.Structures
{
    public class SLLNodo<T>
    {
        public T Dato { get; set; }

        public SLLNodo<T>? Siguiente { get; set; }

        public SLLNodo(T dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }
    public class SimpleLinkedList<T> : IEnumerable<T> 
    {
        private SLLNodo<T>? _primero;

        private int _cantidad;

        public int Cantidad => _cantidad;

        public bool EstaVacia => _primero == null;

        
        public void InsertarInicio(T dato)
        {
            var nuevo =
                new SLLNodo<T>(dato);

            nuevo.Siguiente = _primero;

            _primero = nuevo;

            _cantidad++;
        }

        
        public void InsertarFinal(T dato)
        {
            var nuevo =
                new SLLNodo<T>(dato);

            
            if (_primero == null)
            {
                _primero = nuevo;
            }
            else
            {
                var actual = _primero;

                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }

                actual.Siguiente = nuevo;
            }

            _cantidad++;
        }


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

        
        public IEnumerable<T> BuscarTodos(
            Func<T, bool> condicion)
        {
            var resultados =
                new List<T>();

            var actual = _primero;

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
            if (_primero == null)
            {
                return false;
            }

            
            if (condicion(_primero.Dato))
            {
                _primero = _primero.Siguiente;

                _cantidad--;

                return true;
            }

            var actual = _primero;

            while (actual.Siguiente != null)
            {
                if (condicion(actual.Siguiente.Dato))
                {
                    actual.Siguiente =
                        actual.Siguiente.Siguiente;

                    _cantidad--;

                    return true;
                }

                actual = actual.Siguiente;
            }

            return false;
        }

        
        public bool Actualizar(
            Func<T, bool> condicion,
            T nuevoDato)
        {
            var actual = _primero;

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
            _primero = null;

            _cantidad = 0;
        }

        
        public void CargarDesdeColeccion(
            IEnumerable<T> coleccion)
        {
            Limpiar();

            foreach (var item in coleccion)
            {
                InsertarFinal(item);
            }
        }

        
        public T[] ConvertirArreglo()
        {
            var arreglo =
                new T[_cantidad];

            var actual = _primero;

            int i = 0;

            while (actual != null)
            {
                arreglo[i++] = actual.Dato;

                actual = actual.Siguiente;
            }

            return arreglo;
        }

       

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



using System;
using System.Collections.Generic;
using System.Text;

namespace ApiBecasUPN.Structures
{

    public class CustomQueue<T> : IEnumerable<T>
    {
        private readonly DoublyLinkedList<T> lista = new();

        public int Cantidad => lista.Cantidad;

        public bool EstaVacia => lista.EstaVacia;

        
        public void Encolar(T item)
        {
            lista.InsertarFinal(item);
        }

        
        public T Desencolar()
        {
            if (EstaVacia)
            {
              throw new InvalidOperationException("La cola está vacía.");
            }

            var valor = lista.EliminarPrimero();

            return valor;
        }

        public T VerPrimero()
        {
            if (EstaVacia)
            {
                throw new InvalidOperationException("La cola está vacía.");
            }

            return lista.Primero;
        }

        public bool IntentarDesencolar(out T item)
        {
            if (EstaVacia)
            {
                item = default;

                return false;
            }

            item = Desencolar();

            return true;
        }

        
        public bool IntentarVerPrimero(out T item)
        {
            if (EstaVacia)
            {
                item = default;

                return false;
            }

            item = VerPrimero();

            return true;
        }


        public void Limpiar()
        {
            lista.Limpiar();
        }

        

        public IEnumerator<T> GetEnumerator()
        {
            return lista.GetEnumerator();
        }

        System.Collections.IEnumerator
            System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    
    public class CustomStack<T> : IEnumerable<T>
    {
        private SLLNodo<T> tope;

        private int cantidad;

        public int Cantidad => cantidad;

        public bool EstaVacia => tope == null;

        
        public void Push(T item)
        {
            var nuevo = new SLLNodo<T>(item);

            nuevo.Siguiente = tope;

            tope = nuevo;

            cantidad++;
        }

        
        public T Pop()
        {
            if (EstaVacia)
            {
                throw new InvalidOperationException("La pila está vacía.");
            }

            var valor = tope.Dato;

            tope = tope.Siguiente;

            cantidad--;

            return valor;
        }

        
        public T Peek()
        {
            if (EstaVacia)
            {
                throw new InvalidOperationException("La pila está vacía.");
            }

            return tope.Dato;
        }

        
        public bool IntentarPop(out T item)
        {
            if (EstaVacia)
            {
                item = default;

                return false;
            }

            item = Pop();

            return true;
        }

        
        public bool IntentarPeek(out T item)
        {
            if (EstaVacia)
            {
                item = default;

                return false;
            }

            item = Peek();

            return true;
        }

      

        public void Limpiar()
        {
            tope = null;

            cantidad = 0;
        }

        
        public void CargarDesdeColeccion(IEnumerable<T> elementos)
        {
            Limpiar();

            foreach (var item in elementos)
            {
                Push(item);
            }
        }

        
        public IEnumerator<T> GetEnumerator()
        {
            var actual = tope;

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

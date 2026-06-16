using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL__ESTRUCTURAS_VERSION_2
{

    public class CustomQueue<T> 
    {
        private readonly LstDoble<T> lista = new LstDoble<T>();
        public int Cantidad
        {
            get
            {
                return lista.Cantidad;
            }
        }
        public bool EstaVacia
        {
            get
            {
                return lista.EstaVacia;
            }
        }
        public void Encolar(T dato)
        {
            lista.InsertarAlFinal(dato);
        }
        public T Desencolar()
        {
            if (EstaVacia)
            {
                throw new InvalidOperationException("La cola está vacía.");
            }

            return lista.EliminarPrimero();
        }
        public T VerFrente()
        {
            if (EstaVacia)
            {
                throw new InvalidOperationException("La cola está vacía.");
            }

            return lista.Ultimo;

        }
        public T[] ListarAlertas()
        {
            return lista.ListarTodas();
        }
        public void Limpiar()
        {
            lista.Limpiar();
        }
        

    }

    public class CustomStack<T> 
    {
        public class Nodo
        {
            public T Dato { get; set; }

            public Nodo Siguiente { get; set; }

            public Nodo(T dato)
            {
                Dato = dato;

                Siguiente = null;
            }
        }

        private Nodo tope;

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
                return tope == null;
            }
        }
        public void Push(T dato)
        {
            Nodo nuevo = new Nodo(dato);

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

            T valor = tope.Dato;

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

        public void Limpiar()
        {
            tope = null;

            cantidad = 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL__ESTRUCTURAS_VERSION_2
{
    public class ABB<T>
    {
        public class Nodo
        {
            public T Dato { get; set; }

            public Nodo Izquierda { get; set; }

            public Nodo Derecha { get; set; }

            public Nodo(T dato)
            {
                Dato = dato;

                Izquierda = null;

                Derecha = null;
            }
        }
        private Nodo raiz;

        private int cantidad;

        private readonly Func<T, T, int> comparar;
        public ABB(Func<T, T, int> comparador)
        {
            comparar = comparador;
        }

        public int Cantidad
        {
            get
            {
                return cantidad;
            }
        }

        public bool EstaVacio
        {
            get
            {
                return raiz == null;
            }
        }
        public void Insertar(T dato)
        {
            raiz = InsertarRecursivo(raiz, dato);

            cantidad++;
        }

        private Nodo InsertarRecursivo(Nodo nodo,T dato)
        {
            if (nodo == null)
            {
                return new Nodo(dato);
            }

            int comparacion = comparar(dato, nodo.Dato);

            if (comparacion < 0)
            {
                nodo.Izquierda = InsertarRecursivo(nodo.Izquierda,dato);
            }

            else if (comparacion > 0)
            {
                nodo.Derecha =InsertarRecursivo(nodo.Derecha,dato);
            }

            else
            {
                nodo.Dato = dato;
            }

            return nodo;
        }
        public T Buscar(Func<T, bool> condicion)
        {
            return BuscarRecursivo(raiz,condicion);
        }

        private T BuscarRecursivo(Nodo nodo,Func<T, bool> condicion)
        {
            if (nodo == null)
            {
                return default;
            }

            if (condicion(nodo.Dato))
            {
                return nodo.Dato;
            }

            T izquierda =BuscarRecursivo(nodo.Izquierda,condicion);

            if (izquierda != null)
            {
                return izquierda;
            }

            return BuscarRecursivo(nodo.Derecha,condicion);
        }
        public List<T> BuscarRango(Func<T, bool> condicion)
        {
            List<T> resultados =new List<T>();

            BuscarRangoRecursivo(raiz, condicion,resultados);

            return resultados;
        }

        private void BuscarRangoRecursivo(Nodo nodo,Func<T, bool> condicion,List<T> resultados)
        {
            if (nodo == null)
            {
                return;
            }

            BuscarRangoRecursivo(nodo.Izquierda,condicion,resultados);

            if (condicion(nodo.Dato))
            {
                resultados.Add(nodo.Dato);
            }

            BuscarRangoRecursivo(nodo.Derecha,condicion,resultados);
        }
        public void Eliminar(T dato)
        {
            raiz =EliminarRecursivo(raiz,dato);
        }

        private Nodo EliminarRecursivo(Nodo nodo,T dato)
        {
            if (nodo == null)
            {
                return null;
            }

            int comparacion =comparar(dato, nodo.Dato);

            if (comparacion < 0)
            {
                nodo.Izquierda =EliminarRecursivo(nodo.Izquierda,dato);
            }

            else if (comparacion > 0)
            {
                nodo.Derecha =EliminarRecursivo(nodo.Derecha,dato);
            }

            else
            {
                cantidad--;

                // SIN HIJO IZQUIERDO
                if (nodo.Izquierda == null)
                {
                    return nodo.Derecha;
                }

                // SIN HIJO DERECHO
                if (nodo.Derecha == null)
                {
                    return nodo.Izquierda;
                }

                // DOS HIJOS
                Nodo menor =ObtenerNodoMenor(nodo.Derecha);

                nodo.Dato = menor.Dato;

                nodo.Derecha =EliminarRecursivo(nodo.Derecha,menor.Dato);
            }

            return nodo;
        }

        private Nodo ObtenerNodoMenor(Nodo nodo)
        {
            while (nodo.Izquierda != null)
            {
                nodo = nodo.Izquierda;
            }

            return nodo;
        }
        public List<T> RecorridoInOrder()
        {
            List<T> lista =new List<T>();

            InOrderRecursivo(raiz,lista);

            return lista;
        }

        private void InOrderRecursivo( Nodo nodo,List<T> lista)
        {
            if (nodo == null)
            {
                return;
            }

            InOrderRecursivo(nodo.Izquierda,lista); 
            lista.Add(nodo.Dato);

            InOrderRecursivo(nodo.Derecha,lista);
        }

        public List<T> ObtenerProximasAVencer(int dias,Func<T, DateTime> obtenerFecha)
        {
            DateTime hoy = DateTime.Now;

            DateTime limite = hoy.AddDays(dias);

            return BuscarRango(x =>
            {
                DateTime fecha =obtenerFecha(x);return fecha >= hoy && fecha <= limite;
            });
        }
        public void Limpiar()
        {
            raiz = null;

            cantidad = 0;
        }








    }
}

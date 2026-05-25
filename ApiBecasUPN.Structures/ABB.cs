using System;
using System.Collections.Generic;
using System.Text;

namespace ApiBecasUPN.Structures
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


        public int Cantidad => cantidad;

        public bool EstaVacio => raiz == null;

    

        public void Insertar(T dato)
        {
            raiz = InsertarRecursivo(raiz, dato);

            cantidad++;
        }

        private Nodo InsertarRecursivo(Nodo nodo, T dato)
        {
            if (nodo == null)
            {
                return new Nodo(dato);
            }

            int comparacion = comparar(dato, nodo.Dato);

            if (comparacion < 0)
            {
                nodo.Izquierda =
                    InsertarRecursivo(nodo.Izquierda, dato);
            }

            else if (comparacion > 0)
            {
                nodo.Derecha =
                    InsertarRecursivo(nodo.Derecha, dato);
            }

            else
            {
                nodo.Dato = dato;
            }

            return nodo;
        }

        public void CargarDesdeColeccion(IEnumerable<T> datos)
        {
            Limpiar();

            foreach (var dato in datos)
            {
                Insertar(dato);
            }
        }

       

        public T Buscar(Func<T, bool> condicion)
        {
            return BuscarRecursivo(raiz, condicion);
        }

        private T BuscarRecursivo(
            Nodo nodo,
            Func<T, bool> condicion)
        {
            if (nodo == null)
            {
                return default;
            }

            if (condicion(nodo.Dato))
            {
                return nodo.Dato;
            }

            var izquierda =
                BuscarRecursivo(
                    nodo.Izquierda,
                    condicion);

            if (izquierda != null)
            {
                return izquierda;
            }

            return BuscarRecursivo(
                nodo.Derecha,
                condicion);
        }



        public List<T> BuscarRango(
            Func<T, bool> condicion)
        {
            var resultados = new List<T>();

            BuscarRangoRecursivo(
                raiz,
                condicion,
                resultados);

            return resultados;
        }

        private void BuscarRangoRecursivo(
            Nodo nodo,
            Func<T, bool> condicion,
            List<T> resultados)
        {
            if (nodo == null)
            {
                return;
            }

            BuscarRangoRecursivo(
                nodo.Izquierda,
                condicion,
                resultados);

            if (condicion(nodo.Dato))
            {
                resultados.Add(nodo.Dato);
            }

            BuscarRangoRecursivo(
                nodo.Derecha,
                condicion,
                resultados);
        }



        public T ObtenerMenor()
        {
            if (raiz == null)
            {
                return default;
            }

            var actual = raiz;

            while (actual.Izquierda != null)
            {
                actual = actual.Izquierda;
            }

            return actual.Dato;
        }


        public T ObtenerMayor()
        {
            if (raiz == null)
            {
                return default;
            }

            var actual = raiz;

            while (actual.Derecha != null)
            {
                actual = actual.Derecha;
            }

            return actual.Dato;
        }

     

        public List<T> InOrder()
        {
            var lista = new List<T>();

            InOrderRecursivo(raiz, lista);

            return lista;
        }

        private void InOrderRecursivo(
            Nodo nodo,
            List<T> lista)
        {
            if (nodo == null)
            {
                return;
            }

            InOrderRecursivo(
                nodo.Izquierda,
                lista);

            lista.Add(nodo.Dato);

            InOrderRecursivo(
                nodo.Derecha,
                lista);
        }

    
        public List<T> PreOrder()
        {
            var lista = new List<T>();

            PreOrderRecursivo(raiz, lista);

            return lista;
        }

        private void PreOrderRecursivo(
            Nodo nodo,
            List<T> lista)
        {
            if (nodo == null)
            {
                return;
            }

            lista.Add(nodo.Dato);

            PreOrderRecursivo(
                nodo.Izquierda,
                lista);

            PreOrderRecursivo(
                nodo.Derecha,
                lista);
        }

        public int Altura()
        {
            return AlturaRecursiva(raiz);
        }

        private int AlturaRecursiva(Nodo nodo)
        {
            if (nodo == null)
            {
                return 0;
            }

            return 1 + Math.Max(
                AlturaRecursiva(nodo.Izquierda),
                AlturaRecursiva(nodo.Derecha));
        }

       

        public void Limpiar()
        {
            raiz = null;

            cantidad = 0;
        }
    }
}

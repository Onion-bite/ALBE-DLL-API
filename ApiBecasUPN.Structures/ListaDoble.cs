using System;
using System.Collections.Generic;
using System.Text;

namespace ApiBecasUPN.Structures
{
    internal class ListaDoble
    {
        public class Nodo<T>
        {
            public T Dato { get; set; }
            public Nodo<T> Atras { get; set; }
            public Nodo<T> Adelante { get; set; }

            public Nodo(T dato)
            {
                Dato = dato;
                Atras = null;
                Adelante = null;
            }
        }

        public class LstDoble<T> : IEnumerable<T>
        {
            private Nodo<T> cabeza = null;
            private Nodo<T> cola = null;
            private int total = 0;

            public int Cantidad => total;
            public bool EstaVacia => cabeza == null;

            public T Primero
            {
                get
                {
                    if (cabeza != null)
                        return cabeza.Dato;
                    else
                        return default(T);
                }
            }

            public T Ultimo
            {
                get
                {
                    if (cola != null)
                        return cola.Dato;
                    else
                        return default(T);
                }
            }

            

            public void InsertarInicio(T dato)
            {
                Nodo<T> nuevo = new Nodo<T>(dato);

                if (cabeza == null)
                {
                    cabeza = cola = nuevo;
                }
                else
                {
                    nuevo.Adelante = cabeza;
                    cabeza.Atras = nuevo;
                    cabeza = nuevo;
                }

                total++;
            }

            public void InsertarFinal(T dato)
            {
                Nodo<T> nuevo = new Nodo<T>(dato);

                if (cola == null)
                {
                    cabeza = cola = nuevo;
                }
                else
                {
                    nuevo.Atras = cola;
                    cola.Adelante = nuevo;
                    cola = nuevo;
                }

                total++;
            }

           

            public T EliminarPrimero()
            {
                if (cabeza == null) return default(T);

                T datoGuardado = cabeza.Dato;

                if (cabeza == cola)
                {
                    cabeza = cola = null;
                }
                else
                {
                    cabeza = cabeza.Adelante;
                    cabeza.Atras = null;
                }

                total--;
                return datoGuardado;
            }

            public T EliminarUltimo()
            {
                if (cola == null) return default(T);

                T datoGuardado = cola.Dato;

                if (cabeza == cola)
                {
                    cabeza = cola = null;
                }
                else
                {
                    cola = cola.Atras;
                    cola.Adelante = null;
                }

                total--;
                return datoGuardado;
            }

            public bool Eliminar(Func<T, bool> condicion)
            {
                Nodo<T> actual = cabeza;

                while (actual != null)
                {
                    if (condicion(actual.Dato))
                    {
                        if (actual.Atras != null)
                            actual.Atras.Adelante = actual.Adelante;
                        else
                            cabeza = actual.Adelante;

                        if (actual.Adelante != null)
                            actual.Adelante.Atras = actual.Atras;
                        else
                            cola = actual.Atras;

                        total--;
                        return true;
                    }

                    actual = actual.Adelante;
                }

                return false;
            }

            

            public T Buscar(Func<T, bool> condicion)
            {
                Nodo<T> actual = cabeza;

                while (actual != null)
                {
                    if (condicion(actual.Dato))
                        return actual.Dato;

                    actual = actual.Adelante;
                }

                return default(T);
            }

          
            public List<T> RecorrerReversa()
            {
                List<T> lista = new List<T>();
                Nodo<T> actual = cola;

                while (actual != null)
                {
                    lista.Add(actual.Dato);
                    actual = actual.Atras;
                }

                return lista;
            }

            

            public void Limpiar()
            {
                cabeza = null;
                cola = null;
                total = 0;
            }

            public void CargarDesdeColeccion(IEnumerable<T> coleccion)
            {
                Limpiar();

                foreach (T item in coleccion)
                    InsertarFinal(item);
            }

         

            public IEnumerator<T> GetEnumerator()
            {
                List<T> lista = new List<T>();
                Nodo<T> actual = cabeza;

                while (actual != null)
                {
                    lista.Add(actual.Dato);
                    actual = actual.Adelante;
                }

                return lista.GetEnumerator();
            }

            System.Collections.IEnumerator
                System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}

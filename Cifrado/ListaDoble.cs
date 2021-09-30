﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ListaDobleEnlace
{
    public class ListaDoble<T> : IEnumerable<T>
    { 
        public Nodo<T> inicio = new Nodo<T>();
        private Nodo<T> fin = new Nodo<T>();
        public int contador;


        public ListaDoble()
        {
            inicio = null;
            fin = null;
            contador = 0;
        }

        ~ListaDoble() { }

        bool ListaVacia()
        {
            return contador == 0;
        }

        public void Vaciar()
        {
            if(inicio != null)
            {
                inicio = null;
                fin = null;
                contador = 0;
            }
        }

        public void InsertarInicio(T NuevoValor)
        {
            if(NuevoValor == null)
            {
                return;
            }
            Nodo<T> nuevoNodo = new Nodo<T>();
            nuevoNodo.Valor = NuevoValor;

            if (ListaVacia())
            {
                inicio = nuevoNodo;
                fin = nuevoNodo;
            }
            else
            {
                nuevoNodo.Siguiente = inicio;
                inicio.Anterior = nuevoNodo;
                inicio = nuevoNodo;
            }
            contador++;
            return;
        }

        public void InsertarEnPosicion(T NuevoValor, int Posicion)
        {
            Nodo<T> temporal = inicio;
            Nodo<T> NodoAdd = new Nodo<T>();
            NodoAdd.Valor = NuevoValor;

            if (contador >= 0)
            {
                if (Posicion == 0)
                {
                    InsertarInicio(NodoAdd.Valor);
                }
                else if (Posicion == contador)
                {
                    InsertarFinal(NodoAdd.Valor);
                }
                else if (Posicion > 0 && Posicion < contador + 1)
                {
                    Nodo<T> auxiliar = inicio;
                    int pos = 0;

                    while ((pos < Posicion))
                    {
                        auxiliar = auxiliar.Siguiente;
                        pos++;
                    }

                    auxiliar.Anterior.Siguiente = NodoAdd;
                    NodoAdd.Siguiente = auxiliar;
                    NodoAdd.Anterior = auxiliar.Anterior;
                    auxiliar.Anterior = NodoAdd;

                    contador++;
                }
            }

            /*if (!ListaVacia())
            {
                Nodo<T> auxiliar = inicio;
                int pos = 0;

                while ((pos < Posicion))
                {
                    auxiliar = auxiliar.Siguiente;
                    pos++;
                }
                if(Posicion == 0)
                {
                    InsertarInicio(NodoAdd.Valor);
                }
                else
                {
                    auxiliar.Anterior.Siguiente = NodoAdd;
                    NodoAdd.Anterior = auxiliar.Anterior;
                    auxiliar.Anterior = NodoAdd;
                    NodoAdd.Siguiente = auxiliar;
                    contador++;
                }
            }*/
        }

        public void InsertarFinal(T NuevoValor)
        {
            Nodo<T> nuevoNodo = new Nodo<T>();
            nuevoNodo.Valor = NuevoValor;

            if (ListaVacia())
            {
                inicio = nuevoNodo;
                fin = nuevoNodo;
            }
            else
            {
                fin.Siguiente = nuevoNodo;
                nuevoNodo.Anterior = fin; 
                fin = nuevoNodo; 
            }
            contador++;
            return;
        }
        
        public Nodo<T> ExtraerEnPosicion(int posicion)
        {
            Nodo<T> temporal = inicio;

            if (!ListaVacia())
            {
                if ((contador == 1) || (posicion == 0))
                {
                    return ExtraerInicio();
                }
                else
                {
                    if (posicion >= contador - 1)
                    {
                        return ExtraerFinal();
                    }
                    else
                    {
                        Nodo<T> auxiliar = inicio;
                        int pos = 0;

                        while ((pos < posicion))
                        {
                            auxiliar = auxiliar.Siguiente;
                            pos++;
                        }
                        auxiliar.Anterior.Siguiente = auxiliar.Siguiente;
                        auxiliar.Siguiente.Anterior = auxiliar.Anterior;
                        temporal = auxiliar; //linea extra
                        contador--;
                    }
                }
            }
            return temporal;
        }
        public T ObtenerValor(int posicion)
        {
            if (posicion >= 0 && posicion < contador)
            {

                Nodo<T> temporal = new Nodo<T>();
                temporal = inicio;
                int ubicacion = 0;

                while (ubicacion < posicion)
                {
                    temporal = temporal.Siguiente;
                    ubicacion++;
                }
                return temporal.Valor;
            }
            return default(T);
        }

        public T ObtenerValor(int posicion, T value) //sobre carga para poder cambiar los valores pero no las propiedades del nodo
        {
            if (posicion >= 0 && posicion < contador)
            {

                Nodo<T> temporal = new Nodo<T>();
                temporal = inicio;
                int ubicacion = 0;

                while (ubicacion < posicion)
                {
                    temporal = temporal.Siguiente;
                    ubicacion++;
                }
                temporal.Valor = value;
                return temporal.Valor;
            }
            return default(T);
        }

        public Nodo<T> ObtenerNodo(int posicion)
        {
            if (posicion >= 0 && posicion < contador)
            {
                Nodo<T> temporal = new Nodo<T>();
                temporal = inicio;
                int ubicacion = 0;

                while (ubicacion < posicion)
                {
                    temporal = temporal.Siguiente;
                    ubicacion++;
                }
                return temporal;
            }
            return default(Nodo<T>);
        }

        public Nodo<T> ExtraerInicio()
        {
            Nodo<T> temporal = inicio;

            if (!ListaVacia())
            {
                if (contador == 1)
                {
                    fin = inicio;
                }
                else
                {
                    inicio = inicio.Siguiente;
                    inicio.Anterior = null;
                }
                contador--;

                /*inicio = inicio.Siguiente;
                inicio.Anterior = null;
                if (contador == 1)
                {
                    fin = inicio;
                }
                contador--;
            }*/
            }
            return temporal;
        }

       public  Nodo<T> ExtraerFinal()
        {
            Nodo<T> temporal = fin;

            if (!ListaVacia())
            {
                if (contador == 1)
                {
                    fin = fin.Siguiente;
                    inicio = fin;
                }
                else
                {
                    fin = fin.Anterior;
                    fin.Siguiente = null;
                }
                contador--;
            }
            return temporal;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Nodo<T> nodo = inicio;
            while (nodo != null)
            {
                yield return nodo.Valor;
                nodo = nodo.Siguiente;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator(); 
        }
    }
}

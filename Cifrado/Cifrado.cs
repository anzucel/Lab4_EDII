using System;
using ListaDobleEnlace;
using System.Collections.Generic;
using System.Text;

namespace Cifrado
{
    public class Cifrado : ICifrado
    {
        private char[] ABC = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private char[] abc = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'ñ', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private char[] ABCaux;
        private char[] abcaux;


        //constructor
        public Cifrado()
        {
            abcaux = new char[27];
            ABCaux = new char[27];
        }

        private void ArmarABC(string clave)
        {
            string UpperCaseKey = clave.ToUpper();
            string LowerCaseKey = clave.ToLower();
            bool insert = true;

            for (int i = 0; i < UpperCaseKey.Length; i++)
            {
                ABCaux[i] = UpperCaseKey[i];
                abcaux[i] = LowerCaseKey[i];
            }

            int posicion = UpperCaseKey.Length;
            for (int i = 0; i < ABC.Length; i++)
            {
                for (int j = 0; j < ABCaux.Length; j++)
                {
                    if (ABCaux[j] == ABC[i])
                    {
                        insert = false;
                        break;
                    }
                }
                if (insert)
                {
                    ABCaux[posicion] = ABC[i];
                    posicion++;
                }
                else
                    insert = true;
            }

            posicion = LowerCaseKey.Length;
            for (int i = 0; i < abc.Length; i++)
            {
                for (int j = 0; j < abcaux.Length; j++)
                {
                    if (abcaux[j] == abc[i])
                    {
                        insert = false;
                        break;
                    }
                }
                if (insert)
                {
                    abcaux[posicion] = abc[i];
                    posicion++;
                }
                else
                    insert = true;
            }
        }

        public string Cesar(string texto, string clave)
        {
            string txt_cifrado = "";
            string claveaux = clave[0].ToString();
            bool insertar = true; ;

            for (int i = 0; i < clave.Length; i++)
            {
                insertar = true; ;
                for (int j = 0; j < claveaux.Length; j++)
                {
                    if (clave[i] == claveaux[j]) { insertar = false; ; }
                }
                if (insertar)
                    claveaux += clave[i];
            }

            ArmarABC(claveaux);
            bool cambiar = false;

            for (int i = 0; i < texto.Length; i++)
            {
                cambiar = false;
                if (char.IsLower(texto[i]))
                {
                    for (int j = 0; j < abc.Length; j++)
                    {
                        if (abc[j] == texto[i])
                        {
                            txt_cifrado += abcaux.GetValue(j).ToString();
                            cambiar = true;
                            break;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < ABC.Length; j++)
                    {
                        if (ABC[j] == texto[i])
                        {
                            txt_cifrado += ABCaux.GetValue(j).ToString();
                            cambiar = true;
                            break;
                        }
                    }
                }
                if (!cambiar)
                {
                    txt_cifrado += texto[i];
                }
            }

            return txt_cifrado;
        }

        public string Zigzag(string texto, int clave)
        {
            ListaDoble<string> caracteres = new ListaDoble<string>();
            decimal cant_olas = texto.Length / clave;
            cant_olas = Math.Floor(cant_olas);
            int claves_ola = (clave * 2) - 2;
            int valTotales = Convert.ToInt32(cant_olas) * claves_ola;
            string txt_cifrado = "";

            int inicio = 0, fin = claves_ola;
            while (texto.Length > 0)
            {
                if (texto.Length < fin)
                {
                    int extra = fin - texto.Length;
                    for (int i = 0; i < extra; i++)
                    {
                        texto += "■";
                    }
                    caracteres.InsertarFinal(texto.Substring(inicio, fin));
                    texto = texto.Remove(inicio, fin);
                }
                else                    

                {
                    caracteres.InsertarFinal(texto.Substring(inicio, fin));
                    texto = texto.Remove(inicio, fin);
                }
            }

            for (int i = clave, j = 0; i > 0; i--, j++)
            {
                if (i == clave)
                {
                    for (int h = 0; h < caracteres.contador; h++)
                    {
                        string aux = caracteres.ObtenerValor(h);
                        txt_cifrado += aux[0];
                    }
                }
                if (i == 1)
                {
                    int posicion = claves_ola / 2;
                    for (int k = 0; k < caracteres.contador; k++)
                    {
                        string aux = caracteres.ObtenerValor(k);
                        txt_cifrado += aux[posicion];
                    }
                }
                if (i > 1 && i < clave)
                {
                    int posicion;
                    int contador;
                    for (int l = 0; l < caracteres.contador; l++)
                    {
                        contador = 0;
                        posicion = j;
                        while (contador < 2)
                        {
                            string aux = caracteres.ObtenerValor(l);
                            txt_cifrado += aux[posicion];
                            posicion += (i * 2) - 2;
                            contador++;
                        }
                    }
                }
            }

            return txt_cifrado;
        }

        //descifrados

        public string descompress_Cesar(string clave, string texto)
        {
            string txt_descifrado = "";
            string claveaux = clave[0].ToString();
            bool insertar = true; ;

            for (int i = 0; i < clave.Length; i++)
            {
                insertar = true; ;
                for (int j = 0; j < claveaux.Length; j++)
                {
                    if (clave[i] == claveaux[j]) { insertar = false; ; }
                }
                if (insertar)
                    claveaux += clave[i];
            }

            ArmarABC(claveaux);
            bool cambiar = false;

            //Desifrar
            for (int i = 0; i < texto.Length; i++)
            {
                cambiar = false;
                if (char.IsLower(texto[i]))
                {
                    for (int j = 0; j < abc.Length; j++)
                    {
                        if (abcaux[j] == texto[i])
                        {
                            txt_descifrado += abc.GetValue(j).ToString();
                            cambiar = true;
                            break;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < ABC.Length; j++)
                    {
                        if (ABCaux[j] == texto[i])
                        {
                            txt_descifrado += ABC.GetValue(j).ToString();
                            cambiar = true;
                            break;
                        }
                    }
                }
                if (!cambiar)
                {
                    txt_descifrado += texto[i];
                }
            }

            return txt_descifrado;


        }


        public string descompress_zigzag(int clave, string texto)
        {
            string txt_descifrado = "";
            int Tamaño_olas = 1 + 1 + 2 * (clave - 2);
            texto = texto.Remove(0,1);
            int cant_caracteres = texto.Length;
            int cant_olas = cant_caracteres / Tamaño_olas;
          
            List<string> cadena = new List<string>();
            

            //extraer pico superior de las olas
            string pico_S = texto.Substring(0, cant_olas);
            texto=texto.Remove(0, cant_olas);
            cadena.Add(pico_S);

            //extraer pico inferior de las olas
            int cont = texto.Length - cant_olas;
            string pico_I = texto.Substring(cont, cant_olas);
            texto = texto.Remove(cont, cant_olas);

            //separar texto
            int separar = cant_olas * 2;
            for(int i=0;i<texto.Length;i++)
            {
                string insert = texto.Substring(0, separar);
                texto= texto.Remove(0, separar);
                cadena.Add(insert);
            }

            cadena.Add(pico_I);


            //formar texto
            while(cant_caracteres!=0)
            {
                for(int i=0;i<(cadena.Count-1);i++)
                {
                    if (cadena[i].Substring(0, 1) != "■")
                    {
                        txt_descifrado += cadena[i].Substring(0, 1);

                        string delete = cadena[i].Remove(0, 1);
                        cadena[i] = delete;
                        cant_caracteres--;
                    }
                    else
                    {
                        string delete = cadena[i].Remove(0, 1);
                        cadena[i] = delete;
                        cant_caracteres--;
                    }
                }

                for (int i = (cadena.Count - 1); i > 0; i--)
                {
                    if (cadena[i].Substring(0, 1) != "■")
                    {
                        txt_descifrado += cadena[i].Substring(0, 1);

                        string delete = cadena[i].Remove(0, 1);
                        cadena[i] = delete;
                        cant_caracteres--;
                    }

                    else
                    {
                        string delete = cadena[i].Remove(0, 1);
                        cadena[i] = delete;
                        cant_caracteres--;
                    }
                }
            }



            return txt_descifrado;
        }
    }
}

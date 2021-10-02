using System;
using ListaDobleEnlace;

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
                    if(clave[i] == claveaux[j]) { insertar = false; ; }
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
                        if(ABC[j] == texto[i])
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
                        texto += "$";
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
    }
}

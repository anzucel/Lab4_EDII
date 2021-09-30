using System;

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
            //char[] ABC = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            //char[] abc = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'ñ', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
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
            ArmarABC(clave);
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
            throw new NotImplementedException();
        }
    }
}

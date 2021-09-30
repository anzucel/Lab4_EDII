using System;
using Cifrado;

namespace PruebaConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            ICifrado cifrado;
            cifrado = new Cifrado.Cifrado();

            string texto = "La música está demasiado fuerte cómo para oír la clase";
            string clave = "sapo";
            string cesar = "";

            cesar = cifrado.Cesar(texto, clave);

            Console.ReadKey();
            
        }
    }
}

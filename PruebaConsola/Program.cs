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

            string texto = "JUGAMOS A LOS DADOS";// "ANITA LAVA LA TINA"; //"La música está demasiado fuerte cómo para oír la clase";
            string clave = "mapa";
            int clavez = 4;
            string cesar = "";
            string zigzag = "";

            cesar = cifrado.Cesar(texto, clave);
            zigzag = cifrado.Zigzag(texto, clavez);

            Console.ReadKey();
            
        }
    }
}

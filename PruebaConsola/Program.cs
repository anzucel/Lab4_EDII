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

            string texto = "La música está demasiado fuerte cómo para oír la clase"; //"JUGAMOS A LOS DADOS";// "ANITA LAVA LA TINA"; 

            string clave = "mapa";
            int clavez = 4;
            string cesar = "";
            string cesar_decompres = "";
            string zigzag = "";
            string zigzag_decompress = "";

          //  cesar = cifrado.Cesar(texto, clave);
            zigzag = cifrado.Zigzag(texto, clavez);
            //  cesar_decompres = cifrado.descompress_Cesar(clave, cesar);
            zigzag_decompress = cifrado.descompress_zigzag(clavez, zigzag);
            Console.ReadKey();
            
        }
    }
}

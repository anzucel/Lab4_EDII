using System;
using System.Collections.Generic;
using System.Text;

namespace Cifrado
{
    public interface ICifrado
    {
        string Cesar(string texto, string clave);
        string Zigzag(string texto, int clave);
        string descompress_Cesar(string clave, string texto);
        string descompress_zigzag(int clave, string texto);
    }
}

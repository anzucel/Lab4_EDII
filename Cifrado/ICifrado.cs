using System;
using System.Collections.Generic;
using System.Text;

namespace Cifrado
{
    public interface ICifrado
    {
        string Cesar(string texto, string clave);
        string Zigzag(string texto, int clave);
    }
}

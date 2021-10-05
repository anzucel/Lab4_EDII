using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cifrado;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]   
    public class apiController : ControllerBase
    {
        // GET: api/
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/
        [HttpPost]
        [Route("cipher/{method}")]
        public IActionResult PostFileCipher([FromForm] IFormFile file, [FromRoute] string method, [FromForm] string key)
        {
            using var archivo = new MemoryStream();
            try
            {
                file.CopyToAsync(archivo);
                var coleccion = Encoding.UTF8.GetString(archivo.ToArray()); //texto a cadena 
                Byte[] texto_bytes = Encoding.UTF8.GetBytes(coleccion);
                string texto = Encoding.UTF8.GetString(texto_bytes);
                ICifrado cifrado = new Cifrado.Cifrado();
                string txt_cifrado = "";
                if (method == "cesar") { txt_cifrado = cifrado.Cesar(texto, key/*"fef"*/); };
                if (method == "zigzag") { txt_cifrado = cifrado.Zigzag(texto, Convert.ToInt32(key)); };
                string nombreArchivo = file.FileName.Remove(file.FileName.Length - 4);
                Escribir(txt_cifrado, nombreArchivo, method);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("decipher")]
        public IActionResult PostFileDecipher([FromForm] IFormFile file, [FromForm] string key)
        {
            using var archivo = new MemoryStream();
            try
            {
                file.CopyToAsync(archivo);
                var coleccion = Encoding.UTF8.GetString(archivo.ToArray()); //texto a cadena 
                Byte[] texto_bytes = Encoding.UTF8.GetBytes(coleccion);
                string texto = Encoding.UTF8.GetString(texto_bytes);
                string txt_descifrado = "";
                ICifrado cifrado = new Cifrado.Cifrado();

                string name = file.FileName;
                string[] extension = name.Split(".");

                if (extension[1] == "csr") { txt_descifrado = cifrado.descompress_Cesar(key, texto); }
                if (extension[1] == "zz") { txt_descifrado = cifrado.descompress_zigzag(Int32.Parse(key), texto); }
                escribirtxt(txt_descifrado, extension[0]);
                return Ok();


            }
            catch (Exception)
            {
                return StatusCode(500);
            }

           // throw new NotImplementedException();
        }

            void Escribir(string imprimir, string nombre, string metodo)
        {
            try
            {
                string ruta = "";
                if (metodo == "cesar") ruta = "../Archivos/" + nombre + ".csr";
                if (metodo == "zigzag") ruta = "../Archivos/" + nombre + ".zz";

                Byte[] texto_bytes = Encoding.UTF8.GetBytes(imprimir);
                string x = Encoding.UTF8.GetString(texto_bytes);

                StreamWriter sw = new StreamWriter(ruta, false, Encoding.UTF8);
                sw.Write(x);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        void escribirtxt(string imprimir, string name)
        {
           
            string DireccionNombre = "../Archivos/" + name + ".txt";//Ruta en donde se guardará 



            Encoding utf8 = Encoding.UTF8;
            //pasar de string a bytes 
            Byte[] texto_bytes = utf8.GetBytes(imprimir);

            //pasar de bytes a string
            string x = Encoding.UTF8.GetString(texto_bytes);
            try
            {
                //Open the File
                StreamWriter sw = new StreamWriter(DireccionNombre, false, Encoding.UTF8);

                sw.Write(x);

                //close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

        }
    }
}

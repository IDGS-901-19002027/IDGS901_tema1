using IDGS901_tema1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace IDGS901_tema1.Services
{
    public class GuardarPalabras
    {
        // Método para guardar archivo
        public void GuardarArchivo(Traductor traductor)
        {
            var esp = traductor.espaniol;
            var ingl = traductor.ingles;

            var datos = esp + "," + ingl + Environment.NewLine;
            var archivo = HttpContext.Current.Server.MapPath("~/App_Data/Traductor.txt");
            //File.WriteAllText(archivo, datos);

            File.AppendAllText(archivo, datos);
            LeerPalabras();
        }

        // Método para Leer el archivo
        public Array LeerPalabras()
        {
            Array palabrasData = null;
            var archivo = HttpContext.Current.Server.MapPath("~/App_Data/Traductor.txt");
            if (File.Exists(archivo))
            {
                // Obtener el contenido del archivo
                palabrasData = File.ReadAllLines(archivo);
            }
            return palabrasData;
        }

        // Método para buscar palabras
        public static List<Traductor> BuscarPalabras(string rutaArchivo, string palabra, string eleccion)
        {
            List<Traductor> resultados = new List<Traductor>();

            // Lee todas las líneas del archivo
            string[] lineas = File.ReadAllLines(rutaArchivo);


            foreach (string linea in lineas)
            {
                string[] palabras = linea.Split(',');
                //Console.WriteLine(palabras);

                if (palabras.Length >= 2 && palabras[0].Trim().Equals(palabra, StringComparison.OrdinalIgnoreCase) || palabras[1].Trim().Equals(palabra, StringComparison.OrdinalIgnoreCase) )
                {
                    // Instancia de un objeto de la clase Traductor
                    Traductor resultado = new Traductor
                    {
                        espaniol = palabras[0].Trim(),
                        ingles = palabras[1].Trim(),
                        eleccion = eleccion
                    };

                    if (eleccion.Equals("1", StringComparison.OrdinalIgnoreCase))
                    {
                        resultado.resultado = resultado.espaniol;
                    }
                    else if (eleccion.Equals("2", StringComparison.OrdinalIgnoreCase))
                    {
                        resultado.resultado = resultado.ingles;
                    }

                    resultados.Add(resultado);
                }
                /*if (eleccion == "1" && palabras.Length >= 3 && palabras[0].Trim().ToLower() == palabra.Trim().ToLower())
                {
                    resultados.Add(new Traductor { espaniol = palabras[0].Trim(), ingles = palabras[1].Trim() });
                }
                else if (eleccion == "2" && palabras.Length >= 3 && palabras[1].Trim().ToLower() == palabra.Trim().ToLower())
                {
                    resultados.Add(new Traductor { espaniol = palabras[0].Trim(), ingles = palabras[1].Trim() });
                }*/
            }

            return resultados;
        }
    }
}
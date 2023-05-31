using IDGS901_tema1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IDGS901_tema1.Controllers
{
    public class TrianguloController : Controller
    {
        // GET: Triangulo
        public ActionResult Area()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Area(Triangulo triangulo)
        {
            // Obtener el área del triángulo a través de la fórmula de Herón
            // A = √s(s-lado1)(s-lado2)(s-lado3)
            // donde s = semiperímetro 

            double lado1 = Math.Round(Math.Sqrt(Math.Pow(triangulo.x2 - triangulo.x1, 2) + Math.Pow(triangulo.y2 - triangulo.y1, 2)), 2);
            double lado2 = Math.Round(Math.Sqrt(Math.Pow(triangulo.x3 - triangulo.x2, 2) + Math.Pow(triangulo.y3 - triangulo.y2, 2)), 2);
            double lado3 = Math.Round(Math.Sqrt(Math.Pow(triangulo.x1 - triangulo.x3, 2) + Math.Pow(triangulo.y1 - triangulo.y3, 2)), 2);

            // Calcular semiperimetro -> s = (lado1 + lado2 + lado3) /2
            double semiperimetro = (lado1 + lado2 + lado3) / 2;
            // Calcular área del triángulo
            double area = Math.Sqrt(semiperimetro * (semiperimetro - lado1) * (semiperimetro - lado2) * (semiperimetro - lado3));

            string tipo = "";

            if ((triangulo.x1 * (triangulo.y2 - triangulo.y3) + triangulo.x2 * (triangulo.y3 - triangulo.y1) + triangulo.x3 * (triangulo.y1 - triangulo.y2)) != 0)
            {   
                // Verificar que el lado mayor no sea mayor a la suma de los otros dos lados 
                if (lado1 + lado2 > lado3 && lado2 + lado3 > lado1 && lado3 + lado1 > lado2)
                {
                    if (lado1 == lado2 && lado2 == lado3)
                        tipo = "Equilátero";
                    else if (lado1 == lado2 || lado2 == lado3 || lado3 == lado1)
                        tipo = "Isósceles";
                    else
                        tipo = "Escaleno";

                    triangulo.resultado = area;
                    triangulo.tipo = tipo;

                    return View(triangulo);
                }
                else
                {
                    triangulo.tipo = "No se cumple la condición de que el lado mayor sea menor que la suma de los otros 2.";
                }
            }
            else
            {
                triangulo.resultado = 0;
                triangulo.tipo = "Error, los puntos ingresados no forman un triángulo válido";
            }

            return View(triangulo);

        }
    }
}
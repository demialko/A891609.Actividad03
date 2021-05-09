using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A891609.Actividad03
{
    static class Validador
    {
        public static string pedirStrNoVacio(string mensaje)
        {
            string strNoVacio = "";
            do
            {
                Console.WriteLine(mensaje);
                strNoVacio = Console.ReadLine().ToUpper();
            } while (strNoVacio == "");
            return strNoVacio;
        }
    }
}

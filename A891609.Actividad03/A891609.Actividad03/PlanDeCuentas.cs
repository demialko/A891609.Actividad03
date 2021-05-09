using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A891609.Actividad03
{
    static class PlanDeCuentas
    {
        private static readonly Dictionary<int, Cuenta> inputCuentas;
        private static readonly Dictionary<int, Cuenta> inputMayor;
        //En mi caso, la ruta default es bin/debug
        const string archivo = "PlanDeCuentas.txt";
        const string archivoMayor = "Mayor.txt";
        static PlanDeCuentas()
        {
            inputCuentas = new Dictionary<int, Cuenta>();
            inputMayor = new Dictionary<int, Cuenta>();
            if (File.Exists(archivo))
            {
                using (var reader = new StreamReader(archivo))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var cuenta = new Cuenta(linea);
                        if (cuenta.Codigo != -1)
                        {
                            inputCuentas.Add(cuenta.Codigo, cuenta);
                        }
                    }
                }
            }
            if (File.Exists(archivoMayor))
            {
                using (var reader = new StreamReader(archivoMayor))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        var datos = linea.Split('|');
                        if (!int.TryParse(datos[0], out var codigo))
                        {
                            codigo = -1;
                        }
                        if (codigo != -1)
                        {
                            var cuenta = new Cuenta(codigo);
                            inputMayor.Add(cuenta.Codigo, cuenta);
                        }  
                    }
                }
            }
        }
        public static void AgregarCuenta(Cuenta cuenta)
        {
            inputCuentas.Add(cuenta.Codigo, cuenta);
            GrabarArchivo();
        }
        public static Cuenta SeleccionarCuenta()
        {
            var modeloBusqueda = Cuenta.CrearModeloBusqueda();

            foreach (var cuenta in inputCuentas.Values)
            {
                if (cuenta.CoincideCon(modeloBusqueda))
                {
                    return cuenta;
                }
            }
            Console.WriteLine("No se ha encontrado una cuenta que coincida con los criterios indicados.");
            return null;
        }

        internal static void EliminarCuenta(Cuenta cuenta)
        {
            if (ExisteEnMayor(cuenta.Codigo))
            {
                Console.WriteLine("La cuenta no se puede eliminar debido a que la misma se encuentra en el Mayor.txt");
            }
            else
            {
                inputCuentas.Remove(cuenta.Codigo);
                GrabarArchivo();
                Console.WriteLine($"{cuenta.DescBreve} ha sido dada de baja");
            }
        }
        public static bool Existe(int codigo)
        {
            return inputCuentas.ContainsKey(codigo);
        }
        public static bool ExisteEnMayor(int codigo)
        {
            return inputMayor.ContainsKey(codigo);
        }
        public static void GrabarArchivo()
        {
            using (var writer = new StreamWriter(archivo, append: false))
            {
                string header = "Codigo | Nombre | Tipo";
                writer.WriteLine(header);
                foreach (var cuenta in inputCuentas.Values)
                {
                    var linea = cuenta.ObtenerLineaDatos();
                    writer.WriteLine(linea);
                }
            }
        }
    }
}

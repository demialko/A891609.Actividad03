using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A891609.Actividad03
{
    class Cuenta
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string DescBreve
        {
            get
            {
                return $"{Nombre}, {Tipo}";
            }
        }
        public Cuenta() { }
        public Cuenta(string linea)
        {
            var datosCuenta = linea.Split('|');
            //Prueba
            if (!int.TryParse(datosCuenta[0], out var codigo))
            {
                codigo =  -1;
            }
            //
            Codigo = codigo;
            //Codigo = int.Parse(datosCuenta[0]);
            Nombre = datosCuenta[1];
            Tipo = datosCuenta[2];
        }
        //Prueba mayor
        public Cuenta(int codigo)
        {
            Codigo = codigo;
        }
        public string ObtenerLineaDatos()
        {
            return $"{Codigo}|{Nombre}|{Tipo}";
        }

        public static Cuenta IngresarNueva()
        {
            var cuenta = new Cuenta();
            cuenta.Codigo = IngresarCodigo();
            cuenta.Nombre = IngresoNombre();
            cuenta.Tipo = IngresoTipo();
            return cuenta;
        }

        public void Modificar()
        {
            Console.WriteLine($"Nombre: {Nombre} - Presione S para modificar");
            var tecla = Console.ReadKey(true);
            if (tecla.Key == ConsoleKey.S)
            {
                Nombre = IngresoNombre();
            }
            PlanDeCuentas.GrabarArchivo();
        }
        public void Mostrar()
        {
            Console.WriteLine();
            Console.WriteLine($"Código: {Codigo}");
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Tipo: {Tipo}");
            Console.WriteLine();
        }
        public static Cuenta CrearModeloBusqueda()
        {
            var modeloBusqueda = new Cuenta();
            bool ok = false;
            do
            {
                modeloBusqueda.Codigo = IngresarCodigoBuscar(false);
                modeloBusqueda.Nombre = IngresoNombre(false);
                if (modeloBusqueda.Codigo == 0 && modeloBusqueda.Nombre == "")
                {
                    ok = false;
                    return null;
                }
                else
                {
                    ok = true;
                    return modeloBusqueda;
                }
            } while (!ok);
        }
        private static int IngresarCodigo(bool obligatorio = true)
        {
            var titulo = "Ingrese el código de la cuenta";
            if (!obligatorio)
            {
                titulo += " o presione [Enter] para continuar: ";
            }
            else
            {
                titulo += ": ";
            }
            do
            {
                Console.Write(titulo);
                var ingreso = Console.ReadLine();
                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    return 0;
                }
                if (!int.TryParse(ingreso, out var codigo))
                {
                    Console.WriteLine("No ha ingresado un CODIGO valido. Ingrese nuevamente: ");
                    continue;
                }
                if (codigo < 1)
                {
                    Console.WriteLine("Debe ser un numero entero positivo. Ingrese nuevamente: ");
                    continue;
                }
                if (PlanDeCuentas.Existe(codigo))
                {
                    Console.WriteLine("El Codigo indicado ya existe en el Plan de Cuentas. Ingrese nuevamente: ");
                    continue;
                }
                return codigo;
            } while (true) ;
        }
        private static int IngresarCodigoBuscar(bool obligatorio = true)
        {
            var titulo = "Ingrese el código de la cuenta";
            if (!obligatorio)
            {
                titulo += " o presione [Enter] para continuar: ";
            }
            else
            {
                titulo += ": ";
            }
            do
            {
                Console.Write(titulo);
                var ingreso = Console.ReadLine();
                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    return 0;
                }
                if (!int.TryParse(ingreso, out var codigo))
                {
                    Console.WriteLine("No ha ingresado un CODIGO valido. Ingrese nuevamente: ");
                    continue;
                }
                if (codigo < 1)
                {
                    Console.WriteLine("Debe ser un numero entero positivo. Ingrese nuevamente: ");
                    continue;
                }
                return codigo;
            } while (true);
        }
        private static string IngresoNombre(bool obligatorio = true)
        {
            string ingreso;
            string titulo = "Ingrese el nombre de la cuenta";
            if (!obligatorio)
            {
                titulo += " o presione [Enter] para continuar: ";
            }
            else
            {
                titulo += ": ";
            }
            do
            {
                Console.Write(titulo);
                ingreso = Console.ReadLine();
                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    return null;
                }
                if (obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("Debe ingresar un valor.");
                    continue;
                }
                if (ingreso.Any(Char.IsDigit))
                {
                    Console.WriteLine("El valor ingresado no debe contener numeros.");
                    continue;
                }
                break;
            } while (true);
            return ingreso;
        }
        private static string IngresoTipo(bool obligatorio = true)
        {
            string ingreso;
            string titulo = "Ingrese el tipo de la cuenta";
            if (!obligatorio)
            {
                titulo += " o presione [Enter] para continuar: ";
            }
            else
            {
                titulo += ": ";
            }
            do
            {
                Console.Write(titulo);
                ingreso = Console.ReadLine();
                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    return null;
                }
                if (obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("Debe ingresar un valor.");
                    continue;
                }
                if (ingreso.Any(Char.IsDigit))
                {
                    Console.WriteLine("El valor ingresado no debe contener numeros.");
                    continue;
                }
                if (ingreso != "Activo" && ingreso != "Pasivo" && ingreso != "PatrimonioNeto")
                {
                    Console.WriteLine("El valor ingresado debe ser de un tipo de cuenta valido, y debe respetar correctamente las mayusculas y minusculas (Activo - Pasivo - PatrimonioNeto).");
                    continue;
                }
                break;
            } while (true);
            return ingreso;
        }           
        public bool CoincideCon(Cuenta modelo)
        {
            if (modelo is null)
            {
                return false;
            }
            else
            {
                if (modelo.Codigo != 0 && Codigo != modelo.Codigo)
                {
                    return false;
                }
                if (!string.IsNullOrWhiteSpace(modelo.Nombre) && !Nombre.Equals(modelo.Nombre, StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }
                if (!string.IsNullOrWhiteSpace(modelo.Tipo) && !Tipo.Equals(modelo.Tipo, StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }
                return true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace A891609.Actividad03
{
    class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;
            do
            {
                string opc = "X";
                MostrarMenu();
                opc = Validador.pedirStrNoVacio("Ingrese una opción");
                switch (opc)
                {
                    case "1":
                        AltaCuenta();
                        break;
                    case "2":
                        ModificarCuenta();
                        break;
                    case "3":
                        BajaCuenta();
                        break;
                    case "4":
                        MostrarCuenta();
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("No se ha ingresado una opción correcta");
                        break;
                }

            } while (!salir);
        }

        private static void AltaCuenta()
        {
            var cuenta = Cuenta.IngresarNueva();
            PlanDeCuentas.AgregarCuenta(cuenta);
        }
        private static void BajaCuenta()
        {
            var cuenta = PlanDeCuentas.SeleccionarCuenta();
            if (cuenta is null)
            {
                return;
            }
            cuenta.Mostrar();
            Console.WriteLine($"Se dará de baja la cuenta {cuenta.DescBreve}. Desea confirmar? (S/N)");
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.S)
            {
                PlanDeCuentas.EliminarCuenta(cuenta);
            }
        }
        private static void ModificarCuenta()
        {
            var cuenta = PlanDeCuentas.SeleccionarCuenta();
            if (cuenta is null)
            {
                return;
            }
            cuenta.Mostrar();
            cuenta.Modificar();
        }
        private static void MostrarCuenta()
        {
            var cuenta = PlanDeCuentas.SeleccionarCuenta();
            if (cuenta is null)
            {
                return;
            }
            cuenta.Mostrar();
        }
        private static void MostrarMenu()
        {
            Console.WriteLine("MENÚ");
            Console.WriteLine("Acciones vinculadas al plan de cuentas");
            Console.WriteLine("- - - -");
            Console.WriteLine("1 - Alta");
            Console.WriteLine("2 - Modificación");
            Console.WriteLine("3 - Baja");
            Console.WriteLine("4 - Busqueda");
            Console.WriteLine("0 - Salir");
        }
    }
}

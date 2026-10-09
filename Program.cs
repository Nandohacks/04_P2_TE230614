//Ejercicio 4

namespace ControlCargaBateriaApp
{
    // ==========================================
    // CLASE: CargadorBateria
    // Encargada de gestionar la lógica del ciclo
    // de carga progresiva de la batería.
    // ==========================================
    public class CargadorBateria
    {
        // --------------------------------------
        // PROPIEDADES
        // --------------------------------------
        public double VoltajeInicial { get; private set; }
        public double IncrementoPorCiclo { get; private set; }
        public double VoltajeActual { get; private set; }

        // Límite máximo de carga especificado
        public double VoltajeMaximo { get; } = 12.6;

        // --------------------------------------
        // MÉTODOS
        // --------------------------------------

        /// <summary>
        /// Configura los parámetros iniciales del proceso de carga.
        /// </summary>
        /// <param name="vInicial">Voltaje inicial introducido</param>
        /// <param name="vIncremento">Incremento por ciclo</param>
        public void ConfigurarCarga(double vInicial, double vIncremento)
        {
            VoltajeInicial = vInicial;
            IncrementoPorCiclo = vIncremento;
            VoltajeActual = vInicial;
        }

        /// <summary>
        /// Ejecuta el proceso de carga mediante un ciclo while hasta alcanzar 12.6 V.
        /// Garantiza no superar el límite y previene ciclos infinitos.
        /// </summary>
        public void SimularCarga()
        {
            int ciclo = 0;

            Console.WriteLine("\n=================================================");
            Console.WriteLine("        PROCESO DE CARGA EN EJECUCIÓN");
            Console.WriteLine("=================================================");
            Console.WriteLine($"Voltaje Inicial : {VoltajeActual:F2} V");
            Console.WriteLine($"Incremento/Ciclo: {IncrementoPorCiclo:F2} V");
            Console.WriteLine($"Límite Máximo   : {VoltajeMaximo:F2} V");
            Console.WriteLine("-------------------------------------------------");

            // Si la batería ya está cargada desde el inicio
            if (VoltajeActual >= VoltajeMaximo)
            {
                VoltajeActual = VoltajeMaximo;
                Console.WriteLine($"La batería ya se encuentra completamente cargada ({VoltajeActual:F2} V).");
                return;
            }

            // Ciclo while: Mientras el voltaje sea menor a 12.6 V
            while (VoltajeActual < VoltajeMaximo)
            {
                ciclo++;
                VoltajeActual += IncrementoPorCiclo;

                // Si el último incremento supera el límite, se ajusta a 12.6 V
                if (VoltajeActual > VoltajeMaximo)
                {
                    VoltajeActual = VoltajeMaximo;
                }

                // Salida con número de ciclo y voltaje actualizado incluyendo la unidad (V)
                Console.WriteLine($"Ciclo #{ciclo:D2} | Voltaje acumulado: {VoltajeActual:F2} V");
            }

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"CARGA COMPLETADA EXITOSAMENTE EN {ciclo} CICLO(S).");
            Console.WriteLine($"Voltaje Final Alcanzado: {VoltajeActual:F2} V");
            Console.WriteLine("=================================================\n");
        }
    }

    // ==========================================
    // CLASE PRINCIPAL: Program
    // Punto de entrada de la aplicación de consola.
    // ==========================================
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Configurar codificación para consola
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // --------------------------------------
            // CREACIÓN DEL OBJETO
            // --------------------------------------
            CargadorBateria miCargador = new CargadorBateria();

            Console.WriteLine("=================================================");
            Console.WriteLine("    SISTEMA DE CONTROL DE CARGA DE BATERÍA");
            Console.WriteLine("=================================================");

            // --- Captura y Validación de Voltaje Inicial ---
            double vInicial = 0;
            bool vInicialValido = false;

            while (!vInicialValido)
            {
                Console.Write("Ingrese el voltaje inicial de la batería (V): ");
                if (double.TryParse(Console.ReadLine(), out vInicial))
                {
                    if (vInicial >= 0)
                    {
                        vInicialValido = true;
                    }
                    else
                    {
                        Console.WriteLine("  [ERROR] El voltaje inicial no puede ser negativo.");
                    }
                }
                else
                {
                    Console.WriteLine("  [ERROR] Ingrese un valor numérico válido.");
                }
            }

            // --- Captura y Validación de Incremento por Ciclo ---
            // Validación clave para evitar ciclos infinitos (vIncremento debe ser > 0)
            double vIncremento = 0;
            bool vIncrementoValido = false;

            while (!vIncrementoValido)
            {
                Console.Write("Ingrese el incremento de voltaje por ciclo (V): ");
                if (double.TryParse(Console.ReadLine(), out vIncremento))
                {
                    if (vIncremento > 0)
                    {
                        vIncrementoValido = true;
                    }
                    else
                    {
                        Console.WriteLine("  [ERROR] El incremento debe ser mayor que cero para evitar un ciclo infinito.");
                    }
                }
                else
                {
                    Console.WriteLine("  [ERROR] Ingrese un valor numérico válido.");
                }
            }

            // Cargar configuración e iniciar simulación
            miCargador.ConfigurarCarga(vInicial, vIncremento);
            miCargador.SimularCarga();

            Console.WriteLine("Presione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
using BibliotecaITM.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaITM.Domain
{
    public class Programa
    {
        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("---Biblioteca ITM---\n");

            // --- Encargados disponibles ---
            var encargados = new List<Encargado>
            {
                new Encargado("1036668706", "Daniel Jimenez Correa", "danieljimenez208573@correo.itm.edu.co", "Administrador"),
                new Encargado("1111111", "Daniel Jaramillo Rivera", "danieljaramillo263299@correo.itm.edu.co", "Bibliotecario")
            };

        
            Console.WriteLine("Elige al encargado (1 o 2):");
            for (int i = 0; i < encargados.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {encargados[i].Nombre} - {encargados[i].Cargo}");
            }

            int opcionEncargado = int.Parse(Console.ReadLine() ?? "1");
            var encargadoActivo = encargados[opcionEncargado - 1];
            Console.WriteLine($"\nEncargado seleccionado: {encargadoActivo}\n");

            // --- Crear lista de socios ---

            //Creamos una lista de socios iniciales
            var socios = new List<Socio>()
            {
                new Socio("1001112325", "Juan Pérez", "juanperez@mail.com"),
                new Socio("100223352", "Ana Gómez", "anagomez@mail.com"),
                new Socio("100344445", "Luis Torres", "luistorres@mail.com")
            };

            // --- Libros disponibles ---

            //Creamos una lista de libros iniciales
            var libros = new List<Libro>
            {
                new Libro("978-1234567890", "Un mundo feliz", "Aldous Huxley", 1932, CategoriaLibro.Academico, 3),
                new Libro("978-9876543210", "Fahrenheit 451", "Ray Bradbury", 1953, CategoriaLibro.Poemas, 2),
                new Libro("978-1111111111", "Nosotros", "Yevgeny Zamyatin", 1924, CategoriaLibro.Matematicas, 1)
            };

            // --- Crear lista de prestamos  ---

            //Creamos una lista de prestamos iniciales
            var prestamos = new List<Prestamo>()
            {
                new Prestamo(socios[0], libros[0]),

            };

            // --- Servicio de préstamos ---
            var procesador = new ProcesadorPrestamos();

            // --- Menú principal ---
            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\nMenú principal:");
                Console.WriteLine("1. Registrar socio");
                Console.WriteLine("2. Mostrar Socios");
                Console.WriteLine("3. Mostrar libros disponibles");
                Console.WriteLine("4. Registrar préstamo");
                Console.WriteLine("5. Registrar devolución");
                Console.WriteLine("6. Pagar multa");
                Console.WriteLine("7. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Documento: ");
                        string doc = Console.ReadLine() ?? "";
                        Console.Write("Nombre: ");
                        string nom = Console.ReadLine() ?? "";
                        Console.Write("Email: ");
                        string email = Console.ReadLine() ?? "";

                        try
                        {
                            var socio = new Socio(doc, nom, email);
                            socios.Add(socio);
                            Console.WriteLine($"Socio registrado con exito: {socio}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "2":
                        Console.WriteLine("\nSocios registrados");
                        foreach (var socio in socios)
                            Console.WriteLine(socio);
                        break;

                    case "3":
                        Console.WriteLine("\nLibros en la biblioteca:");
                        foreach (var libro in libros)
                            Console.WriteLine(libro);
                        break;

                    case "4":
                        if (socios.Count == 0)
                        {
                            Console.WriteLine("Alerta, no hay socios registrados.");
                            break;
                        }

                        Console.WriteLine("\nSeleccione un socio:");
                        for (int i = 0; i < socios.Count; i++)
                            Console.WriteLine($"{i + 1}. {socios[i]}");

                        int socioIdx = int.Parse(Console.ReadLine() ?? "1") - 1;
                        var socioPrestamo = socios[socioIdx];

                        Console.WriteLine("\nSeleccione un libro:");
                        for (int i = 0; i < libros.Count; i++)
                            Console.WriteLine($"{i + 1}. {libros[i]}");

                        int libroIdx = int.Parse(Console.ReadLine() ?? "1") - 1;
                        var libroPrestamo = libros[libroIdx];

                        try
                        {
                            var prestamo = new Prestamo(socioPrestamo, libroPrestamo);
                            var resultadoPrestamo = procesador.RegistrarPrestamo(prestamo);
                            Console.WriteLine(resultadoPrestamo.Message);
                            if (resultadoPrestamo.Ok)
                                prestamos.Add(prestamo);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al registrar préstamo: {ex.Message}");
                        }
                        break;

                    case "5":
                        if (prestamos.Count == 0)
                        {
                            Console.WriteLine("No hay préstamos registrados.");
                            break;
                        }

                        Console.WriteLine("\nSeleccione préstamo a devolver:");
                        for (int i = 0; i < prestamos.Count; i++)
                            Console.WriteLine($"{i + 1}. {prestamos[i]}");

                        int prestamoIdx = int.Parse(Console.ReadLine() ?? "1") - 1;
                        var prestamoDev = prestamos[prestamoIdx];

                        var resultadoDev = procesador.RegistrarDevolucion(prestamoDev);
                        Console.WriteLine(resultadoDev.Message);
                        break;

                    case "6":
                        if (socios.Count == 0)
                        {
                            Console.WriteLine("⚠ No hay socios registrados.");
                            break;
                        }

                        Console.WriteLine("\nSeleccione socio para pagar multa:");
                        for (int i = 0; i < socios.Count; i++)
                            Console.WriteLine($"{i + 1}. {socios[i]}");

                        int socioMultaIdx = int.Parse(Console.ReadLine() ?? "1") - 1;
                        var socioMulta = socios[socioMultaIdx];

                        var resultadoPago = procesador.PagarMulta(socioMulta);
                        Console.WriteLine(resultadoPago.Message);
                        break;

                    case "7":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opción inválida, elige una opción correcta");
                        break;
                }
            }

            Console.WriteLine("\nGracias por usar la Biblioteca ITM");
        }
    }
}
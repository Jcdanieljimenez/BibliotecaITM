using BibliotecaITM.Services;
using System;
using System.Collections.Generic;
using System.IO;
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
                new Encargado("1020417253", "Daniel Jaramillo Rivera", "danieljaramillo263299@correo.itm.edu.co", "Bibliotecario")
            };

        
            Console.WriteLine("Elige al encargado (1 o 2):");
            for (int i = 0; i < encargados.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {encargados[i].Nombre} - {encargados[i].Cargo}");
            }

            int opcionEncargado = int.Parse(Console.ReadLine() ?? "1");
            var encargadoActivo = encargados[opcionEncargado - 1];
            Console.WriteLine($"\nEncargado seleccionado: {encargadoActivo}\n");

            // --- llamar Socios.txt desde la clase ArchivosLS ---
            var socios = ArchivosLS.CargarSocios();


            // --- llamar libros.txt desde la clase ArchivosLS ---
            var libros = ArchivosLS.CargarLibros();

            //Validar que los txt donde se encuentran los socios y libros no esten vacios o la ruta sea incorrecta

            if (socios.Count == 0 || libros.Count == 0)
            {
                Console.WriteLine("Advertencia, No hay socios o libros cargados. Verifica los archivos TXT.");
                return;
            }


            // --- Crear lista de prestamos  ---

            //Creamos una lista de prestamos iniciales
            var prestamos = new List<Prestamo>()
            {
                new Prestamo(socios[0], libros[1]), // ejemplo de que el Socio en la posición 0 presta el libro en la posición 1, desde consola, en este caso el usuario 1001112325,Juan Pérez prestó "Fahrenheit 451"

            };

            // --- Servicio de préstamos ---
            var procesador = new ProcesadorPrestamos();

            // --- Menú principal ---
            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\nMenú principal:");                
                Console.WriteLine("1. Mostrar Socios");
                Console.WriteLine("2. Mostrar libros disponibles");
                Console.WriteLine("3. Registrar socio");
                Console.WriteLine("4. Registrar libro");
                Console.WriteLine("5. Registrar préstamo");
                Console.WriteLine("6. Registrar devolución");
                Console.WriteLine("7. Pagar multa");
                Console.WriteLine("8. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {                  

                    case "1":
                        Console.WriteLine("\nSocios registrados");
                        foreach (var socio in socios)
                            Console.WriteLine(socio);
                        break;

                    case "2":
                        Console.WriteLine("\nLibros en la biblioteca:");
                        foreach (var libro in libros)
                            Console.WriteLine(libro);
                        break;

                    case "3":
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
                            ArchivosLS.GuardarSocio(socio); // lo guarda en Socios.txt
                            Console.WriteLine($"Socio registrado con exito: {socio}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "4":
                        Console.Write("ISBN: ");
                        string isbn = Console.ReadLine() ?? "";

                        Console.Write("Título: ");
                        string titulo = Console.ReadLine() ?? "";

                        Console.Write("Autor: ");
                        string autor = Console.ReadLine() ?? "";

                        Console.Write("Año de publicación: ");
                        int anio = int.Parse(Console.ReadLine() ?? "0");

                        Console.WriteLine("Categoría (0=Academico, 1=Poemas, 2=Matematicas, 3=Historia, 4=Novela, 5=Infantil):");
                        int catIdx = int.Parse(Console.ReadLine() ?? "0");
                        var categoria = (CategoriaLibro)catIdx;

                        Console.WriteLine("Tipo de libro (0=Fisico, 1=Digital):");
                        int tipoIdx = int.Parse(Console.ReadLine() ?? "0");
                        var tipo = (TipoLibro)tipoIdx;

                        int copias = 1;
                        if (tipo == TipoLibro.Fisico)
                        {
                            Console.Write("Número de copias: ");
                            copias = int.Parse(Console.ReadLine() ?? "1");
                        }

                        try
                        {
                            var libro = new Libro(isbn, titulo, autor, anio, categoria, tipo, copias);
                            libros.Add(libro);
                            ArchivosLS.GuardarLibro(libro); // 🔹 Guarda en libros.txt
                            Console.WriteLine($"Libro registrado con éxito: {libro}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al registrar libro: {ex.Message}");
                        }
                        break;

                    case "5":
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

                    case "6":
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

                    case "7":
                        if (socios.Count == 0)
                        {
                            Console.WriteLine("Alerta, no hay socios registrados.");
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

                    case "8":
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
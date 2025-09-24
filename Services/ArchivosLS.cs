using BibliotecaITM.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaITM.Services
{
    public static class ArchivosLS
    {
        // Rutas de los archivos
        private const string RutaLibros = "Data/Libros.txt";
        private const string RutaSocios = "Data/Socios.txt";

        // ------------------------
        // Metodo para encargar libros desde un archivo de texto
        // ------------------------
        public static List<Libro> CargarLibros()
        {
            var libros = new List<Libro>();

            if (!File.Exists(RutaLibros))
            {
                Console.WriteLine($"Alerta, no se encontró el archivo {RutaLibros}");
                return libros;
            }

            foreach (var linea in File.ReadAllLines(RutaLibros))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;

                try
                {
                    var datos = linea.Split(',');
                    if (datos.Length < 7) continue;

                    string isbn = datos[0];
                    string titulo = datos[1];
                    string autor = datos[2];
                    int anio = int.Parse(datos[3]);
                    CategoriaLibro categoria = Enum.Parse<CategoriaLibro>(datos[4]);
                    TipoLibro tipo = Enum.Parse<TipoLibro>(datos[5]);
                    int stock = int.Parse(datos[6]);

                    libros.Add(new Libro(isbn, titulo, autor, anio, categoria, tipo, stock));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al leer libro: {ex.Message}");
                }
            }

            return libros;
        }

        // --- Metodo de guardar libros ---
        public static void GuardarLibro(Libro libro)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(RutaLibros)!);
                File.AppendAllText(RutaLibros, $"{libro.ISBN},{libro.Titulo},{libro.Autor},{libro.Anio},{libro.Categoria},{libro.Tipo},{libro.TotalCopias},{Environment.NewLine}");  // Agrega una nueva línea al archivo
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar libro: {ex.Message}");
            }
        }

        
        // ------------------------
        // Metodo para cargar socios desde un archivo de texto
        // ------------------------
        public static List<Socio> CargarSocios()
        {
            var socios = new List<Socio>();

            if (!File.Exists(RutaSocios))
            {
                Console.WriteLine($"Alerta, no se encontró el archivo {RutaSocios}");
                return socios;
            }

            foreach (var linea in File.ReadAllLines(RutaSocios))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;

                try
                {
                    var datos = linea.Split(',');
                    if (datos.Length < 3) continue;

                    string documento = datos[0];
                    string nombre = datos[1];
                    string email = datos[2];

                    socios.Add(new Socio(documento, nombre, email));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al leer socio: {ex.Message}");
                }
            }

            return socios;
        }

        // --- Metodo de guardar Socio ---
        public static void GuardarSocio(Socio socio)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(RutaSocios)!);
                File.AppendAllText(RutaSocios, $"{socio.Documento},{socio.Nombre},{socio.Email}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar socio: {ex.Message}");
            }
        }
    }
}

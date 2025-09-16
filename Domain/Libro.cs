using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaITM.Domain
{
    // Enumeración para limitar las categorías de los libros
    public enum CategoriaLibro
    {
        Academico,
        Poemas,
        Matematicas,
        Historia,
        Novela,
        Infantil
    }
    public class Libro
    {
        // Propiedades del libro
        public string ISBN { get; }  // Solo lectura, se asigna en el constructor
        public string Titulo { get; private set; }
        public string Autor { get; private set; }
        public int Anio { get; private set; }
        public CategoriaLibro Categoria { get; private set; }

        public int TotalCopias { get; private set; }
        public int CopiasDisponibles { get; private set; }

        // Constructor
        public Libro(string isbn, string titulo, string autor, int anio, CategoriaLibro categoria, int totalCopias)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("El ISBN no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("El título no puede estar vacío.");

            if (totalCopias < 1)
                throw new ArgumentException("Debe haber al menos una copia del libro.");

            ISBN = isbn.Trim();
            Titulo = titulo.Trim();
            Autor = string.IsNullOrWhiteSpace(autor) ? "Desconocido" : autor.Trim();
            Anio = anio;
            Categoria = categoria;
            TotalCopias = totalCopias;
            CopiasDisponibles = totalCopias; // Al inicio todas están disponibles
        }

        // Método para prestar un libro
        public bool Prestar()
        {
            if (CopiasDisponibles <= 0)
                return false; // No se puede prestar si no hay disponibles

            CopiasDisponibles--;
            return true;
        }

        // Método para devolver un libro
        public void Devolver()
        {
            if (CopiasDisponibles < TotalCopias)
                CopiasDisponibles++;
        }

        // Representación en texto del libro
        public override string ToString() =>
            $"{Titulo} - {Autor} ({Anio}) [{Categoria}] - Disponibles: {CopiasDisponibles}/{TotalCopias}";
    }
}

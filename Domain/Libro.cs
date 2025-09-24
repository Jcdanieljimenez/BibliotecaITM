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

    // Determinar tipo de libro

    public enum TipoLibro
    {
        Fisico,
        Digital
    }

    public class Libro
    {
        // Propiedades del libro
        public string ISBN { get; }  // Solo lectura, se asigna en el constructor
        public string Titulo { get; private set; } 
        public string Autor { get; private set; }
        public int Anio { get; private set; }
        public CategoriaLibro Categoria { get; private set; }

        public TipoLibro Tipo { get; private set; }
        public int TotalCopias { get; private set; }
        public int CopiasDisponibles { get; private set; }

        // Constructor
        public Libro(string isbn, string titulo, string autor, int anio, CategoriaLibro categoria, TipoLibro tipo, int totalCopias)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("El ISBN no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("El título no puede estar vacío.");

            if (tipo == TipoLibro.Fisico && totalCopias < 1)
                throw new ArgumentException("Los libros físicos deben tener al menos una copia.");

            ISBN = isbn.Trim();
            Titulo = titulo.Trim();
            Autor = string.IsNullOrWhiteSpace(autor) ? "Desconocido" : autor.Trim();
            Anio = anio;
            Categoria = categoria;
            Tipo = tipo;

            if (tipo == TipoLibro.Fisico)
            {
                TotalCopias = totalCopias;
                CopiasDisponibles = totalCopias;
            }
            else
            {
                // Para digitales, stock "infinito"
                TotalCopias = int.MaxValue;
                CopiasDisponibles = int.MaxValue;
            }
        }

        public bool Prestar()
        {
            if (Tipo == TipoLibro.Digital)
                return true; // Siempre disponible

            if (CopiasDisponibles <= 0)
                return false;

            CopiasDisponibles--;
            return true;
        }

        public void Devolver()
        {
            if (Tipo == TipoLibro.Fisico && CopiasDisponibles < TotalCopias)
                CopiasDisponibles++;
            // Para digitales no aplica devolución (opcionalmente podrías dejarlo vacío)
        }

        public override string ToString() =>
            Tipo == TipoLibro.Fisico
                ? $"{Titulo} - {Autor} ({Anio}) [{Categoria}] - Disponibles: {CopiasDisponibles}/{TotalCopias}"
                : $"{Titulo} - {Autor} ({Anio}) [{Categoria}] - [Digital: acceso ilimitado]";
    
    }
}

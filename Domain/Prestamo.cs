using BibliotecaITM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaITM.Domain
{
    public class Prestamo
    {
        // --- Propiedades ---
        public Socio Socio { get; }
        public Libro Libro { get; }

        public DateTime FechaPrestamo { get; }
        public DateTime FechaVencimiento { get; }
        public DateTime? FechaDevolucion { get; private set; } // Puede ser null si no se ha devuelto

        public bool Activo => FechaDevolucion == null; // Un préstamo está activo si no se devolvió

        // --- Constructor ---
        public Prestamo(Socio socio, Libro libro, int diasPrestamo = 7)
        {
            if (socio == null)
                throw new ArgumentNullException(nameof(socio));
            if (libro == null)
                throw new ArgumentNullException(nameof(libro));

            // Intentamos prestar el libro
            if (!libro.Prestar())
                throw new InvalidOperationException("No hay copias disponibles para prestar.");

            Socio = socio;
            Libro = libro;
            FechaPrestamo = DateTime.Now;
            FechaVencimiento = FechaPrestamo.AddDays(diasPrestamo);
            FechaDevolucion = null;
        }

        // --- Métodos ---

        //Metodo para devolver el libro
        public Result Devolver()
        {
            if (!Activo)
                return Result.Fail("El préstamo ya fue cerrado.");

            Libro.Devolver();
            FechaDevolucion = DateTime.Now;

            if (FechaDevolucion > FechaVencimiento)
                return Result.Fail("El libro fue devuelto con retraso.");

            return Result.Success("Libro devuelto a tiempo.");
        }

        public override string ToString()
        {
            string estado = Activo ? "Activo" : "Cerrado";
            return $"Préstamo de '{Libro.Titulo}' a {Socio.Nombre} ({estado}) " +
                   $"- Prestado: {FechaPrestamo:d}, Vence: {FechaVencimiento:d}, Devuelto: {FechaDevolucion?.ToString("d") ?? "No"}";
        }
    }
}

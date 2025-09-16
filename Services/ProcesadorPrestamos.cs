using BibliotecaITM.Common;
using BibliotecaITM.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaITM.Services
{
    public class ProcesadorPrestamos
    {
        // Método para registrar un préstamo
        public Result RegistrarPrestamo(Prestamo prestamo)
        {
            if (!prestamo.Socio.Estado)
                return Result.Fail("El socio no está activo.");

            return Result.Success($"Préstamo registrado: {prestamo}");
        }


        // Método para registrar devolución
        public Result RegistrarDevolucion(Prestamo prestamo)
        {
            var resultado = prestamo.Devolver();

            // Si devolvió con retraso, calculamos multa
            if (!resultado.Ok && prestamo.FechaDevolucion.HasValue)
            {
                decimal multa = Penalizacion.CalcularMulta(prestamo.FechaVencimiento, prestamo.FechaDevolucion.Value);
                prestamo.Socio.AgregarPenalizacion(multa);
                return Result.Fail($"Libro devuelto con retraso. Multa aplicada: {multa:C}");
            }

            return resultado;
        }

        // Método para pagar una multa
        public Result PagarMulta(Socio socio)
        {
            if (socio.Penalizacion <= 0)
                return Result.Fail("El socio no tiene multas pendientes.");

            socio.PagarPenalizacion();
            return Result.Success("Multa pagada en su totalidad.");
        }
    }
}

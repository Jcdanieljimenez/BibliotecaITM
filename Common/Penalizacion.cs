using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaITM.Common
{
    public static class Penalizacion
    {
        // Valor fijo por día de retraso
        public const decimal MultaPorDia = 1000m; // en pesos o la moneda que uses

        // Método que calcula la multa según el retraso
        public static decimal CalcularMulta(DateTime fechaVencimiento, DateTime fechaDevolucion)
        {
            if (fechaDevolucion <= fechaVencimiento)
                return 0m; // No hay multa

            int diasRetraso = (fechaDevolucion - fechaVencimiento).Days;
            return diasRetraso * MultaPorDia;
        }
    }
}

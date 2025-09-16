using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaITM.Common
{
    public class Result
    {
        // Propiedades
        public bool Ok { get; } // Solo lectura, indica si la operacion fue exitosa
        public string Message { get; } // Solo lectura, mensaje asociado al resultado

        // Constructor privado para forzar el uso de los metodos estaticos

        private Result(bool ok, string message)
        {
            Ok = ok;
            Message = message;
        }
        // Metodo Success
        public static Result Success(string msg = "OK") => new(true, msg); // Metodo estatico para crear un resultado Success 
        // Metodo Fail
        public static Result Fail(string msg) => new(false, msg); // Metodo estatico para crear un resultado Fail
    }
}
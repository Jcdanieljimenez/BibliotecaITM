using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaITM.Domain
{
    public class Socio : Persona
    {
        // Propiedad Email
        public string Email { get; private set; } // Solo lectura fuera de la clase, pero modificable dentro de la clase

        // Propiedad Estado (Activa/Inactiva)
        public bool Estado { get; private set; } = true; // Por defecto, el cliente esta activo

        // Propiedad penalizacion/multas
        public decimal Penalizacion { get; private set; } = 0m; // Por defecto, sin penalizacion


        // Constructor

        public Socio(string documento, string nombre, string email) : base(documento, nombre)
        {
            // Inicializacion del email
            Email = string.IsNullOrWhiteSpace(email) ? "sin-correo@na" : email.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("El email no es valido.");

        }

        // Métodos de negocio
        
        public void Suspender() => Estado = false; // Suspender el socio
        public void Reactivar() => Estado = true; // Reactivar el socio

        //Agregar penalizacion
        public void AgregarPenalizacion(decimal monto)
        {
            if (monto <= 0) return; // No se agregan penalizaciones negativas o cero
            Penalizacion += monto;
        }

        //Pagar penalizacion

        public void PagarPenalizacion()
        {
            Penalizacion = 0;
        }

        public override string ToString() => $"{base.ToString()} - {(Estado ? "Activo" : "Inactivo")}, Penalización: {Penalizacion:C}";
    }
}

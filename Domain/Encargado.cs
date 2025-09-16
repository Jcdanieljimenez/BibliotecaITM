using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaITM.Domain
{
    public class Encargado : Persona
    {
        // Propiedad Correo Electronico
        public string Email { get; private set; } // Solo lectura fuera de la clase, pero solo modificable dentro de la clase

        // Propiedad Puesto o Cargo
        public string Cargo { get; private set; } // Solo lectura fuera de la clase, pero solo modificable dentro de la clase

        // Propiedad estado

        public bool Estado { get; private set; } = true; // Por defecto, el encargado esta activo

        // Constructor

        public Encargado(string documento, string nombre, string email, string cargo) : base(documento, nombre)
        {
            //Inicializacion del cargo
            Cargo = string.IsNullOrWhiteSpace(cargo) ? "Empleado" : cargo.Trim();

            // Inicializacion del email
            Email = string.IsNullOrWhiteSpace(email) ? "sin-correo@na" : email.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("El email no es valido.");
        }

        // Métodos de negocio

        //Activar o desactivar encargado
        public void Suspender() => Estado = false;
        public void Reactivar() => Estado = true;

        public override string ToString()
            => $"{Nombre} ({Documento}) - Cargo: {Cargo}, Estado: {(Estado ? "Activo" : "Inactivo")}";
    }
}

    


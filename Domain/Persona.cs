using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaITM.Domain
{
    public abstract class Persona
    {
        // Propiedades autoimplementadas
        // Encapsulamiento: uso de propiedades para controlar el acceso a los campos
        public string Documento { get; } // Solo lectura, se asigna en el constructor   
        public string Nombre { get; private set; } // Solo lectura fuera de la clase, pero solo modificable dentro de la clase

        //Constructor protegido para que solo las clases derivadas puedan instanciar

        protected Persona(string documento, string nombre)
        {
            if (string.IsNullOrWhiteSpace(documento))
                throw new ArgumentException("El documento no puede estar vacio.");
           
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacio.");
            Documento = documento;
            Nombre = nombre.Trim();
        }

        // Metodo para cambiar el nombre

        public void CambiarNombre(string nuevoNombre)
        {
            if (string.IsNullOrWhiteSpace(nuevoNombre)) return;
            Nombre = nuevoNombre.Trim();
        }

        //Metodo sobrescrito para representar la persona como cadena ToString()

        public override string ToString() => $"{Nombre} ({Documento})";
    }
}

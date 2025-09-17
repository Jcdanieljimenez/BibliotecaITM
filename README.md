# BibliotecaITM
Biblioteca POO con enfoques en prestamos y devoluciones 

**Problema:**
La biblioteca estudiantil ITM gestiona sus libros y préstamos de forma manual (libros de registro en papel y hojas Excel). Esto causa errores frecuentes: libros mal localizados, control deficiente de copias prestadas y vencidas, retrasos en la devolución, poca trazabilidad de usuarios, y dificultad para conocer la disponibilidad en tiempo real. Además no hay un registro claro de reservas ni de sanciones por retrasos.

Usuarios:
Los usuarios principales son:
**Socios:** personas registradas que desean consultar, prestar o devolver libros.

**Encargados:** personal de la biblioteca que gestiona los préstamos y devoluciones.


**El valor esperado del sistema es:**
Controlar préstamos y devoluciones de libros físicos y digitales.

Evitar que se presten más ejemplares de los que existen (control de stock).

Permitir a los encargados visualizar la disponibilidad de libros.

Facilitar el registro histórico de préstamos y devoluciones.

Biblioteca: Libro (abstracto) → Físico/Digital, Socio, Prestamo, IPoliticaPrestamo.

---------------------------------------------------------------------------------------------------------

**Historias de usuario (6 con criterios Given/When/Then)**

1. Registro de socio
Given que un nuevo usuario llega a la biblioteca,
When el encargado registra sus datos,
Then el sistema debe almacenar al socio como activo.


2. Consulta de disponibilidad
Given que un socio desea un libro,
When consulta su disponibilidad,
Then el sistema debe mostrar cuántos ejemplares físicos hay disponibles o si está en formato digital.


3. Préstamo de libro físico
Given que un socio solicita un libro físico,
When hay ejemplares disponibles,
Then el sistema debe registrar el préstamo y descontar 1 unidad del stock disponible.


4. Préstamo de libro digital
Given que un socio solicita un libro digital,
When realiza el préstamo,
Then el sistema debe registrar el préstamo sin afectar stock.


5.Devolución de libro físico
Given que un socio devuelve un libro,
When el encargado registra la devolución,
Then el sistema debe aumentar en 1 el stock disponible de ese libro.

6.Control de vencimiento
Given que un préstamo supera la fecha de devolución,
When el encargado consulta,
Then el sistema debe marcar el préstamo como vencido y calcular la multa correspondiente.

---------------------------------------------------------------------------------------------------------

**Requerimientos de negocio (5)**
1. El sistema debe permitir registrar socios y libros (físicos o digitales).

2. El sistema debe controlar la disponibilidad de libros físicos mediante stock.

3. El sistema debe registrar y consultar préstamos activos e históricos.

4. El sistema debe calcular penalizaciones por retrasos en devoluciones.

5. El sistema debe ser operado por personal autorizado de la biblioteca.



**4. Requerimientos funcionales (8)**
1. Registrar y modificar datos de socios.

2. Registrar y modificar datos de libros (físicos y digitales).

3. Consultar la disponibilidad de libros.

4. Realizar préstamos de libros físicos y digitales.

5. Registrar devoluciones de libros físicos.

6. Calcular penalizaciones automáticas al registrar devoluciones fuera de plazo.

7. Consultar historial de préstamos por socio.

8. Generar un log básico de operaciones (registro de acciones de préstamo y devolución).

**5. Requerimientos no funcionales (6)**
1.Rendimiento: El sistema debe registrar y consultar préstamos en menos de 1 segundo con hasta 100 libros y socios.

2. Usabilidad: Los menús en consola deben ser claros y guiar al usuario paso a paso.

3. Manejo de errores: Validar entradas incorrectas (ej. cédula repetida, stock agotado, montos inválidos) y mostrar mensajes explicativos.

4. Escalabilidad: El diseño debe permitir agregar nuevos tipos de libros (ej. audiolibros) o reglas sin reescribir todo el código.

5. Calidad del código: Mantener principios de POO (encapsulación, clases separadas por responsabilidad).

6. Portabilidad: Ejecutarse en Windows o Linux con .NET 8 sin configuraciones extra.


**6. Modelo conceptual (texto con relaciones)**

Entidades principales:
**Libro**
Atributos: LibroID (PK), ISBN, Título, Autor, Año, Categoría, Tipo (Digital/Físico), TotalCopias, CopiasDisponibles
Relación: 1 ibro — 0..N Préstamos


**Personas (abstracta)**
Atributos: Nombre, Documento

* Socio (hereda de Persona)
Atributos: SocioID(PK), Correo, Estado, Penalización
Relación: 1 Socio — 0..N Préstamos

* Encargado (hereda de Person)
Atributos: EncargadoID (PK), Correo, Estado
Relación: encargado autoriza conceptual préstamos y devoluciones.


**Préstamo**

Atributos: PrestamoID (PK), LibrosID (FK), SocioID (FK), FechaPrestamo, FechaVencimiento, FechaDevolucion, Sanción
Relación: vincula Libro y Socio. 
Relación: 1 socio - 0..N Préstamos.
Relación: 1 Libro - 0..N Prestamos



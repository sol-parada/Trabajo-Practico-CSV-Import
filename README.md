# Trabajo-Practico-CSV-Import

## Descripción

**Trabajo-Practico-CSV-Import** es una herramienta desarrollada en C# y .NET para la importación masiva de datos desde archivos CSV a una base de datos PostgreSQL. Está pensada para procesar grandes volúmenes de información, como el caso de uso de cargar 2.5 millones de registros de usuarios/alumnos en la tabla `alumnos` de la base de datos `DEV_SYSACAD`.

El proyecto implementa una arquitectura por capas, separando las responsabilidades de acceso a datos, lógica de aplicación y dominio. Utiliza la biblioteca `CsvHelper` para el procesamiento eficiente de archivos CSV y el paquete `Npgsql` para la comunicación con PostgreSQL. El proceso de importación está optimizado mediante el uso de inserciones en lote (batch insert) para mejorar el rendimiento y la eficiencia de los recursos.

## Características principales

- **Importación masiva** de registros desde archivos CSV.
- Optimización mediante **inserciones por lotes** (por defecto, lotes de 8000 registros).
- Manejo eficiente de recursos y transacciones para asegurar la integridad de los datos.
- Configuración flexible mediante archivos de configuración y variables de entorno.
- Separación clara de capas: Dominio, Aplicación, Infraestructura.

## Estructura del proyecto

```
Trabajo-Practico-CSV-Import/
│
├── TrabajoPracticoCSV/
│   ├── Application/
│   │   └── CsvImporter.cs          # Lógica de importación desde CSV
│   ├── Domain/
│   │   └── Alumno.cs               # Modelo de datos 'Alumno'
│   ├── Infrastructure/
│   │   ├── AlumnoRepository.cs     # Acceso y escritura a base de datos
│   │   └── DbConnectionFactory.cs  # Generador de conexiones a PostgreSQL
│   └── Program.cs                  # Punto de entrada y configuración general
│
├── appsettings.json                # Configuración de la aplicación
└── README.md                       # (Este archivo)
```

## Instalación

> **Requisitos:**
> - .NET 6.0 o superior
> - PostgreSQL disponible y accesible
> - Archivo CSV con los datos a importar
> - Variables de entorno configuradas para la conexión a la base de datos

1. **Clona el repositorio:**
   ```bash
   git clone https://github.com/sol-parada/Trabajo-Practico-CSV-Import.git
   cd Trabajo-Practico-CSV-Import
   ```

   Asegúrate también de que el archivo `appsettings.json` tenga la cadena de conexión adecuada usando las variables de entorno.

2. **Coloca tu archivo CSV en la ruta especificada en `appsettings.json`:**

   ```json
   {
     "CsvFilePath": "ruta/alumnos.csv",
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=${POSTGRES_PORT};Database=${POSTGRES_DB};Username=${POSTGRES_USER};Password=${POSTGRES_PASSWORD}"
     }
   }
   ```

3. **Restaura los paquetes y compila el proyecto:**

   ```bash
   dotnet restore
   dotnet build
   ```

4. **Ejecuta la importación:**

   ```bash
   dotnet run --project TrabajoPracticoCSV/TrabajoPracticoCSV
   ```

   El programa mostrará el progreso y la cantidad de registros importados.


```

## Modelo de datos (`alumnos`)

La clase `Alumno` corresponde a la estructura de cada registro en el CSV y en la tabla destino:

- **apellido**: string
- **nombre**: string
- **nro_documento**: string
- **tipo_documento**: string
- **fecha_nacimiento**: DateTime
- **sexo**: string
- **nro_legajo**: int
- **fecha_ingreso**: DateTime


## Autor

- Sol Parada

---

> Proyecto desarrollado como práctica de importación eficiente de datos masivos en C# y PostgreSQL.

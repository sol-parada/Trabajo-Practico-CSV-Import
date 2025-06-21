# Trabajo Práctico: Importador de CSV en C#

Este repositorio contiene una aplicación de consola en C# que importa datos desde un archivo CSV a una base de datos PostgreSQL. El proyecto está preparado para ser evaluado fácilmente por el docente, con instrucciones claras para su ejecución tanto en entorno local como a través de Docker.

---

## Estructura del proyecto

```
TrabajoPracticoCSV/
└── TrabajoPracticoCSV/
    ├── Application/         # Lógica de aplicación
    ├── Data/
    │   └── alumnos.csv      # Archivo CSV de ejemplo
    ├── Domain/              # Entidades de dominio
    ├── Infra/
    │   ├── .env             # Variables de entorno (configuración de la base)
    │   └── docker-compose.yml # Stack PostgreSQL listo para usar
    ├── Infrastructure/      # Acceso a datos
    ├── Program.cs           # Entrada principal de la app
    ├── TrabajoPracticoCSV.csproj # Proyecto C#
    ├── appsettings.json     # Configuración de conexión y archivo CSV
    └── Properties/
```

---

## Requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) o superior
- [Docker](https://www.docker.com/) **(opcional, recomendado para pruebas rápidas)**
- PostgreSQL (si no usas Docker, debe estar disponible y accesible)

---

## Primeros pasos rápidos

### 1. Clonar el repositorio

```sh
git clone https://github.com/sol-parada/Trabajo-Practico-CSV-Import.git
cd Trabajo-Practico-CSV-Import/TrabajoPracticoCSV/TrabajoPracticoCSV
```

### 2. Configurar la base de datos

#### Opción A: Con Docker (lo más simple)

1. Edita el archivo `Infra/.env` si quieres cambiar usuario, contraseña o nombre de la base de datos.
2. Ejecuta:

    ```sh
    docker compose -f Infra/docker-compose.yml up -d
    ```

   Esto levantará una base de datos PostgreSQL lista para usar con los valores del `.env`.

#### Opción B: Manual (sin Docker)

- Crea una base de datos PostgreSQL y agrega las credenciales en el archivo `Infra/.env`:

```
POSTGRES_USER=usuario
POSTGRES_PASSWORD=contraseña
POSTGRES_DB=trabajo
POSTGRES_PORT=5432
```

---

### 3. Configuración de la aplicación

- El archivo `appsettings.json` define la cadena de conexión (usando variables de entorno) y la ruta al CSV:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=${POSTGRES_PORT};Database=${POSTGRES_DB};Username=${POSTGRES_USER};Password=${POSTGRES_PASSWORD}"
  },
  "CsvFilePath": "Data/alumnos.csv"
}
```

Asegúrate de que el archivo `Data/alumnos.csv` exista y tenga el formato correcto.

---

### 4. Ejecutar la aplicación

**Rápido desde consola:**

```sh
dotnet restore
dotnet run
```

La app cargará la configuración, conectará a la base PostgreSQL y comenzará a importar el archivo CSV. Al finalizar, mostrará cuántos registros se importaron y el tiempo de ejecución.

---

## Personalización

- Para importar otro archivo, cambia la ruta en `appsettings.json` (`CsvFilePath`).
- Puedes ajustar la configuración de la base de datos en el archivo `.env` o directamente en `appsettings.json`.

---

## Dependencias principales

- CsvHelper (lectura eficiente de CSV)
- Dapper (acceso a datos micro-ORM)
- Npgsql (conector PostgreSQL)
- DotNetEnv (variables de entorno)
- Microsoft.Extensions.Configuration (configuración flexible)

Las dependencias se restauran automáticamente con `dotnet restore`.

---

## Archivos clave

- `Program.cs` - Lógica de arranque, carga de configuración y ejecución del importador.
- `appsettings.json` - Configuración de conexión y CSV.
- `Infra/.env` - Variables utilizadas en la cadena de conexión.
- `Infra/docker-compose.yml` - Stack Docker para PostgreSQL.
- `Data/alumnos.csv` - Archivo de ejemplo para importar.

---

## Notas para el docente

- El código está documentado y modularizado en carpetas por responsabilidad.
- El proceso de importación es asincrónico y eficiente (usa Stopwatch para medir el tiempo).
- Se puede probar en cualquier sistema operativo compatible con .NET y Docker.

---

## Autor

Sol Parada

---

¡Listo para corregir! Ante cualquier consulta, estoy a disposición.

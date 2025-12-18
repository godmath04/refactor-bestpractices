# Guía de Implementación de Base de Datos

## Estado Actual del Sistema

### Sistema Funcional Sin Base de Datos

El sistema de gestión de vehículos está completamente funcional **sin necesidad de base de datos**. Utiliza almacenamiento en memoria que permite probar toda la funcionalidad mientras el equipo de base de datos prepara el esquema.

**Fecha de Inicio:** Proyecto configurado para funcionar sin BD
**Razón:** El equipo de base de datos aún está preparando el esquema
**Solución:** Patrón Repository con implementación en memoria

---

## Arquitectura Actual

### Patrón Repository Implementado

```
IVehicleRepository (Interfaz - Contrato)
    |
    ├── MyVehiclesRepository (ACTIVA - Almacenamiento en memoria)
    |   └── VehicleCollection (Singleton thread-safe)
    |
    └── DBVehicleRepository (PREPARADA - Pendiente de implementación)
        └── Plantilla lista para cuando BD esté disponible
```

### Componentes Clave

#### 1. IVehicleRepository
- **Ubicación:** `Repositories/IVehicleRepository.cs`
- **Propósito:** Define el contrato para operaciones CRUD
- **Métodos:**
  - `GetVehicles()` - Obtener todos los vehículos
  - `AddVehicle(Vehicle)` - Agregar nuevo vehículo
  - `Find(Guid id)` - Buscar vehículo por ID

#### 2. MyVehiclesRepository (EN USO)
- **Ubicación:** `Repositories/MyVehiclesRepository.cs`
- **Propósito:** Implementación en memoria
- **Almacenamiento:** VehicleCollection (Singleton)
- **Ventajas:**
  - No requiere configuración de BD
  - Ideal para desarrollo y pruebas
  - Rápido y simple
- **Limitaciones:**
  - Datos se pierden al reiniciar
  - No persistente
  - No apto para producción

#### 3. DBVehicleRepository (PREPARADA)
- **Ubicación:** `Repositories/DBVehicleRepository.cs`
- **Propósito:** Implementación con base de datos
- **Estado:** Plantilla documentada lista para implementar
- **Contiene:** Comentarios TODO con ejemplos de código

#### 4. VehicleCollection (Singleton)
- **Ubicación:** `Infraestructure/Singletons/VehicleCollection.cs`
- **Propósito:** Almacenamiento compartido en memoria
- **Características:**
  - Thread-safe usando Lazy&lt;T&gt;
  - Única instancia en toda la aplicación
  - Persiste durante la sesión

---

## Cómo Funciona Actualmente

### Flujo de Datos (Sin Base de Datos)

```
1. Usuario hace clic "Add Mustang"
   ↓
2. HomeController.AddMustang()
   ↓
3. FordMustangCreator.Create() → Crea objeto Car
   ↓
4. InitializeVehicleWithFuel() → Agrega combustible inicial
   ↓
5. _vehicleRepository.AddVehicle(vehicle)
   ↓
6. MyVehiclesRepository.AddVehicle()
   ↓
7. VehicleCollection.Instance.Vehicles.Add(vehicle)
   ↓
8. Vehículo almacenado EN MEMORIA
   ↓
9. Redirect a Index → Muestra vehículos
```

### Ventajas del Diseño Actual

1. **Separación de Responsabilidades**
   - HomeController no sabe si usa memoria o BD
   - Cambio transparente entre implementaciones

2. **Facilidad de Pruebas**
   - No requiere BD para desarrollo
   - Tests unitarios más rápidos
   - Fácil reset de datos (reiniciar app)

3. **Preparado para el Futuro**
   - DBVehicleRepository ya existe como plantilla
   - Cambio requiere solo 1 línea de código
   - Interfaz permanece estable

---

## Preparación para Base de Datos

### Archivos Preparados

#### ServicesConfiguration.cs
Configurado con tres opciones comentadas:

```csharp
// OPCIÓN 1: Repositorio en memoria (ACTUAL - EN USO)
services.AddTransient<IVehicleRepository, MyVehiclesRepository>();

// OPCIÓN 2: Repositorio de base de datos (FUTURO)
// Descomentar cuando el equipo de BD complete el esquema:
// services.AddTransient<IVehicleRepository, DBVehicleRepository>();

// OPCIÓN 3: Configuración dinámica basada en appsettings.json (AVANZADO)
// Permite cambiar entre memoria y BD sin recompilar
```

#### DBVehicleRepository.cs
Plantilla completa con:
- Comentarios TODO detallados
- Ejemplos de implementación
- Notas sobre mejores prácticas
- Sugerencias de métodos adicionales

#### Startup.cs
Incluye comentarios sobre dónde agregar DbContext:

```csharp
// TODO: Cuando la base de datos esté lista, descomentar:
// services.AddDbContext<VehicleDbContext>(options =>
//     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
```

---

## Cuando la Base de Datos Esté Lista

### Checklist de Migración

#### Fase 1: Preparación (Equipo de BD)
- [ ] Definir esquema de base de datos
- [ ] Crear tabla Vehicles con campos necesarios
- [ ] Configurar índices (ID, Brand, VehicleType)
- [ ] Preparar cadena de conexión
- [ ] Probar conectividad

#### Fase 2: Configuración (Desarrollo)
- [ ] Instalar paquetes NuGet necesarios
- [ ] Crear VehicleDbContext
- [ ] Configurar cadena de conexión en appsettings.json
- [ ] Crear entidad Vehicle para EF Core
- [ ] Configurar mapeo en DbContext

#### Fase 3: Implementación (Desarrollo)
- [ ] Implementar DBVehicleRepository.GetVehicles()
- [ ] Implementar DBVehicleRepository.AddVehicle()
- [ ] Implementar DBVehicleRepository.Find()
- [ ] Agregar manejo de errores de BD
- [ ] Implementar transacciones si es necesario

#### Fase 4: Migración (DevOps)
- [ ] Crear migraciones de Entity Framework
- [ ] Ejecutar migraciones en base de datos
- [ ] Verificar esquema creado correctamente
- [ ] Agregar datos de prueba (opcional)

#### Fase 5: Activación (Configuración)
- [ ] Descomentar DBVehicleRepository en ServicesConfiguration
- [ ] Comentar MyVehiclesRepository
- [ ] Actualizar appsettings.json con cadena de conexión
- [ ] Reiniciar aplicación

#### Fase 6: Pruebas (QA)
- [ ] Probar agregar vehículos
- [ ] Probar listar vehículos
- [ ] Probar buscar vehículo
- [ ] Probar operaciones de motor y combustible
- [ ] Verificar persistencia entre reinicios
- [ ] Probar manejo de errores de BD

---

## Implementación Paso a Paso

### Paso 1: Instalar Paquetes NuGet

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 3.1.32
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 3.1.32
dotnet add package Microsoft.EntityFrameworkCore.Design --version 3.1.32
```

### Paso 2: Crear DbContext

**Archivo:** `Data/VehicleDbContext.cs`

```csharp
using Best_Practices.Models;
using Microsoft.EntityFrameworkCore;

namespace Best_Practices.Data
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Brand).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Model).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Color).IsRequired().HasMaxLength(50);

                // Discriminador para herencia (Car vs Motorcycle)
                entity.HasDiscriminator<string>("VehicleType")
                    .HasValue<Car>("Car")
                    .HasValue<Motorcycle>("Motorcycle");
            });
        }
    }
}
```

### Paso 3: Configurar Cadena de Conexión

**Archivo:** `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=VehicleManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "UseDatabase": false,
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  }
}
```

### Paso 4: Implementar DBVehicleRepository

**Archivo:** `Repositories/DBVehicleRepository.cs`

```csharp
using Best_Practices.Data;
using Best_Practices.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Best_Practices.Repositories
{
    public class DBVehicleRepository : IVehicleRepository
    {
        private readonly VehicleDbContext _context;

        public DBVehicleRepository(VehicleDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));

            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public Vehicle Find(Guid id)
        {
            return _context.Vehicles.Find(id);
        }

        public ICollection<Vehicle> GetVehicles()
        {
            return _context.Vehicles
                .OrderBy(v => v.Brand)
                .ThenBy(v => v.Model)
                .ToList();
        }
    }
}
```

### Paso 5: Actualizar Startup.cs

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();

    // Agregar DbContext
    services.AddDbContext<VehicleDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

    // Registrar servicios de aplicación
    services.AddApplicationServices(Configuration);
}
```

### Paso 6: Actualizar ServicesConfiguration.cs

```csharp
public static IServiceCollection AddApplicationServices(
    this IServiceCollection services,
    IConfiguration configuration = null)
{
    // Cambiar a repositorio de base de datos
    services.AddTransient<IVehicleRepository, DBVehicleRepository>();

    // Comentar repositorio en memoria
    // services.AddTransient<IVehicleRepository, MyVehiclesRepository>();

    return services;
}
```

### Paso 7: Crear y Ejecutar Migraciones

```bash
# Crear migración inicial
dotnet ef migrations add InitialCreate

# Aplicar migración a base de datos
dotnet ef database update
```

---

## Esquema de Base de Datos Recomendado

### Tabla: Vehicles

```sql
CREATE TABLE Vehicles (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    VehicleType NVARCHAR(50) NOT NULL,      -- 'Car' o 'Motorcycle'
    Brand NVARCHAR(100) NOT NULL,
    Model NVARCHAR(100) NOT NULL,
    Color NVARCHAR(50) NOT NULL,
    Tires INT NOT NULL,
    Gas FLOAT NOT NULL DEFAULT 0,
    FuelLimit FLOAT NOT NULL,
    IsEngineOn BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),

    CONSTRAINT CK_Vehicles_Gas CHECK (Gas >= 0),
    CONSTRAINT CK_Vehicles_FuelLimit CHECK (FuelLimit > 0),
    CONSTRAINT CK_Vehicles_Tires CHECK (Tires > 0)
);

-- Índices para mejor rendimiento
CREATE INDEX IX_Vehicles_VehicleType ON Vehicles(VehicleType);
CREATE INDEX IX_Vehicles_Brand ON Vehicles(Brand);
CREATE INDEX IX_Vehicles_IsEngineOn ON Vehicles(IsEngineOn);

-- Índice compuesto para consultas comunes
CREATE INDEX IX_Vehicles_Brand_Model ON Vehicles(Brand, Model);
```

---

## Estrategias de Migración

### Opción A: Cambio Directo (Simple)
1. Implementar DBVehicleRepository
2. Cambiar ServicesConfiguration
3. Reiniciar aplicación
4. Todo funciona con BD

**Ventajas:** Simple y directo
**Desventajas:** No hay rollback fácil

### Opción B: Configuración Dinámica (Recomendado)
1. Implementar DBVehicleRepository
2. Usar appsettings.json para controlar qué repositorio usar
3. Permite cambio sin recompilar

**Código:**
```csharp
var useDatabase = configuration.GetValue<bool>("UseDatabase", false);

if (useDatabase)
    services.AddTransient<IVehicleRepository, DBVehicleRepository>();
else
    services.AddTransient<IVehicleRepository, MyVehiclesRepository>();
```

**appsettings.json:**
```json
{
  "UseDatabase": false  // Cambiar a true para activar BD
}
```

**Ventajas:**
- Cambio sin recompilar
- Fácil rollback
- Útil para diferentes entornos

### Opción C: Feature Flag (Avanzado)
Usar sistema de feature flags para control granular en producción.

---

## Consideraciones Importantes

### 1. Modelo de Datos

**Herencia en Entity Framework:**
- Vehicle es clase abstracta
- Car y Motorcycle heredan de Vehicle
- Usar Table-Per-Hierarchy (TPH) con discriminador
- Discriminador: columna VehicleType ('Car', 'Motorcycle')

### 2. Propiedades Inmutables

El modelo actual tiene propiedades read-only:
- Id, Brand, Model, Color son inmutables

**En BD:** Asegurar que estos campos no se actualicen después de insert:
```csharp
entity.Property(e => e.Brand).ValueGeneratedNever();
```

### 3. Engine State

`_isEngineOn` es campo privado. Para BD, necesita mapearse:

**Opción A:** Agregar propiedad de respaldo:
```csharp
public bool IsEngineOn { get; private set; }
```

**Opción B:** Usar backing field de EF Core:
```csharp
entity.Property<bool>("_isEngineOn")
    .HasField("_isEngineOn")
    .UsePropertyAccessMode(PropertyAccessMode.Field);
```

### 4. Transacciones

Para operaciones que modifican múltiples entidades:
```csharp
using (var transaction = _context.Database.BeginTransaction())
{
    try
    {
        _context.Vehicles.Add(vehicle);
        _context.SaveChanges();
        transaction.Commit();
    }
    catch
    {
        transaction.Rollback();
        throw;
    }
}
```

### 5. Async/Await

Para mejor rendimiento en producción:
```csharp
public async Task<ICollection<Vehicle>> GetVehiclesAsync()
{
    return await _context.Vehicles.ToListAsync();
}
```

**Nota:** Requiere cambiar IVehicleRepository a métodos async.

---

## Testing

### Test con Memoria (Actual)
```csharp
[Fact]
public void AddVehicle_Should_Store_In_Memory()
{
    var repo = new MyVehiclesRepository();
    var car = new Car("Red", "Ford", "Mustang");

    repo.AddVehicle(car);
    var vehicles = repo.GetVehicles();

    Assert.Contains(car, vehicles);
}
```

### Test con BD (Futuro)
```csharp
[Fact]
public async Task AddVehicle_Should_Persist_To_Database()
{
    var options = new DbContextOptionsBuilder<VehicleDbContext>()
        .UseInMemoryDatabase(databaseName: "TestDb")
        .Options;

    using (var context = new VehicleDbContext(options))
    {
        var repo = new DBVehicleRepository(context);
        var car = new Car("Red", "Ford", "Mustang");

        repo.AddVehicle(car);

        Assert.Equal(1, await context.Vehicles.CountAsync());
    }
}
```

---

## Troubleshooting

### Problema: "No se puede conectar a la base de datos"
**Solución:**
1. Verificar cadena de conexión en appsettings.json
2. Verificar que SQL Server está corriendo
3. Verificar permisos de usuario

### Problema: "Entity type 'Vehicle' is abstract"
**Solución:**
Configurar discriminador correctamente en OnModelCreating.

### Problema: "Datos no persisten"
**Solución:**
1. Verificar que está usando DBVehicleRepository
2. Verificar que SaveChanges() se llama
3. Verificar que no hay excepciones silenciosas

### Problema: "Migration failed"
**Solución:**
```bash
# Eliminar migración
dotnet ef migrations remove

# Crear nueva migración
dotnet ef migrations add InitialCreate

# Forzar update
dotnet ef database update --force
```

---

## Resumen

### Estado Actual
- Sistema 100% funcional sin base de datos
- Usa almacenamiento en memoria (MyVehiclesRepository)
- Patrón Repository permite cambio transparente

### Cuando BD Esté Lista
- Implementar DBVehicleRepository (plantilla preparada)
- Cambiar 1 línea en ServicesConfiguration
- Código del controlador NO cambia
- Interfaz permanece estable

### Ventaja Principal
**Principio de Inversión de Dependencias (DIP):**
- HomeController depende de IVehicleRepository (abstracción)
- NO depende de MyVehiclesRepository o DBVehicleRepository (concretos)
- Cambio de implementación es transparente

---

**Próximo Paso:** Esperar confirmación del equipo de base de datos
**Impacto del Cambio:** Mínimo (solo configuración DI + implementación de repositorio)
**Tiempo Estimado:** 2-4 horas una vez que BD esté lista

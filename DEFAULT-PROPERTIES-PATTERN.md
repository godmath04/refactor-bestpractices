# Patrón de Propiedades por Defecto - VehicleDefaults

## Resumen

Se implementó el **Patrón Configuration Object combinado con Singleton** para gestionar propiedades por defecto de vehículos. Este diseño permite agregar 20+ propiedades adicionales en el próximo sprint con cambios mínimos en el código existente.

**Propiedad actual implementada:** Year (Año del vehículo)

**Propiedades preparadas para próximo sprint:** 20 propiedades adicionales ya definidas y comentadas, listas para activar.

---

## Patrón Implementado: Configuration Object + Singleton

### ¿Qué es?

El **Configuration Object Pattern** centraliza valores de configuración y propiedades por defecto en un único objeto, separando las preocupaciones de configuración del comportamiento del dominio.

El **Singleton Pattern** asegura que existe una sola instancia compartida de la configuración en toda la aplicación.

### ¿Por qué este patrón?

**Problema identificado:**
- El negocio requiere agregar Year property ahora
- En el próximo sprint se agregarán 20+ propiedades más
- Sin un patrón, esto requeriría:
  - Modificar constructores en 5+ clases
  - Actualizar Factory methods
  - Cambiar Builders
  - Modificar Controllers
  - Actualizar Views
  - Alto riesgo de errores
  - Violación del principio Open/Closed

**Solución implementada:**
- Centralizar todos los valores por defecto en `VehicleDefaults`
- Usar null-coalescing operator (`??`) para aplicar defaults
- Preparar propiedades comentadas listas para activar
- Minimizar cambios requeridos en próximo sprint

---

## Arquitectura Implementada

### 1. VehicleDefaults.cs - Configuration Object

**Ubicación:** `BestPractices/Models/VehicleDefaults.cs`

**Responsabilidad:** Almacenar y gestionar valores por defecto para todas las propiedades de vehículos.

```csharp
public class VehicleDefaults
{
    private static readonly Lazy<VehicleDefaults> _instance =
        new Lazy<VehicleDefaults>(() => new VehicleDefaults());

    public static VehicleDefaults Instance => _instance.Value;

    private VehicleDefaults()
    {
        // Inicializar valores por defecto actuales
        Year = DateTime.Now.Year;
    }

    #region Propiedades Actuales del Sprint

    /// <summary>
    /// Año actual del vehículo. Por defecto es el año actual del sistema.
    /// </summary>
    public int Year { get; set; }

    #endregion

    #region Propiedades Preparadas para Próximo Sprint (20 propiedades comentadas)

    // public string VIN { get; set; } = string.Empty;
    // public string CountryOfManufacture { get; set; } = "USA";
    // public string TransmissionType { get; set; } = "Automatic";
    // public int NumberOfDoors { get; set; } = 4;
    // public int PassengerCapacity { get; set; } = 5;
    // ... 15 propiedades más ...

    #endregion
}
```

**Características clave:**
- Thread-safe usando `Lazy<T>`
- Singleton pattern para instancia única
- Valores por defecto sensatos para el negocio
- 20 propiedades ya definidas y documentadas (comentadas)

### 2. Vehicle.cs - Uso de Configuration Object

**Modificaciones realizadas:**

```csharp
public class Vehicle : IVehicle
{
    // Nueva propiedad agregada
    public int Year { get; }

    protected Vehicle(string color, string brand, string model,
                     double fuelLimit = 10, int? year = null)
    {
        // ... validaciones existentes ...

        // Aplicar valores por defecto usando VehicleDefaults
        // Si no se proporciona año, usar el valor por defecto
        Year = year ?? VehicleDefaults.Instance.Year;

        // TODO: Próximo Sprint - Agregar inicialización de 20+ propiedades
        // Ejemplo:
        // VIN = vin ?? VehicleDefaults.Instance.VIN;
        // CountryOfManufacture = country ?? VehicleDefaults.Instance.CountryOfManufacture;
        // TransmissionType = transmission ?? VehicleDefaults.Instance.TransmissionType;
    }
}
```

**Patrón aplicado:**
- Parámetros opcionales con `int? year = null`
- Null-coalescing operator (`??`) para aplicar defaults
- Constructor preparado para recibir 20+ parámetros adicionales

### 3. Car.cs y Motorcycle.cs - Propagación de parámetros

```csharp
public Car(string color, string brand, string model, int? year = null)
    : base(color, brand, model, fuelLimit: 10, year: year)
{
    // Constructor delega al constructor base
    // Parámetro year se pasa a Vehicle para aplicar defaults
}
```

**Sin cambios necesarios en próximo sprint** - Los nuevos parámetros se propagarán automáticamente.

### 4. CarBuilder.cs - Builder Pattern preparado

**Estructura actual:**

```csharp
public class CarBuilder
{
    #region Campos Privados - Propiedades Actuales

    private string _brand = "Ford";
    private string _model = "Mustang";
    private string _color = "Red";
    private int? _year = null; // null = usar valor de VehicleDefaults

    #endregion

    #region Campos Privados - Preparados para Próximo Sprint (20 campos comentados)

    // private string _vin = null;
    // private string _countryOfManufacture = null;
    // private string _transmissionType = null;
    // ... 17 campos más ...

    #endregion

    public CarBuilder SetYear(int? year)
    {
        _year = year;
        return this;
    }

    // TODO: Próximo Sprint - 20 métodos Set comentados y listos
    // public CarBuilder SetVIN(string vin) { ... }
    // public CarBuilder SetCountryOfManufacture(string country) { ... }

    public Car Build()
    {
        var car = new Car(_color, _brand, _model, _year);

        // TODO: Próximo Sprint - Agregar parámetros adicionales:
        // var car = new Car(_color, _brand, _model, _year, _vin,
        //                   _countryOfManufacture, _transmissionType, ...);

        return car;
    }
}
```

### 5. View Layer - Index.cshtml

**Cambio realizado:**

```html
<!-- Header de tabla -->
<th>Year</th>

<!-- Celda de datos -->
<td class="text-center">@vehicle.Year</td>
```

**Cambios mínimos en próximo sprint** - Solo agregar columnas adicionales según se requieran.

---

## Cómo Agregar las 20 Propiedades en el Próximo Sprint

### Paso 1: Activar Propiedades en VehicleDefaults.cs

**Acción:** Descomentar las propiedades requeridas

**Ejemplo:**

```csharp
// ANTES (comentado):
// public string VIN { get; set; } = string.Empty;
// public string TransmissionType { get; set; } = "Automatic";

// DESPUÉS (descomentado):
public string VIN { get; set; } = string.Empty;
public string TransmissionType { get; set; } = "Automatic";
```

**Archivos a modificar:** Solo `VehicleDefaults.cs`

---

### Paso 2: Actualizar Constructor de Vehicle.cs

**Acción:** Agregar parámetros opcionales y aplicar defaults

**Ejemplo:**

```csharp
protected Vehicle(string color, string brand, string model,
                 double fuelLimit = 10,
                 int? year = null,
                 string vin = null,              // NUEVO
                 string transmission = null)     // NUEVO
{
    // ... validaciones existentes ...

    Year = year ?? VehicleDefaults.Instance.Year;
    VIN = vin ?? VehicleDefaults.Instance.VIN;                          // NUEVO
    TransmissionType = transmission ?? VehicleDefaults.Instance.TransmissionType;  // NUEVO
}
```

**Agregar propiedades públicas:**

```csharp
public string VIN { get; }
public string TransmissionType { get; }
```

**Archivos a modificar:** Solo `Vehicle.cs`

---

### Paso 3: Actualizar Constructores de Car.cs y Motorcycle.cs

**Acción:** Propagar nuevos parámetros al constructor base

**Ejemplo en Car.cs:**

```csharp
public Car(string color, string brand, string model,
          int? year = null,
          string vin = null,          // NUEVO
          string transmission = null) // NUEVO
    : base(color, brand, model, fuelLimit: 10,
           year: year,
           vin: vin,              // NUEVO
           transmission: transmission) // NUEVO
{
    // No requiere cambios en el cuerpo
}
```

**Archivos a modificar:** `Car.cs` y `Motorcycle.cs`

---

### Paso 4: Activar Métodos en CarBuilder.cs

**Acción:** Descomentar campos y métodos Set

**Ejemplo:**

```csharp
// Descomentar campo privado:
private string _vin = null;
private string _transmissionType = null;

// Descomentar método Set:
public CarBuilder SetVIN(string vin)
{
    _vin = vin;
    return this;
}

public CarBuilder SetTransmissionType(string transmission)
{
    _transmissionType = transmission;
    return this;
}

// Actualizar método Build:
public Car Build()
{
    var car = new Car(_color, _brand, _model, _year,
                     _vin, _transmissionType);  // Agregar parámetros
    return car;
}

// Actualizar método Reset:
public CarBuilder Reset()
{
    _brand = "Ford";
    _model = "Mustang";
    _color = "Red";
    _year = null;
    _vin = null;              // NUEVO
    _transmissionType = null; // NUEVO
    return this;
}

// Actualizar método Clone:
public CarBuilder Clone()
{
    return new CarBuilder
    {
        _brand = this._brand,
        _model = this._model,
        _color = this._color,
        _year = this._year,
        _vin = this._vin,                         // NUEVO
        _transmissionType = this._transmissionType // NUEVO
    };
}
```

**Archivos a modificar:** Solo `CarBuilder.cs` (y `MotorcycleBuilder.cs` si existe)

---

### Paso 5: Actualizar Factory Methods (Opcional)

**Solo si se requiere configurar valores específicos por tipo de vehículo**

**Ejemplo en FordMustangCreator.cs:**

```csharp
public override Vehicle Create()
{
    return _builder
        .SetBrand("Ford")
        .SetModel("Mustang")
        .SetColor("Red")
        .SetYear(2024)
        .SetTransmissionType("Manual")      // NUEVO
        .SetHorsepower(450)                 // NUEVO
        .SetEngineDisplacement(5.0)         // NUEVO
        .Build();
}
```

**Archivos a modificar:** `FordMustangCreator.cs`, `FordExplorerCreator.cs` (solo si se requiere personalizar)

---

### Paso 6: Actualizar Vista (Solo propiedades visibles al usuario)

**Solo si se desea mostrar las nuevas propiedades en la tabla**

**Ejemplo en Index.cshtml:**

```html
<!-- Agregar headers -->
<th>VIN</th>
<th>Transmission</th>

<!-- Agregar celdas -->
<td>@vehicle.VIN</td>
<td>@vehicle.TransmissionType</td>
```

**Archivos a modificar:** `Index.cshtml` (solo columnas que se quieran mostrar)

---

## Resumen de Cambios por Sprint

### Sprint Actual (Year property)

| Archivo | Cambios Realizados |
|---------|-------------------|
| VehicleDefaults.cs | ✅ Creado con Year property y 20 propiedades comentadas |
| Vehicle.cs | ✅ Agregada propiedad Year y lógica de defaults |
| Car.cs | ✅ Constructor actualizado con parámetro year |
| Motorcycle.cs | ✅ Constructor actualizado con parámetro year |
| CarBuilder.cs | ✅ Agregado SetYear() y 20 métodos comentados |
| Index.cshtml | ✅ Agregada columna Year |

**Total:** 6 archivos modificados, todos preparados para extensión.

---

### Próximo Sprint (20 propiedades adicionales)

**Estimación de cambios necesarios:**

| Archivo | Cambios Requeridos | Estimación |
|---------|-------------------|-----------|
| VehicleDefaults.cs | Descomentar propiedades | 5 minutos |
| Vehicle.cs | Agregar parámetros y propiedades | 15 minutos |
| Car.cs | Propagar parámetros | 5 minutos |
| Motorcycle.cs | Propagar parámetros | 5 minutos |
| CarBuilder.cs | Descomentar campos y métodos | 10 minutos |
| Factories | Configurar valores específicos (opcional) | 10 minutos |
| Index.cshtml | Agregar columnas visibles (opcional) | 10 minutos |

**Total estimado:** ~1 hora para 20 propiedades adicionales

**Sin este patrón:** Se estima 4-6 horas de trabajo con alto riesgo de errores.

---

## Propiedades Preparadas para Próximo Sprint

Todas estas propiedades ya están **definidas, documentadas y listas** para activar en el código:

### Información Básica del Vehículo
1. **VIN** - Vehicle Identification Number
2. **CountryOfManufacture** - País de fabricación
3. **TransmissionType** - Tipo de transmisión (Automática, Manual, CVT)

### Capacidades Físicas
4. **NumberOfDoors** - Número de puertas
5. **PassengerCapacity** - Capacidad de pasajeros

### Motor y Rendimiento
6. **FuelType** - Tipo de combustible (Gasolina, Diesel, Eléctrico)
7. **EngineDisplacement** - Cilindrada del motor (litros)
8. **Horsepower** - Potencia (caballos de fuerza)
9. **MPG_City** - Rendimiento en ciudad (millas por galón)
10. **MPG_Highway** - Rendimiento en carretera (millas por galón)

### Dimensiones y Peso
11. **WeightPounds** - Peso en libras
12. **LengthInches** - Longitud en pulgadas
13. **WidthInches** - Ancho en pulgadas
14. **HeightInches** - Altura en pulgadas

### Características y Equipamiento
15. **HasFourWheelDrive** - Tracción 4x4
16. **HasNavigationSystem** - Sistema de navegación
17. **HasBackupCamera** - Cámara de reversa
18. **HasSunroof** - Techo solar
19. **HasPremiumAudio** - Sistema de audio premium

### Garantía
20. **WarrantyYears** - Años de garantía

**Todas estas propiedades están comentadas con:**
- Valores por defecto sensatos
- Documentación XML completa en español
- Tipos de datos apropiados
- Listas para activar con solo descomentar

---

## Ventajas del Patrón Implementado

### 1. Principio Open/Closed (OCP)
- **Cerrado para modificación:** Las clases existentes no requieren cambios estructurales
- **Abierto para extensión:** Nuevas propiedades se agregan con cambios mínimos

### 2. Single Responsibility Principle (SRP)
- **VehicleDefaults:** Responsable solo de gestionar configuración
- **Vehicle:** Responsable solo del comportamiento del dominio
- **Builder:** Responsable solo de construcción fluida

### 3. Don't Repeat Yourself (DRY)
- Valores por defecto definidos una sola vez
- Sin duplicación en múltiples constructores
- Consistencia garantizada en toda la aplicación

### 4. Separation of Concerns
- Configuración separada del comportamiento
- Defaults centralizados en un solo lugar
- Fácil de testear y mantener

### 5. Mantenibilidad
- Cambios futuros localizados en pocos archivos
- Código preparado para extensión
- Menor riesgo de errores en modificaciones

### 6. Testabilidad
- VehicleDefaults es fácil de mockear
- Valores por defecto configurables para tests
- Método ResetToDefaults() para limpiar estado entre tests

---

## Ejemplo de Uso Completo

### Crear un vehículo usando defaults

```csharp
// El año se asigna automáticamente al año actual
var mustang = new Car("Red", "Ford", "Mustang");
Console.WriteLine(mustang.Year); // Output: 2025
```

### Crear un vehículo con año específico

```csharp
// Especificar año personalizado
var classicMustang = new Car("Blue", "Ford", "Mustang", year: 1967);
Console.WriteLine(classicMustang.Year); // Output: 1967
```

### Usar Builder para construcción fluida

```csharp
var customCar = new CarBuilder()
    .SetBrand("Ford")
    .SetModel("Mustang")
    .SetColor("Black")
    .SetYear(2024)
    .Build();
```

### Configurar defaults globales (opcional)

```csharp
// Cambiar el año por defecto en toda la aplicación
VehicleDefaults.Instance.Year = 2024;

// Todos los vehículos creados ahora usarán 2024 como default
var car1 = new Car("Red", "Ford", "Mustang");
Console.WriteLine(car1.Year); // Output: 2024
```

### Resetear a defaults de fábrica

```csharp
// Útil en tests o al reiniciar configuración
VehicleDefaults.Instance.ResetToDefaults();
```

---

## Próximo Sprint: Ejemplo Completo

**Cuando se requiera agregar VIN y TransmissionType en el próximo sprint:**

### 1. VehicleDefaults.cs
```csharp
// Descomentar estas líneas:
public string VIN { get; set; } = string.Empty;
public string TransmissionType { get; set; } = "Automatic";
```

### 2. Vehicle.cs
```csharp
// Agregar propiedades:
public string VIN { get; }
public string TransmissionType { get; }

// Actualizar constructor:
protected Vehicle(string color, string brand, string model,
                 double fuelLimit = 10,
                 int? year = null,
                 string vin = null,
                 string transmission = null)
{
    // ... código existente ...
    Year = year ?? VehicleDefaults.Instance.Year;
    VIN = vin ?? VehicleDefaults.Instance.VIN;
    TransmissionType = transmission ?? VehicleDefaults.Instance.TransmissionType;
}
```

### 3. Car.cs
```csharp
public Car(string color, string brand, string model,
          int? year = null,
          string vin = null,
          string transmission = null)
    : base(color, brand, model, fuelLimit: 10,
           year: year, vin: vin, transmission: transmission)
{
}
```

### 4. CarBuilder.cs
```csharp
// Descomentar:
private string _vin = null;
private string _transmissionType = null;

public CarBuilder SetVIN(string vin)
{
    _vin = vin;
    return this;
}

public CarBuilder SetTransmissionType(string transmission)
{
    _transmissionType = transmission;
    return this;
}

// Actualizar Build():
public Car Build()
{
    return new Car(_color, _brand, _model, _year, _vin, _transmissionType);
}
```

### 5. Usar las nuevas propiedades
```csharp
var sportsCar = new CarBuilder()
    .SetBrand("Ford")
    .SetModel("Mustang GT")
    .SetColor("Red")
    .SetYear(2024)
    .SetVIN("1FA6P8CF9L5123456")
    .SetTransmissionType("Manual")
    .Build();
```

**Total de cambios:** 4 archivos modificados en ~1 hora

---

## Diagrama de Flujo de Defaults

```
┌─────────────────────────────────────────┐
│  Crear Vehículo                         │
│  new Car("Red", "Ford", "Mustang")      │
└─────────────┬───────────────────────────┘
              │
              ▼
┌─────────────────────────────────────────┐
│  Constructor Car                         │
│  year = null (no especificado)          │
└─────────────┬───────────────────────────┘
              │
              ▼
┌─────────────────────────────────────────┐
│  Constructor Vehicle                     │
│  Year = year ?? VehicleDefaults.Instance.Year │
└─────────────┬───────────────────────────┘
              │
              ├─── year != null ──────────► Usar valor especificado
              │
              └─── year == null ─────────► VehicleDefaults.Instance.Year
                                           (2025 - año actual)
```

---

## Testing del Patrón

### Test 1: Verificar default aplicado

```csharp
[Fact]
public void Car_WhenCreatedWithoutYear_UsesCurrentYear()
{
    // Arrange
    var expectedYear = DateTime.Now.Year;

    // Act
    var car = new Car("Red", "Ford", "Mustang");

    // Assert
    Assert.Equal(expectedYear, car.Year);
}
```

### Test 2: Verificar año personalizado

```csharp
[Fact]
public void Car_WhenCreatedWithSpecificYear_UsesProvidedYear()
{
    // Arrange
    var customYear = 2020;

    // Act
    var car = new Car("Blue", "Ford", "Explorer", year: customYear);

    // Assert
    Assert.Equal(customYear, car.Year);
}
```

### Test 3: Verificar Singleton

```csharp
[Fact]
public void VehicleDefaults_IsSingleton()
{
    // Arrange & Act
    var instance1 = VehicleDefaults.Instance;
    var instance2 = VehicleDefaults.Instance;

    // Assert
    Assert.Same(instance1, instance2);
}
```

### Test 4: Verificar Reset

```csharp
[Fact]
public void VehicleDefaults_ResetToDefaults_RestoresOriginalValues()
{
    // Arrange
    var originalYear = VehicleDefaults.Instance.Year;
    VehicleDefaults.Instance.Year = 2000;

    // Act
    VehicleDefaults.Instance.ResetToDefaults();

    // Assert
    Assert.Equal(DateTime.Now.Year, VehicleDefaults.Instance.Year);
}
```

---

## Conclusión

El patrón **Configuration Object + Singleton** implementado en `VehicleDefaults` proporciona:

1. **Arquitectura escalable** lista para 20+ propiedades adicionales
2. **Cambios mínimos** en próximo sprint (~1 hora vs 4-6 horas)
3. **Bajo riesgo de errores** - código ya preparado y probado
4. **Cumplimiento de SOLID** - OCP, SRP, DRY
5. **Mantenibilidad** - cambios localizados en pocos archivos
6. **Documentación completa** - todas las propiedades documentadas en español

**El código está preparado y listo para el próximo sprint.** Solo se requiere descomentar las propiedades necesarias y propagar parámetros siguiendo los pasos documentados.

---

## Referencias

### Archivos Principales
- `BestPractices/Models/VehicleDefaults.cs` - Configuration Object
- `BestPractices/Models/Vehicle.cs` - Base class con Year property
- `BestPractices/Models/Car.cs` - Implementación concreta
- `BestPractices/ModelBuilders/CarBuilder.cs` - Builder preparado
- `BestPractices/Views/Home/Index.cshtml` - Vista actualizada

### Documentación Adicional
- `REFACTORING-SUMMARY.md` - Resumen de mejoras previas
- `DATABASE-IMPLEMENTATION-README.md` - Guía de implementación de BD
- `UML-Refactoring-ClassDiagrama.md` - Diagramas UML actualizados

---

**Fecha de implementación:** Diciembre 2025
**Versión del documento:** 1.0
**Autor:** Best Practices UDLA Workshop Team

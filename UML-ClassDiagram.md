# UML Class Diagram - Best Practices Workshop

## Diagram (Mermaid)

```mermaid
classDiagram
    %% Interfaces
    class IVehicle {
        <<interface>>
        +StartEngine() void
        +StopEngine() void
        +AddGas() void
        +NeedsGas() bool
        +IsEngineOn() bool
    }

    class IVehicleRepository {
        <<interface>>
        +GetVehicles() ICollection~Vehicle~
        +AddVehicle(vehicle) void
        +Find(id) Vehicle
    }

    %% Abstract Classes
    class Vehicle {
        <<abstract>>
        +Guid Id
        +int Tires
        +string Color
        +string Brand
        +string Model
        +int Gas
        +int FuelLimit
        -bool _isEngineOn
        +StartEngine() void
        +StopEngine() void
        +AddGas() void
        +NeedsGas() bool
        +IsEngineOn() bool
    }

    class Creator {
        <<abstract>>
        +Create()* Vehicle
    }

    %% Concrete Vehicle Classes
    class Car {
        +int Tires = 4
        +int FuelLimit = 10
    }

    class Motocycle {
        +int Tires = 2
        +int FuelLimit = 5
    }

    %% Factory Pattern
    class FordMustangCreator {
        +Create() Vehicle
    }

    class FordExplorerCreator {
        +Create() Vehicle
    }

    %% Builder Pattern
    class CarBuilder {
        -string brand = "Ford"
        -string model = "Mustang"
        -string color = "Red"
        +SetBrand(brand) CarBuilder
        +SetModel(model) CarBuilder
        +SetColor(color) CarBuilder
        +Build() Car
    }

    %% Singleton Pattern
    class VehicleCollection {
        <<Singleton>>
        -VehicleCollection _instance$
        +Instance$ VehicleCollection
        +ICollection~Vehicle~ Vehicles
        -VehicleCollection()
    }

    %% Repository Implementations
    class MyVehiclesRepository {
        +GetVehicles() ICollection~Vehicle~
        +AddVehicle(vehicle) void
        +Find(id) Vehicle
    }

    class DBVehicleRepository {
        +GetVehicles() ICollection~Vehicle~
        +AddVehicle(vehicle) void
        +Find(id) Vehicle
    }

    %% Controller
    class HomeController {
        -IVehicleRepository _vehicleRepository
        +HomeController(vehicleRepository)
        +Index() IActionResult
        +AddMustang() IActionResult
        +AddExplorer() IActionResult
        +StartEngine(id) IActionResult
        +StopEngine(id) IActionResult
        +AddGas(id) IActionResult
        +Error() IActionResult
    }

    %% ViewModels
    class HomeViewModel {
        +ICollection~Vehicle~ Vehicles
    }

    class ErrorViewModel {
        +string RequestId
        +bool ShowRequestId
    }

    %% Dependency Injection
    class ServicesConfiguration {
        <<static>>
        +ConfigureRepositories(services)$ void
    }

    %% Relationships - Inheritance and Implementation
    IVehicle <|.. Vehicle : implements
    Vehicle <|-- Car : extends
    Vehicle <|-- Motocycle : extends

    %% Repository Pattern
    IVehicleRepository <|.. MyVehiclesRepository : implements
    IVehicleRepository <|.. DBVehicleRepository : implements

    %% Factory Pattern
    Creator <|-- FordMustangCreator : extends
    Creator <|-- FordExplorerCreator : extends
    Creator ..> Vehicle : creates
    FordMustangCreator ..> CarBuilder : uses
    FordExplorerCreator ..> CarBuilder : uses
    CarBuilder ..> Car : builds

    %% Singleton Usage
    MyVehiclesRepository ..> VehicleCollection : uses
    VehicleCollection o-- Vehicle : contains

    %% Controller Dependencies
    HomeController --> IVehicleRepository : depends on
    HomeController ..> FordMustangCreator : instantiates
    HomeController ..> FordExplorerCreator : instantiates
    HomeController ..> HomeViewModel : uses
    HomeController ..> ErrorViewModel : uses
    HomeViewModel o-- Vehicle : contains

    %% DI Configuration
    ServicesConfiguration ..> IVehicleRepository : registers
    ServicesConfiguration ..> MyVehiclesRepository : registers
```

## Design Patterns Visualization

### 1. Factory Method Pattern
```
Creator (Abstract)
    ├── FordMustangCreator → creates Car via CarBuilder
    └── FordExplorerCreator → creates Car via CarBuilder
```

### 2. Builder Pattern
```
CarBuilder
    ├── SetBrand() → returns CarBuilder (fluent)
    ├── SetModel() → returns CarBuilder (fluent)
    ├── SetColor() → returns CarBuilder (fluent)
    └── Build() → returns Car
```

### 3. Singleton Pattern
```
VehicleCollection
    ├── private static _instance
    ├── public static Instance (lazy initialization)
    └── public Vehicles (ICollection<Vehicle>)
```

### 4. Repository Pattern
```
IVehicleRepository (Interface)
    ├── MyVehiclesRepository (In-memory using Singleton)
    └── DBVehicleRepository (Stub for database)
```

## Key Relationships

| From | To | Relationship Type | Description |
|------|-----|------------------|-------------|
| Vehicle | IVehicle | Implementation | Vehicle implements IVehicle interface |
| Car, Motorcycle | Vehicle | Inheritance | Concrete vehicles extend abstract Vehicle |
| MyVehiclesRepository | IVehicleRepository | Implementation | Repository implementation |
| HomeController | IVehicleRepository | Dependency | Injected via constructor |
| FordMustangCreator | Creator | Inheritance | Concrete factory extends abstract factory |
| FordMustangCreator | CarBuilder | Dependency | Uses builder to create cars |
| MyVehiclesRepository | VehicleCollection | Association | Uses singleton for storage |
| VehicleCollection | Vehicle | Composition | Contains collection of vehicles |
| HomeController | Creator | Instantiation | Creates factory instances directly |

## How to Use These Diagrams

### PlantUML (UML-ClassDiagram.plantuml)
1. **Online Renderer**:
   - Visit https://www.plantuml.com/plantuml/uml/
   - Copy and paste the content

2. **VS Code Extension**:
   - Install "PlantUML" extension
   - Open the `.plantuml` file
   - Press `Alt+D` to preview

3. **IntelliJ/Rider**:
   - Install "PlantUML Integration" plugin
   - Right-click file → "Show PlantUML Diagram"

### Mermaid (This file)
1. **GitHub**: View this markdown file on GitHub (renders automatically)
2. **VS Code**: Install "Markdown Preview Mermaid Support" extension
3. **Online**: https://mermaid.live/

## Notes

- **VehicleCollection**: Thread-unsafe singleton for educational purposes
- **DBVehicleRepository**: Placeholder - all methods throw NotImplementedException
- **HomeController**: Directly instantiates factories (could be improved with DI)
- **Error Handling**: Uses query parameters instead of TempData or ModelState

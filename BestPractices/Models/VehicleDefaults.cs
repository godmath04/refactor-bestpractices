using System;

namespace Best_Practices.Models
{
    /// <summary>
    /// Clase de configuración que contiene valores por defecto para vehículos.
    /// Implementa el patrón Configuration Object para gestionar propiedades por defecto.
    /// </summary>
    /// <remarks>
    /// PATRÓN IMPLEMENTADO: Configuration Object + Singleton
    ///
    /// PROPÓSITO:
    /// - Centralizar valores por defecto para vehículos
    /// - Facilitar adición de nuevas propiedades sin modificar múltiples clases
    /// - Preparado para recibir 20+ propiedades adicionales en próximo sprint
    ///
    /// CÓMO AGREGAR NUEVAS PROPIEDADES EN PRÓXIMO SPRINT:
    /// 1. Agregar propiedad aquí con valor por defecto
    /// 2. Agregar propiedad correspondiente en Vehicle.cs (si aplica)
    /// 3. El Builder automáticamente usará el valor por defecto
    /// 4. No requiere cambios en Factory ni Controller
    ///
    /// VENTAJAS:
    /// - Single Source of Truth para valores por defecto
    /// - Fácil mantenimiento
    /// - Configuración centralizada
    /// - Preparado para inyección de configuración desde appsettings.json
    /// </remarks>
    public class VehicleDefaults
    {
        private static readonly Lazy<VehicleDefaults> _instance =
            new Lazy<VehicleDefaults>(() => new VehicleDefaults());

        /// <summary>
        /// Obtiene la instancia singleton de VehicleDefaults.
        /// </summary>
        public static VehicleDefaults Instance => _instance.Value;

        /// <summary>
        /// Constructor privado para patrón Singleton.
        /// </summary>
        private VehicleDefaults()
        {
            // Inicializar valores por defecto actuales
            Year = DateTime.Now.Year;
        }

        #region Propiedades Actuales del Sprint

        /// <summary>
        /// Año actual del vehículo.
        /// Por defecto es el año actual del sistema.
        /// </summary>
        /// <remarks>
        /// Requisito del sprint actual: agregar año a todos los vehículos.
        /// </remarks>
        public int Year { get; set; }

        #endregion

        #region Propiedades Preparadas para Próximo Sprint

        // TODO: Próximo Sprint - Descomentar y configurar según requisitos del negocio

        /// <summary>
        /// Número de identificación del vehículo (VIN).
        /// </summary>
        // public string VIN { get; set; } = string.Empty;

        /// <summary>
        /// País de fabricación del vehículo.
        /// </summary>
        // public string CountryOfManufacture { get; set; } = "USA";

        /// <summary>
        /// Tipo de transmisión (Automática, Manual, CVT).
        /// </summary>
        // public string TransmissionType { get; set; } = "Automatic";

        /// <summary>
        /// Número de puertas del vehículo.
        /// </summary>
        // public int NumberOfDoors { get; set; } = 4;

        /// <summary>
        /// Capacidad de pasajeros.
        /// </summary>
        // public int PassengerCapacity { get; set; } = 5;

        /// <summary>
        /// Tipo de combustible (Gasolina, Diesel, Eléctrico, Híbrido).
        /// </summary>
        // public string FuelType { get; set; } = "Gasoline";

        /// <summary>
        /// Cilindrada del motor en litros.
        /// </summary>
        // public double EngineDisplacement { get; set; } = 2.0;

        /// <summary>
        /// Potencia del motor en caballos de fuerza.
        /// </summary>
        // public int Horsepower { get; set; } = 150;

        /// <summary>
        /// Rendimiento de combustible en millas por galón (ciudad).
        /// </summary>
        // public double MPG_City { get; set; } = 25.0;

        /// <summary>
        /// Rendimiento de combustible en millas por galón (carretera).
        /// </summary>
        // public double MPG_Highway { get; set; } = 35.0;

        /// <summary>
        /// Peso del vehículo en libras.
        /// </summary>
        // public double WeightPounds { get; set; } = 3500.0;

        /// <summary>
        /// Longitud del vehículo en pulgadas.
        /// </summary>
        // public double LengthInches { get; set; } = 180.0;

        /// <summary>
        /// Ancho del vehículo en pulgadas.
        /// </summary>
        // public double WidthInches { get; set; } = 72.0;

        /// <summary>
        /// Altura del vehículo en pulgadas.
        /// </summary>
        // public double HeightInches { get; set; } = 60.0;

        /// <summary>
        /// Indica si el vehículo tiene tracción en las cuatro ruedas.
        /// </summary>
        // public bool HasFourWheelDrive { get; set; } = false;

        /// <summary>
        /// Indica si el vehículo tiene sistema de navegación.
        /// </summary>
        // public bool HasNavigationSystem { get; set; } = false;

        /// <summary>
        /// Indica si el vehículo tiene cámara de reversa.
        /// </summary>
        // public bool HasBackupCamera { get; set; } = false;

        /// <summary>
        /// Indica si el vehículo tiene techo solar.
        /// </summary>
        // public bool HasSunroof { get; set; } = false;

        /// <summary>
        /// Indica si el vehículo tiene sistema de audio premium.
        /// </summary>
        // public bool HasPremiumAudio { get; set; } = false;

        /// <summary>
        /// Garantía del vehículo en años.
        /// </summary>
        // public int WarrantyYears { get; set; } = 3;

        #endregion

        #region Métodos de Utilidad

        /// <summary>
        /// Restablece todos los valores a sus defaults de fábrica.
        /// Útil para testing y reinicio de configuración.
        /// </summary>
        public void ResetToDefaults()
        {
            Year = DateTime.Now.Year;

            // TODO: Próximo Sprint - Agregar reset para nuevas propiedades
            // VIN = string.Empty;
            // CountryOfManufacture = "USA";
            // TransmissionType = "Automatic";
            // etc...
        }

        /// <summary>
        /// Crea una copia de los defaults actuales.
        /// Útil para crear configuraciones personalizadas sin afectar el singleton.
        /// </summary>
        /// <returns>Nueva instancia de VehicleDefaults con los mismos valores.</returns>
        public VehicleDefaults Clone()
        {
            return new VehicleDefaults
            {
                Year = this.Year
                // TODO: Próximo Sprint - Agregar clonación de nuevas propiedades
            };
        }

        #endregion
    }
}

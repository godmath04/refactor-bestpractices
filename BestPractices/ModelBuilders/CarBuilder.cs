using Best_Practices.Models;

namespace Best_Practices.ModelBuilders
{
    /// <summary>
    /// Builder class for constructing Car instances with a fluent interface.
    /// Implements the Builder pattern for flexible Car object creation.
    /// Integrado con VehicleDefaults para gestionar propiedades por defecto de forma centralizada.
    /// </summary>
    /// <remarks>
    /// PATRÓN IMPLEMENTADO: Builder Pattern + Configuration Object
    ///
    /// VENTAJAS:
    /// - Construcción fluida de objetos Car
    /// - Valores por defecto centralizados en VehicleDefaults
    /// - Fácil adición de nuevas propiedades en próximo sprint
    ///
    /// CÓMO AGREGAR NUEVAS PROPIEDADES EN PRÓXIMO SPRINT:
    /// 1. Agregar campo privado aquí (ej: private string _vin)
    /// 2. Agregar método Set (ej: SetVIN(string vin))
    /// 3. En Build(), pasar el valor al constructor de Car
    /// 4. No requiere cambios en Factory ni Controller
    /// </remarks>
    public class CarBuilder
    {
        #region Campos Privados - Propiedades Actuales

        private string _brand = "Ford";
        private string _model = "Mustang";
        private string _color = "Red";
        private int? _year = null; // null = usar valor de VehicleDefaults

        #endregion

        #region Campos Privados - Preparados para Próximo Sprint

        // TODO: Próximo Sprint - Descomentar según se necesiten las propiedades

        // private string _vin = null;
        // private string _countryOfManufacture = null;
        // private string _transmissionType = null;
        // private int? _numberOfDoors = null;
        // private int? _passengerCapacity = null;
        // private string _fuelType = null;
        // private double? _engineDisplacement = null;
        // private int? _horsepower = null;
        // private double? _mpgCity = null;
        // private double? _mpgHighway = null;
        // private double? _weightPounds = null;
        // private double? _lengthInches = null;
        // private double? _widthInches = null;
        // private double? _heightInches = null;
        // private bool? _hasFourWheelDrive = null;
        // private bool? _hasNavigationSystem = null;
        // private bool? _hasBackupCamera = null;
        // private bool? _hasSunroof = null;
        // private bool? _hasPremiumAudio = null;
        // private int? _warrantyYears = null;

        #endregion

        #region Métodos Set - Propiedades Actuales

        /// <summary>
        /// Sets the brand of the car.
        /// </summary>
        /// <param name="brand">The brand/manufacturer name.</param>
        /// <returns>The builder instance for method chaining.</returns>
        public CarBuilder SetBrand(string brand)
        {
            _brand = brand;
            return this;
        }

        /// <summary>
        /// Sets the model of the car.
        /// </summary>
        /// <param name="model">The model name.</param>
        /// <returns>The builder instance for method chaining.</returns>
        public CarBuilder SetModel(string model)
        {
            _model = model;
            return this;
        }

        /// <summary>
        /// Sets the color of the car.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The builder instance for method chaining.</returns>
        public CarBuilder SetColor(string color)
        {
            _color = color;
            return this;
        }

        /// <summary>
        /// Sets the year of the car.
        /// </summary>
        /// <param name="year">The year. If null, uses current year from VehicleDefaults.</param>
        /// <returns>The builder instance for method chaining.</returns>
        /// <remarks>
        /// Propiedad agregada según requisito del negocio actual.
        /// Si no se especifica, se usa VehicleDefaults.Instance.Year
        /// </remarks>
        public CarBuilder SetYear(int? year)
        {
            _year = year;
            return this;
        }

        #endregion

        #region Métodos Set - Preparados para Próximo Sprint

        // TODO: Próximo Sprint - Descomentar según se necesiten

        // public CarBuilder SetVIN(string vin)
        // {
        //     _vin = vin;
        //     return this;
        // }

        // public CarBuilder SetCountryOfManufacture(string country)
        // {
        //     _countryOfManufacture = country;
        //     return this;
        // }

        // public CarBuilder SetTransmissionType(string transmission)
        // {
        //     _transmissionType = transmission;
        //     return this;
        // }

        // public CarBuilder SetNumberOfDoors(int doors)
        // {
        //     _numberOfDoors = doors;
        //     return this;
        // }

        // public CarBuilder SetPassengerCapacity(int capacity)
        // {
        //     _passengerCapacity = capacity;
        //     return this;
        // }

        // public CarBuilder SetFuelType(string fuelType)
        // {
        //     _fuelType = fuelType;
        //     return this;
        // }

        // public CarBuilder SetEngineDisplacement(double displacement)
        // {
        //     _engineDisplacement = displacement;
        //     return this;
        // }

        // public CarBuilder SetHorsepower(int horsepower)
        // {
        //     _horsepower = horsepower;
        //     return this;
        // }

        // public CarBuilder SetMPG_City(double mpg)
        // {
        //     _mpgCity = mpg;
        //     return this;
        // }

        // public CarBuilder SetMPG_Highway(double mpg)
        // {
        //     _mpgHighway = mpg;
        //     return this;
        // }

        // public CarBuilder SetWeight(double weight)
        // {
        //     _weightPounds = weight;
        //     return this;
        // }

        // public CarBuilder SetDimensions(double length, double width, double height)
        // {
        //     _lengthInches = length;
        //     _widthInches = width;
        //     _heightInches = height;
        //     return this;
        // }

        // public CarBuilder SetFourWheelDrive(bool has4WD)
        // {
        //     _hasFourWheelDrive = has4WD;
        //     return this;
        // }

        // public CarBuilder SetNavigationSystem(bool hasNav)
        // {
        //     _hasNavigationSystem = hasNav;
        //     return this;
        // }

        // public CarBuilder SetBackupCamera(bool hasCamera)
        // {
        //     _hasBackupCamera = hasCamera;
        //     return this;
        // }

        // public CarBuilder SetSunroof(bool hasSunroof)
        // {
        //     _hasSunroof = hasSunroof;
        //     return this;
        // }

        // public CarBuilder SetPremiumAudio(bool hasAudio)
        // {
        //     _hasPremiumAudio = hasAudio;
        //     return this;
        // }

        // public CarBuilder SetWarrantyYears(int years)
        // {
        //     _warrantyYears = years;
        //     return this;
        // }

        #endregion

        #region Build Method

        /// <summary>
        /// Builds and returns a new Car instance with the configured properties.
        /// Si alguna propiedad no fue configurada, se usarán valores de VehicleDefaults.
        /// </summary>
        /// <returns>A new Car instance.</returns>
        /// <remarks>
        /// Este método aplica el patrón de valores por defecto:
        /// 1. Si el builder tiene un valor configurado, se usa ese valor
        /// 2. Si no, el constructor de Car usa VehicleDefaults automáticamente
        /// 3. Para próximo sprint, solo agregar parámetros adicionales al constructor
        /// </remarks>
        public Car Build()
        {
            // Construcción básica actual
            var car = new Car(_color, _brand, _model, _year);

            // TODO: Próximo Sprint - Cuando Car tenga más propiedades en constructor:
            // var car = new Car(
            //     _color,
            //     _brand,
            //     _model,
            //     _year,
            //     _vin,
            //     _countryOfManufacture,
            //     _transmissionType,
            //     etc...
            // );

            return car;
        }

        #endregion

        #region Métodos de Utilidad

        /// <summary>
        /// Restablece el builder a valores por defecto.
        /// Útil para reutilizar el mismo builder para múltiples objetos.
        /// </summary>
        /// <returns>The builder instance for method chaining.</returns>
        public CarBuilder Reset()
        {
            _brand = "Ford";
            _model = "Mustang";
            _color = "Red";
            _year = null;

            // TODO: Próximo Sprint - Resetear propiedades adicionales
            // _vin = null;
            // _countryOfManufacture = null;
            // etc...

            return this;
        }

        /// <summary>
        /// Crea una copia del builder actual.
        /// Útil para crear variaciones basadas en una configuración base.
        /// </summary>
        /// <returns>New builder instance with same configuration.</returns>
        public CarBuilder Clone()
        {
            return new CarBuilder
            {
                _brand = this._brand,
                _model = this._model,
                _color = this._color,
                _year = this._year

                // TODO: Próximo Sprint - Clonar propiedades adicionales
                // _vin = this._vin,
                // _countryOfManufacture = this._countryOfManufacture,
                // etc...
            };
        }

        #endregion
    }
}

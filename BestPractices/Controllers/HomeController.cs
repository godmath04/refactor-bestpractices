using Best_Practices.Infraestructure.Factories;
using Best_Practices.Models;
using Best_Practices.Models.Exceptions;
using Best_Practices.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Best_Practices.Controllers
{
    /// <summary>
    /// Controller for managing vehicle operations and views.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleRepository _vehicleRepository;

        /// <summary>
        /// Initializes a new instance of the HomeController class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository for data access.</param>
        /// <param name="logger">The logger instance.</param>
        public HomeController(IVehicleRepository vehicleRepository, ILogger<HomeController> logger)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Displays the home page with the list of vehicles.
        /// </summary>
        /// <returns>The index view with vehicle list.</returns>
        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                Vehicles = _vehicleRepository.GetVehicles()
            };

            // Muestra mensaje de exito
            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            // Muestra mensaje de error
            if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }

            return View(model);
        }

        /// <summary>
        /// Añade un Ford Mustang a la collection con fuel inicial
        /// </summary>
        /// <returns>Redirect to home page.</returns>
        [HttpGet]
        public IActionResult AddMustang()
        {
            try
            {
                // Crea un vehiculo mediante el patrón factory
                var factory = new FordMustangCreator();
                var vehicle = factory.Create();

                // Inicializar el vehiculo con gasolina
                InitializeVehicleWithFuel(vehicle, fullTank: false);

                // Add to repository
                _vehicleRepository.AddVehicle(vehicle);

                _logger.LogInformation("Added Ford Mustang with ID {VehicleId}, Color: {Color}, Fuel: {Fuel}/{FuelLimit}",
                    vehicle.Id, vehicle.Color, vehicle.Gas, vehicle.FuelLimit);

                TempData["SuccessMessage"] = $"Successfully added {vehicle.Brand} {vehicle.Model} ({vehicle.Color}) with {vehicle.Gas:N1} gallons of fuel.";
                return RedirectToAction(nameof(Index));
            }
            catch (VehicleException vex)
            {
                _logger.LogWarning(vex, "Vehicle-specific error adding Ford Mustang");
                TempData["ErrorMessage"] = $"Error adding vehicle: {vex.Message}";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error adding Ford Mustang");
                TempData["ErrorMessage"] = "An unexpected error occurred while adding the vehicle.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Añade un Ford Explorer a la collection con fuel inicial
        /// </summary>
        /// <returns>Redirect to home page.</returns>
        [HttpGet]
        public IActionResult AddExplorer()
        {
            try
            {
                // Crear vehiculo mediante factory pattern
                var factory = new FordExplorerCreator();
                var vehicle = factory.Create();

                //Inicializar vehiculo con fuel
                InitializeVehicleWithFuel(vehicle, fullTank: false);

                // Add to repository
                _vehicleRepository.AddVehicle(vehicle);

                _logger.LogInformation("Added Ford Explorer with ID {VehicleId}, Color: {Color}, Fuel: {Fuel}/{FuelLimit}",
                    vehicle.Id, vehicle.Color, vehicle.Gas, vehicle.FuelLimit);

                TempData["SuccessMessage"] = $"Successfully added {vehicle.Brand} {vehicle.Model} ({vehicle.Color}) with {vehicle.Gas:N1} gallons of fuel.";
                return RedirectToAction(nameof(Index));
            }
            catch (VehicleException vex)
            {
                _logger.LogWarning(vex, "Vehicle-specific error adding Ford Explorer");
                TempData["ErrorMessage"] = $"Error adding vehicle: {vex.Message}";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error adding Ford Explorer");
                TempData["ErrorMessage"] = "An unexpected error occurred while adding the vehicle.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Inicia el motor para un vehiculo espeficio
        /// </summary>
        /// <param name="id">The identificado unico de vehiculo.</param>
        /// <returns>Redirect to home page.</returns>
        [HttpGet]
        public IActionResult StartEngine(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid vehicleId))
                {
                    TempData["ErrorMessage"] = "Invalid vehicle ID.";
                    return RedirectToAction(nameof(Index));
                }

                var vehicle = _vehicleRepository.Find(vehicleId);
                if (vehicle == null)
                {
                    TempData["ErrorMessage"] = "Vehicle not found.";
                    _logger.LogWarning("Attempted to start engine for non-existent vehicle {VehicleId}", vehicleId);
                    return RedirectToAction(nameof(Index));
                }

                vehicle.StartEngine();
                _logger.LogInformation("Started engine for vehicle {VehicleId} ({Brand} {Model})",
                    vehicleId, vehicle.Brand, vehicle.Model);

                TempData["SuccessMessage"] = $"Engine started for {vehicle.Brand} {vehicle.Model}.";
                return RedirectToAction(nameof(Index));
            }
            catch (EngineFaultException ex)
            {
                _logger.LogWarning(ex, "Engine fault for vehicle {VehicleId}", id);
                TempData["ErrorMessage"] = $"Engine Error: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
            catch (FuelException ex)
            {
                _logger.LogWarning(ex, "Fuel issue for vehicle {VehicleId}", id);
                TempData["ErrorMessage"] = $"Fuel Error: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error starting engine for vehicle {VehicleId}", id);
                TempData["ErrorMessage"] = "An unexpected error occurred while starting the engine.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Adds fuel to a specific vehicle.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle.</param>
        /// <returns>Redirect to home page.</returns>
        [HttpGet]
        public IActionResult AddGas(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid vehicleId))
                {
                    TempData["ErrorMessage"] = "Invalid vehicle ID.";
                    return RedirectToAction(nameof(Index));
                }

                var vehicle = _vehicleRepository.Find(vehicleId);
                if (vehicle == null)
                {
                    TempData["ErrorMessage"] = "Vehicle not found.";
                    _logger.LogWarning("Attempted to add fuel to non-existent vehicle {VehicleId}", vehicleId);
                    return RedirectToAction(nameof(Index));
                }

                var previousGas = vehicle.Gas;
                vehicle.AddGas();

                _logger.LogInformation("Added fuel to vehicle {VehicleId} ({Brand} {Model}). Fuel: {PreviousGas} -> {CurrentGas}",
                    vehicleId, vehicle.Brand, vehicle.Model, previousGas, vehicle.Gas);

                TempData["SuccessMessage"] = $"Added fuel to {vehicle.Brand} {vehicle.Model}. Current fuel: {vehicle.Gas:N1}/{vehicle.FuelLimit:N1} gallons.";
                return RedirectToAction(nameof(Index));
            }
            catch (FuelException ex)
            {
                _logger.LogWarning(ex, "Fuel issue for vehicle {VehicleId}", id);
                TempData["ErrorMessage"] = $"Fuel Error: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error adding fuel to vehicle {VehicleId}", id);
                TempData["ErrorMessage"] = "An unexpected error occurred while adding fuel.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Stops the engine of a specific vehicle.
        /// </summary>
        /// <param name="id">The unique identifier of the vehicle.</param>
        /// <returns>Redirect to home page.</returns>
        [HttpGet]
        public IActionResult StopEngine(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid vehicleId))
                {
                    TempData["ErrorMessage"] = "Invalid vehicle ID.";
                    return RedirectToAction(nameof(Index));
                }

                var vehicle = _vehicleRepository.Find(vehicleId);
                if (vehicle == null)
                {
                    TempData["ErrorMessage"] = "Vehicle not found.";
                    _logger.LogWarning("Attempted to stop engine for non-existent vehicle {VehicleId}", vehicleId);
                    return RedirectToAction(nameof(Index));
                }

                vehicle.StopEngine();
                _logger.LogInformation("Stopped engine for vehicle {VehicleId} ({Brand} {Model})",
                    vehicleId, vehicle.Brand, vehicle.Model);

                TempData["SuccessMessage"] = $"Engine stopped for {vehicle.Brand} {vehicle.Model}.";
                return RedirectToAction(nameof(Index));
            }
            catch (EngineFaultException ex)
            {
                _logger.LogWarning(ex, "Engine fault for vehicle {VehicleId}", id);
                TempData["ErrorMessage"] = $"Engine Error: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error stopping engine for vehicle {VehicleId}", id);
                TempData["ErrorMessage"] = "An unexpected error occurred while stopping the engine.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Displays the privacy policy page.
        /// </summary>
        /// <returns>The privacy view.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Displays the error page.
        /// </summary>
        /// <returns>The error view.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Metodo para iniciar vehiculo con fuel. Se añade fuel inicial para el uso
        /// </summary>
        /// <param name="vehicle">The vehicle to initialize.</param>
        /// <param name="fullTank">If true, fills tank completely; if false, adds partial fuel.</param>
        private void InitializeVehicleWithFuel(Vehicle vehicle, bool fullTank = false)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));

            try
            {
                if (fullTank)
                {
                    // Tanke lleno
                    while (vehicle.Gas < vehicle.FuelLimit)
                    {
                        vehicle.AddGas();
                    }
                }
                else
                {
                    // Añadir 50% para usabilidad inicial
                    var targetGas = vehicle.FuelLimit * 0.5;
                    while (vehicle.Gas < targetGas)
                    {
                        vehicle.AddGas();
                    }
                }
            }
            catch (FuelException)
            {
                //Tanke lleno o cerca de estar lleno
                _logger.LogDebug("Vehicle {VehicleId} fuel tank reached capacity during initialization", vehicle.Id);
            }
        }
    }
}
